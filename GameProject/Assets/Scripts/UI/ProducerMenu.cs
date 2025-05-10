using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProducerMenu : MonoBehaviour
{


    [NonSerialized]
    public CanvasGroup canvasGroup;
    public Button closeButton;

    public StartMenu startMenu;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        closeButton.onClick.AddListener(Close);

        //本地化按钮
        closeButton.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("返回");
    }

    public void Close()
    {

        float tweenTime = 1f;
        var startMenuCanvasGroup = startMenu.GetComponent<CanvasGroup>();
        YanGF.Tween.Tween(
            canvasGroup,
            cg => cg.alpha,
            0f,
            tweenTime,
            () => Debug.Log("Tween完成")
        );
        YanGF.Tween.Tween(
            startMenuCanvasGroup,
            cg => cg.alpha,
            1f,
            tweenTime,
            () => Debug.Log("Tween完成")
        );
    }
}
