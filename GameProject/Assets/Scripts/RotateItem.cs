using UnityEngine;
public class RotateItem : MonoBehaviour
{
    public float rotationSpeed = 10.0f; // 旋转速度

    void Update()
    {
        // 每帧更新旋转角度
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
    }


 
}