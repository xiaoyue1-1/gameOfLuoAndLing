using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool gameBecomeing;
    public Transform target;          // 跟随目标（角色）
    public float smoothing = 5f;       // 平滑移动速度

    // 地图边界（世界坐标），可在 Inspector 中手动设置，或通过其他方式自动获取
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
        if (gameBecomeing)
        {

            // 计算相机视口半宽和半高（世界单位）
            camHalfHeight = cam.orthographicSize;
            camHalfWidth = camHalfHeight * cam.aspect;

            // 期望的相机位置（以目标为中心，保持 z 轴固定）
            Vector3 targetCamPos = target.position;
            targetCamPos.z = -100;   // 根据你的需求设定

            // 计算相机允许移动的范围
            float minX = mapMinX + camHalfWidth;
            float maxX = mapMaxX - camHalfWidth;
            float minY = mapMinY + camHalfHeight;
            float maxY = mapMaxY - camHalfHeight;

            // 处理地图小于相机视野的情况（此时相机应固定在中间）
            if (minX > maxX)
            {
                // 水平方向无法覆盖，取中间值
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

            // 平滑移动相机
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

    public void Clicked()
    {
        gameBecomeing = true;
    }
}