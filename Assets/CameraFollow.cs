using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool gameBecomeing;
    public Transform target; // 目标对象，即角色
    public float smoothing = 5.0f; // 平滑移动的速度
    
    void FixedUpdate()
    {
        if (gameBecomeing)
        {
            Vector3 targetCamPos = target.position;
            float v = (-100);
            targetCamPos.z = v;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }

    }
    public void Clicked()
    {
        gameBecomeing = true;
    }
}