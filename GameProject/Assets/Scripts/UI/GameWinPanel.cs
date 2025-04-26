using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YanGameFrameWork.UISystem;


public class GameWinPanel : UIPanelBase
{
    public TMP_Text title;

    [Header("重开按钮")]

    public Button btnRestart;


    [Header("退出按钮")]
    public Button btnExit;


    private void Start()
    {
        btnRestart.onClick.AddListener(Restart);
        btnExit.onClick.AddListener(Exit);
    }


    public override void OnEnter()
    {
        base.OnEnter();
        title.text = "游戏成功了喵";
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