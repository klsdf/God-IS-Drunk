using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using YanGameFrameWork.CoreCodes;


/// <summary>
/// 玩家控制器，就是控制三个哥们移动的，只能xy移动，不能z移动
/// </summary>
public class PlayerController : Singleton<PlayerController>
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

    private DistortableRawImage spriteRenderer; // 用于改变颜色的SpriteRenderer
    private Color originalColor; // 原始颜色
    private float damageFlashDuration = 0.25f; // 受伤时颜色变化的持续时间

    private bool isFlashing = false; // 是否正在闪烁


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

        // 获取SpriteRenderer组件
        spriteRenderer = GetComponent<DistortableRawImage>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // 保存原始颜色
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

        // 计算目标位置
        Vector3 movementDirection = new Vector3(
            rightStick.x * (invertX ? -1 : 1),
            rightStick.y * (invertY ? -1 : 1),
            0
        ).normalized;

        // 计算目标位置，并增加x和y的距离
        targetPosition = initialPosition + new Vector3(movementDirection.x * moveDistanceX, movementDirection.y * moveDistanceY, 0);

        // 平滑插值到目标位置
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    // 受伤时调用此方法
    public void TakeDamage()
    {
        // 如果spriteRenderer不为空且当前没有在闪烁
        if (spriteRenderer != null && !isFlashing)
        {
            StartCoroutine(FlashRed());
        }
    }


    public void GainHP()
    {
        StartCoroutine(FlashGreen());
    }

    

    // 颜色闪烁协程
    private IEnumerator FlashRed()
    {
        isFlashing = true; // 设置为正在闪烁
        spriteRenderer.color = Color.red; // 变为红色
        yield return new WaitForSeconds(damageFlashDuration); // 等待一段时间
        spriteRenderer.color = originalColor; // 恢复原始颜色
        isFlashing = false; // 闪烁结束
    }

    private IEnumerator FlashGreen()
    {
        isFlashing = true; // 设置为正在闪烁
        spriteRenderer.color = Color.green; // 变为绿色
        yield return new WaitForSeconds(damageFlashDuration); // 等待一段时间
        spriteRenderer.color = originalColor; // 恢复原始颜色
        isFlashing = false; // 闪烁结束
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