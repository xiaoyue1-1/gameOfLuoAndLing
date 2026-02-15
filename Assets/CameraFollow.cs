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

    private Camera cam;
    private float camHalfWidth;
    private float camHalfHeight;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!gameBecomeing || target == null)
            return;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
        Vector3 targetCamPos = target.position;
        targetCamPos.z = -100;  

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

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    public void Clicked()
    {
        gameBecomeing = true;
    }
}