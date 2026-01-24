using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    void LateUpdate()
    {
        var cam = Camera.main;
        if (!cam) return;

        transform.LookAt(cam.transform);
        transform.Rotate(0f, 180f, 0f); 
    }
}
