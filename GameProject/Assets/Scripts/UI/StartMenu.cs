using UnityEngine;
using UnityEngine.UI;
using YanGameFrameWork.GameSetting;
using System.Collections;

/// <summary>
/// 开始菜单类，负责处理开始菜单的交互逻辑。
/// </summary>
public class StartMenu : MonoBehaviour
{
    /// <summary>
    /// Logo按钮
    /// </summary>
    public Button logoButton;

    /// <summary>
    /// 开始按钮
    /// </summary>
    public Button startButton;

    /// <summary>
    /// 设置按钮
    /// </summary>
    public Button settingsButton;

    /// <summary>
    /// 退出按钮
    /// </summary>
    public Button exitButton;

    /// <summary>
    /// Canvas Group组件，用于控制UI透明度。
    /// </summary>
    private CanvasGroup canvasGroup;

    /// <summary>
    /// RectTransform组件，用于控制UI位置。
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// Canvas的RectTransform，用于计算边界。
    /// </summary>
    private RectTransform canvasRectTransform;

    void Start()
    {
        // 获取Canvas Group组件
        canvasGroup = GetComponent<CanvasGroup>();

        // 获取RectTransform组件
        rectTransform = GetComponent<RectTransform>();

        // 获取Canvas的RectTransform
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        logoButton.onClick.AddListener(OnLogoButtonClick);
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(Exit);
    }

    /// <summary>
    /// 开始游戏的方法，启动协程来移动UI。
    /// </summary>
    public void StartGame()
    {
        YanGF.Tween.Tween(
           canvasGroup,
           cg => cg.alpha,
           0f,
           1f,
           () => Debug.Log("Tween完成")
       );

        YanGF.Tween.MoveUIOutOfCanvas(rectTransform, canvasRectTransform, 1000f);


        GameManager.Instance.StartGame();
    }

    /// <summary>
    /// 协程方法，用于平滑移动UI到Canvas之上。
    /// </summary>
    private IEnumerator MoveUIOutOfCanvas()
    {
        Vector2 direction = new Vector2(0, 1); // 向上移动
        float speed = 1000f; // 移动速度

        while (true)
        {
            rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

            Vector2 pos = rectTransform.anchoredPosition;
            Vector2 halfSize = rectTransform.rect.size * 0.5f;
            Vector2 canvasSize = canvasRectTransform.rect.size;

            // 检查 Y 边界
            if (pos.y - halfSize.y > canvasSize.y)
            {
                pos.y = canvasSize.y + halfSize.y;
                rectTransform.anchoredPosition = pos;
                break;
            }

            rectTransform.anchoredPosition = pos;
            yield return null; // 等待下一帧
        }
    }

    /// <summary>
    /// 打开设置面板。
    /// </summary>
    public void Settings()
    {
        YanGF.UI.PushPanel<GameSettingPanel>();
    }

    /// <summary>
    /// 退出游戏。
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Logo按钮点击事件。
    /// </summary>
    private void OnLogoButtonClick()
    {

    }
}