using UnityEngine;

/// <summary>
/// 摄像头控制器类，用于实现摄像头跟随玩家的功能。
/// </summary>
public class GodCameraController : MonoBehaviour
{
    /// <summary>
    /// 玩家对象的引用。
    /// </summary>
    public Transform player;

    /// <summary>
    /// 摄像头与玩家之间的偏移量。
    /// </summary>
    private Vector3 offset;

    /// <summary>
    /// 玩家上次已知的位置。
    /// </summary>
    private Vector3 lastPlayerPosition;

    /// <summary>
    /// 摄像头移动的比例因子。
    /// </summary>
    private const float moveFactor = 0.5f; 

    /// <summary>
    /// 初始化摄像头的偏移量和玩家的初始位置。
    /// </summary>
    void Start()
    {
        offset = transform.position - player.position;
        lastPlayerPosition = player.position;
    }

    /// <summary>
    /// 每帧更新摄像头的位置。
    /// </summary>
    void LateUpdate()
    {

        // print("player.position:" + player.position);
        // 计算玩家的移动距离
        Vector3 playerMovement = player.position - lastPlayerPosition;

  
        Vector3 newCameraPosition = transform.position + playerMovement * moveFactor;
        transform.position = newCameraPosition;
        lastPlayerPosition = player.position;

        transform.LookAt(player.position);
        
    }
}
