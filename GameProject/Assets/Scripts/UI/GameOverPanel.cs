using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YanGameFrameWork.UISystem;


public class GameOverPanel : UIPanelBase
{
    public TMP_Text title;

    [Header("重开按钮")]

    public Button btnRestart;


    [Header("退出按钮")]
    public Button btnExit;


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