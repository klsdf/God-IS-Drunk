using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YanGameFrameWork.UISystem;
using DG.Tweening;

public class GameWinPanel : UIPanelBase
{
    public TMP_Text title;

    [Header("重开按钮")]

    public Button btnRestart;


    [Header("退出按钮")]
    public Button btnExit;

    public Image winImage;

    public RectTransform restartButtonRectTransform;


    public RectTransform exitButtonRectTransform;

    public RectTransform titleRectTransform;

    public override void ChildStart()
    {
        base.ChildStart();
        btnRestart.onClick.AddListener(Restart);
        btnExit.onClick.AddListener(Exit);
    }

    public override void OnLocalize()
    {
        title.text = YanGF.Localization.Translate("游戏成功了喵");
        btnRestart.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("重开游戏");
        btnExit.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("退出游戏");
    }


    public override void OnEnter()
    {
        base.OnEnter();
        title.text = YanGF.Localization.Translate("游戏成功了喵");



        // 设置初始位置
        restartButtonRectTransform.anchoredPosition = new Vector2(-1040.256f, 478.6225f); ;

        restartButtonRectTransform.DOAnchorPos(new Vector2(394f, 299f), 1f).SetEase(Ease.OutQuad);



        exitButtonRectTransform.anchoredPosition = new Vector2(-425f, 331f);

        exitButtonRectTransform.DOAnchorPos(new Vector2(588, 144), 1f).SetEase(Ease.OutQuad);


        // 旋转并同时改变缩放比例
        titleRectTransform.DORotate(new Vector3(0,0 ,1800 ), 2f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => titleRectTransform.rotation = Quaternion.Euler(0, 0, 0));

        // 从0缩放到1
        titleRectTransform.localScale = Vector3.zero; // 初始缩放为0
        titleRectTransform.DOScale(Vector3.one, 2f).SetEase(Ease.OutQuad);

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