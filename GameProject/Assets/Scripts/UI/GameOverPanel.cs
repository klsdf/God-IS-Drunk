using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YanGameFrameWork.UISystem;


public class GameOverPanel : UIPanelBase
{
    public TMP_Text title;

    public Button btn;


    public void OnInit(string info)
    {
        base.OnInit();

        title.text = info;
        btn.onClick.AddListener(Restart);
    }

    public void Restart()
    {
        YanGF.UI.PopPanel();

        
    }

}