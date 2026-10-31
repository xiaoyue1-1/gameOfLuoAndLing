using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class Keep_16_9 : MonoBehaviour
{
    public float targetAspect = 16f / 9f;
    [SerializeField, Range(1f, 2f)] private float backgroundZoom = 1.15f;
    [SerializeField, Range(1, 6)] private int blurIterations = 4;
    [SerializeField, Range(1f, 8f)] private float blurRadius = 2f;
    private Camera cam;
    private Rect originalRect;
    private RenderTexture originalTarget;
    private Canvas borderCanvas;
    private RawImage sharpImage, backgroundImage;
    private RenderTexture frame, blurA, blurB;
    private Material blurMaterial;
    private int screenWidth, screenHeight;
    private float appliedAspect, appliedZoom;
    private bool hasBorders;
    private Camera inputCamera;
    private readonly List<GraphicRaycaster> originalRaycasters = new List<GraphicRaycaster>();
    private readonly List<CameraBorderInputRaycaster> inputRaycasters = new List<CameraBorderInputRaycaster>();

    // This camera now owns its frame and blur textures; skip the legacy plugin.
    public bool NeedsBlur => false;
    private void Awake() => cam = GetComponent<Camera>();
    private void OnEnable()
    {
        if (cam == null) cam = GetComponent<Camera>();
        originalRect = cam.rect;
        originalTarget = cam.targetTexture;
        RenderPipelineManager.endCameraRendering += OnCameraRendered;
        UpdateViewport();
    }
    private void LateUpdate()
    {
        if (Screen.width != screenWidth || Screen.height != screenHeight ||
            !Mathf.Approximately(targetAspect, appliedAspect) ||
            !Mathf.Approximately(backgroundZoom, appliedZoom)) UpdateViewport();
        if (borderCanvas != null) borderCanvas.enabled = hasBorders && cam.enabled;
        if (hasBorders && inputCamera != null)
        {
            inputCamera.transform.SetPositionAndRotation(cam.transform.position, cam.transform.rotation);
            inputCamera.orthographicSize = cam.orthographicSize;
            inputCamera.fieldOfView = cam.fieldOfView;
        }
    }
    private void UpdateViewport()
    {
        screenWidth = Mathf.Max(1, Screen.width);
        screenHeight = Mathf.Max(1, Screen.height);
        appliedAspect = targetAspect;
        appliedZoom = backgroundZoom;
        if (originalTarget != null) return;
        float aspect = Mathf.Max(0.01f, targetAspect);
        float windowAspect = (float)screenWidth / screenHeight;
        float width = Mathf.Min(1f, aspect / windowAspect);
        float height = Mathf.Min(1f, windowAspect / aspect);
        Rect viewport = new Rect((1f - width) * 0.5f, (1f - height) * 0.5f, width, height);
        hasBorders = width < 0.9999f || height < 0.9999f;
        if (!hasBorders)
        {
            cam.targetTexture = null;
            cam.rect = viewport;
            cam.ResetAspect();
            RestoreInput();
            if (borderCanvas != null) borderCanvas.enabled = false;
            ReleaseTextures();
            return;
        }
        if (borderCanvas == null && !CreateDisplay()) { cam.rect = viewport; return; }
        int frameWidth = Mathf.Max(1, Mathf.RoundToInt(screenWidth * width));
        int frameHeight = Mathf.Max(1, Mathf.RoundToInt(screenHeight * height));
        if (frame == null || frame.width != frameWidth || frame.height != frameHeight)
        {
            cam.targetTexture = null;
            ReleaseTextures();
            frame = CreateTexture(frameWidth, frameHeight, 24, "Camera sharp frame");
            blurA = CreateTexture(Mathf.Max(1, frameWidth / 8), Mathf.Max(1, frameHeight / 8), 0, "Camera blur A");
            blurB = CreateTexture(blurA.width, blurA.height, 0, "Camera blur B");
        }
        cam.targetTexture = frame;
        cam.rect = new Rect(0, 0, 1, 1);
        cam.aspect = aspect;
        ConfigureInput(viewport);
        sharpImage.texture = frame;
        backgroundImage.texture = blurA;
        SetArea(sharpImage.rectTransform, viewport);
        float uvWidth = Mathf.Min(1f, windowAspect / aspect) / Mathf.Max(1f, backgroundZoom);
        float uvHeight = Mathf.Min(1f, aspect / windowAspect) / Mathf.Max(1f, backgroundZoom);
        backgroundImage.uvRect = new Rect((1f - uvWidth) * 0.5f, (1f - uvHeight) * 0.5f, uvWidth, uvHeight);
        borderCanvas.targetDisplay = cam.targetDisplay;
        borderCanvas.enabled = cam.enabled;
    }
    private bool CreateDisplay()
    {
        Shader shader = Resources.Load<Shader>("CameraFrameBlur");
        if (shader == null || !shader.isSupported)
        {
            Debug.LogError("Camera frame blur shader is unavailable.", this);
            return false;
        }
        blurMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
        var root = new GameObject("Blurred Camera Borders", typeof(RectTransform), typeof(Canvas));
        root.transform.SetParent(transform, false);
        borderCanvas = root.GetComponent<Canvas>();
        borderCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        borderCanvas.sortingOrder = short.MinValue;
        backgroundImage = CreateImage("Blurred Background", root.transform);
        sharpImage = CreateImage("Sharp Camera Frame", root.transform);
        return true;
    }
    private static RawImage CreateImage(string name, Transform parent)
    {
        var obj = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(RawImage));
        obj.transform.SetParent(parent, false);
        var image = obj.GetComponent<RawImage>();
        image.raycastTarget = false;
        image.maskable = false;
        SetArea(image.rectTransform, new Rect(0, 0, 1, 1));
        return image;
    }
    private static void SetArea(RectTransform rect, Rect area)
    {
        rect.anchorMin = area.min;
        rect.anchorMax = area.max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }
    private static RenderTexture CreateTexture(int width, int height, int depth, string name)
    {
        var texture = new RenderTexture(width, height, depth, RenderTextureFormat.ARGB32)
        {
            name = name, filterMode = FilterMode.Bilinear, wrapMode = TextureWrapMode.Clamp,
            hideFlags = HideFlags.HideAndDontSave
        };
        texture.Create();
        return texture;
    }
    private void OnCameraRendered(ScriptableRenderContext context, Camera renderedCamera)
    {
        if (renderedCamera != cam || !hasBorders || frame == null || blurMaterial == null) return;
        // The completed camera image is passed directly into our blur shader.
        RenderTexture previous = RenderTexture.active;
        Graphics.Blit(frame, blurA);
        for (int i = 0; i < blurIterations; i++)
        {
            blurMaterial.SetVector("_BlurStep", new Vector4(blurRadius / blurA.width, 0, 0, 0));
            Graphics.Blit(blurA, blurB, blurMaterial);
            blurMaterial.SetVector("_BlurStep", new Vector4(0, blurRadius / blurA.height, 0, 0));
            Graphics.Blit(blurB, blurA, blurMaterial);
        }
        RenderTexture.active = previous;
    }
    private void ReleaseTextures()
    {
        ReleaseTexture(ref frame);
        ReleaseTexture(ref blurA);
        ReleaseTexture(ref blurB);
    }
    private void ConfigureInput(Rect viewport)
    {
        if (inputCamera == null)
        {
            var obj = new GameObject("Camera Border Input Mapping", typeof(Camera));
            obj.hideFlags = HideFlags.HideAndDontSave;
            inputCamera = obj.GetComponent<Camera>();
        }
        inputCamera.CopyFrom(cam);
        inputCamera.targetTexture = null;
        inputCamera.rect = viewport;
        inputCamera.aspect = Mathf.Max(0.01f, targetAspect);
        inputCamera.enabled = false;
        inputCamera.transform.SetPositionAndRotation(cam.transform.position, cam.transform.rotation);
        foreach (var canvas in FindObjectsOfType<Canvas>(true))
        {
            if (canvas.renderMode != RenderMode.ScreenSpaceCamera || canvas.worldCamera != cam) continue;
            var raycaster = canvas.GetComponent<GraphicRaycaster>();
            if (raycaster == null || !raycaster.enabled || raycaster is CameraBorderInputRaycaster) continue;
            var mapped = canvas.gameObject.AddComponent<CameraBorderInputRaycaster>();
            mapped.InputCamera = inputCamera;
            mapped.ignoreReversedGraphics = raycaster.ignoreReversedGraphics;
            mapped.blockingObjects = raycaster.blockingObjects;
            mapped.blockingMask = raycaster.blockingMask;
            originalRaycasters.Add(raycaster);
            inputRaycasters.Add(mapped);
            raycaster.enabled = false;
        }
    }
    private void RestoreInput()
    {
        foreach (var mapped in inputRaycasters) if (mapped != null) { mapped.enabled = false; Destroy(mapped); }
        foreach (var original in originalRaycasters) if (original != null) original.enabled = true;
        inputRaycasters.Clear();
        originalRaycasters.Clear();
        if (inputCamera != null) Destroy(inputCamera.gameObject);
        inputCamera = null;
    }
    private static void ReleaseTexture(ref RenderTexture texture)
    {
        if (texture == null) return;
        texture.Release();
        Destroy(texture);
        texture = null;
    }
    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= OnCameraRendered;
        RestoreInput();
        if (cam != null)
        {
            cam.targetTexture = originalTarget;
            cam.rect = originalRect;
            cam.ResetAspect();
        }
        if (borderCanvas != null) Destroy(borderCanvas.gameObject);
        if (blurMaterial != null) Destroy(blurMaterial);
        borderCanvas = null;
        blurMaterial = null;
        ReleaseTextures();
    }
}
