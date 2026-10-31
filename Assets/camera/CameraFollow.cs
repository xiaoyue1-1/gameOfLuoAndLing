using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool gameBecomeing;
    public Transform target;          
    public float smoothing = 5f;      


    public float mapMinX;
    public float mapMaxX;
    public float mapMinY;
    public float mapMaxY;
    [Tooltip("Use the background's world bounds as the camera tracking limits.")]
    public SpriteRenderer mapBackground;

    private Camera cam;
    private float camHalfWidth;
    private float camHalfHeight;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null || !cam.orthographic)
            return;

        if (mapBackground != null && mapBackground.sprite != null)
        {
            Bounds bounds = mapBackground.bounds;
            mapMinX = bounds.min.x;
            mapMaxX = bounds.max.x;
            mapMinY = bounds.min.y;
            mapMaxY = bounds.max.y;
            // A centered camera alone cannot cover a viewport larger than the map.
            float maximumSize = Mathf.Min(bounds.extents.y,
                bounds.extents.x / Mathf.Max(0.01f, cam.aspect));
            if (maximumSize > 0f && cam.orthographicSize > maximumSize)
                cam.orthographicSize = maximumSize;
        }

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
        if (!gameBecomeing || target == null)
        {
            if (mapBackground != null)
                transform.position = ClampToMap(transform.position);
            return;
        }
        Vector3 targetCamPos = target.position;
        targetCamPos.z = transform.position.z;
        targetCamPos = ClampToMap(targetCamPos);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetCamPos,
            1f - Mathf.Exp(-Mathf.Max(0f, smoothing) * Time.deltaTime));
        transform.position = ClampToMap(smoothedPosition);
    }

    private Vector3 ClampToMap(Vector3 targetCamPos)
    {

        float minX = mapMinX + camHalfWidth;
        float maxX = mapMaxX - camHalfWidth;
        float minY = mapMinY + camHalfHeight;
        float maxY = mapMaxY - camHalfHeight;


        if (minX > maxX)
        {
            
            targetCamPos.x = (mapMinX + mapMaxX) * 0.5f;
        }
        else
        {
            targetCamPos.x = Mathf.Clamp(targetCamPos.x, minX, maxX);
        }

        if (minY > maxY)
        {
            targetCamPos.y = (mapMinY + mapMaxY) * 0.5f;
        }
        else
        {
            targetCamPos.y = Mathf.Clamp(targetCamPos.y, minY, maxY);
        }

        return targetCamPos;
    }

    public void Clicked()
    {
        gameBecomeing = true;
    }
}
