using UnityEngine;

/// <summary>
/// 注视摄像机的物体类，使物体总是面向摄像机。
/// </summary>
public class 注视摄像机Item : MonoBehaviour
{
    /// <summary>
    /// 摄像机的Transform组件。
    /// </summary>
    public Transform cameraTransform;


    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    /// <summary>
    /// 每帧更新物体的朝向，使其面向摄像机。
    /// </summary>
    void Update()
    {
        // 确保摄像机Transform不为空
        if (cameraTransform != null)
        {
            // 使物体的朝向与摄像机的方向一致
            transform.LookAt(cameraTransform);
        }
    }
}
