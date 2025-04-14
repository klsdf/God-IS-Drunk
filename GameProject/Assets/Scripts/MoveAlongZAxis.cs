using UnityEngine;

/// <summary>
/// 沿Z轴移动
/// </summary>
public class MoveAlongZAxis : MonoBehaviour
{
    public float speed = 5.0f; // 移动速度

    void Update()
    {
        // 每帧沿世界坐标系的z轴正方向移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }
}
