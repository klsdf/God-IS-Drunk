using UnityEngine;

/// <summary>
/// 控制菜单按钮在屏幕上随机移动并在碰到边缘时反弹（针对 UI）
/// </summary>
public class MenuButton : MonoBehaviour
{
    /// <summary>按钮移动速度（单位：像素/秒）</summary>
    [SerializeField]
    private float speed = 200f;

    /// <summary>移动方向</summary>
    private Vector2 direction;

    /// <summary>按钮 RectTransform</summary>
    private RectTransform rectTransform;

    /// <summary>Canvas RectTransform</summary>
    private RectTransform canvasRectTransform;

    /// <summary>所有按钮的 RectTransform 列表</summary>
    private RectTransform[] allButtons;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRectTransform = rectTransform.root.GetComponent<Canvas>().GetComponent<RectTransform>();

        // 随机初始化方向
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // 获取所有 MenuButton 的 RectTransform
        MenuButton[] buttons = FindObjectsOfType<MenuButton>();
        allButtons = new RectTransform[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            allButtons[i] = buttons[i].GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        // 检测鼠标是否悬停在按钮上
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {
            // 如果悬停，则不移动
            return;
        }

        // 移动位置
        rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

        Vector2 pos = rectTransform.anchoredPosition;
        Vector2 halfSize = rectTransform.rect.size * 0.5f;
        Vector2 canvasHalfSize = canvasRectTransform.rect.size * 0.5f;

        // 检查 X 边界
        if (pos.x + halfSize.x > canvasHalfSize.x)
        {
            pos.x = canvasHalfSize.x - halfSize.x;
            direction.x = -direction.x;
        }
        else if (pos.x - halfSize.x < -canvasHalfSize.x)
        {
            pos.x = -canvasHalfSize.x + halfSize.x;
            direction.x = -direction.x;
        }

        // 检查 Y 边界
        if (pos.y + halfSize.y > canvasHalfSize.y)
        {
            pos.y = canvasHalfSize.y - halfSize.y;
            direction.y = -direction.y;
        }
        else if (pos.y - halfSize.y < -canvasHalfSize.y)
        {
            pos.y = -canvasHalfSize.y + halfSize.y;
            direction.y = -direction.y;
        }

        // 更新位置
        rectTransform.anchoredPosition = pos;

        // 检测与其他按钮的碰撞
        foreach (RectTransform otherRect in allButtons)
        {
            if (otherRect == rectTransform) continue;

            if (IsOverlapping(otherRect))
            {
                // 反弹逻辑
                direction = -direction;
                break;
            }
        }
    }

    /// <summary>
    /// 检查当前按钮是否与另一个按钮重叠
    /// </summary>
    /// <param name="otherRect">另一个按钮的 RectTransform</param>
    /// <returns>如果重叠则返回 true，否则返回 false</returns>
    private bool IsOverlapping(RectTransform otherRect)
    {
        Vector2 pos = rectTransform.anchoredPosition;
        Vector2 otherPos = otherRect.anchoredPosition;
        Vector2 halfSize = rectTransform.rect.size * 0.5f;
        Vector2 otherHalfSize = otherRect.rect.size * 0.5f;

        return Mathf.Abs(pos.x - otherPos.x) < (halfSize.x + otherHalfSize.x) &&
               Mathf.Abs(pos.y - otherPos.y) < (halfSize.y + otherHalfSize.y);
    }
}
