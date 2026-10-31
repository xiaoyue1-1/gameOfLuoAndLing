using Unified.UniversalBlur.Runtime;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// Uses the installed package's render pass and algorithm unchanged. The only
// addition is gating it to cameras that actually need non-16:9 border fill.
public sealed class CameraBorderBlurFeature : UniversalBlurFeature
{
    private Camera trackedCamera;
    private Keep_16_9 trackedFraming;
    private Vector4 lastViewport;
    private static readonly int TextureViewportId = Shader.PropertyToID("_UnifiedBorderTextureViewport");
    private bool diagnosticPrinted;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        Camera camera = renderingData.cameraData.camera;
        if (camera.cameraType == CameraType.Game && !diagnosticPrinted)
        {
            camera.TryGetComponent<Keep_16_9>(out var framing);
            Debug.Log($"SkyBlurDiagnostic camera={camera.name} target={camera.targetTexture} framing={framing} needs={framing != null && framing.NeedsBlur} viewport={camera.rect}");
            diagnosticPrinted = true;
        }
        if (camera.cameraType != CameraType.Game || camera.targetTexture != null) return;

        if (trackedCamera != camera)
        {
            trackedCamera = camera;
            camera.TryGetComponent(out trackedFraming);
        }
        if (trackedFraming == null || !trackedFraming.NeedsBlur) return;

        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        float textureAspect = (float)descriptor.width / Mathf.Max(1, descriptor.height);
        Rect rect = camera.rect;
        Vector4 viewport = Mathf.Abs(textureAspect - trackedFraming.targetAspect) < 0.02f
            ? new Vector4(0, 0, 1, 1)
            : new Vector4(rect.x, rect.y, rect.width, rect.height);
        if (lastViewport != viewport)
        {
            Shader.SetGlobalVector(TextureViewportId, viewport);
            lastViewport = viewport;
        }
        base.AddRenderPasses(renderer, ref renderingData);
    }
}
