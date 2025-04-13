using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool invertX = false;     // 是否反转X轴
    [SerializeField] private bool invertY = false;     // 是否反转Y轴

    [Header("平滑设置")]
    [SerializeField] private float smoothSpeed = 10f;  // 平滑速度

    private Gamepad gamepad;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isControllerConnected = false;

    [Header("移动距离X")]
    public float moveDistanceX = 6.0f;    // 移动距离X
    [Header("移动距离Y")]
    public float moveDistanceY = 4.0f;    // 移动距离Y

    private void Start()
    {
        // 检查是否有手柄连接
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.current;
            isControllerConnected = true;
            Debug.Log($"检测到手柄: {gamepad.name}");

            // 保存初始位置
            initialPosition = transform.position;
        }
        else
        {
            Debug.LogWarning("未检测到手柄！");
        }
    }



    public Vector2 leftStick;
    public Vector2 rightStick;

    private void Update()
    {
        if (!isControllerConnected) return;

        // 获取手柄右摇杆数据作为移动输入
        rightStick = gamepad.rightStick.ReadValue();

        // 获取左摇杆输入（新增）
        leftStick = gamepad.leftStick.ReadValue();


        // 打印摇杆数据
        // Debug.Log($"右摇杆数据 - X: {rightStick.x:F2}, Y: {rightStick.y:F2}");

        // 计算目标位置
        Vector3 movementDirection = new Vector3(
            rightStick.x  * (invertX ? -1 : 1),
            rightStick.y  * (invertY ? -1 : 1),
            0
        ).normalized;

        // 计算目标位置，并增加x和y的距离
        targetPosition = initialPosition +  new Vector3(movementDirection.x*moveDistanceX, movementDirection.y*moveDistanceY, 0);

        // 平滑插值到目标位置
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // 打印当前位置
        // Debug.Log($"当前位置 - X: {transform.position.x:F2}, Y: {transform.position.y:F2}, Z: {transform.position.z:F2}");
    }

    // 重置位置
    public void ResetPosition()
    {
        initialPosition = transform.position;
        Debug.Log("位置已重置");
    }

    // 检查手柄连接状态
    private void OnGamepadConnected(Gamepad gamepad)
    {
        this.gamepad = gamepad;
        isControllerConnected = true;
        Debug.Log($"手柄已连接: {gamepad.name}");
    }

    private void OnGamepadDisconnected(Gamepad gamepad)
    {
        if (this.gamepad == gamepad)
        {
            isControllerConnected = false;
            Debug.LogWarning("手柄已断开连接");
        }
    }
}