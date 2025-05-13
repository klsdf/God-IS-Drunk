using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YanGameFrameWork.UISystem;
using DG.Tweening;


public class GameOverPanel : UIPanelBase
{
    public TMP_Text title;

    [Header("重开按钮")]

    public Button btnRestart;


    [Header("退出按钮")]
    public Button btnExit;

    public CanvasGroup canvasGroup;


    public override void ChildStart()
    {
        base.ChildStart();
        btnRestart.onClick.AddListener(Restart);
        btnExit.onClick.AddListener(Exit);
    }

    public override void OnLocalize()
    {
        title.text = YanGF.Localization.Translate("游戏失败了喵");
        btnRestart.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("重开游戏");
        btnExit.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("退出游戏");
    }


    public override void OnEnter()
    {
        base.OnEnter();
        title.text = YanGF.Localization.Translate("游戏失败了喵");

        // 设置初始透明度为0
        canvasGroup.alpha = 0;
        // 透明度从0变为1
        canvasGroup.DOFade(1, 1f).SetEase(Ease.OutQuad);
    }

 

    public void Restart()
    {
        YanGF.UI.PopPanel();
    }

    public void Exit()
    {
        Application.Quit();

    }

}