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

    public CircleText2 circleText2;


    public GameObject 制作人员名单图片;

    public float rotateSpeed = 30f; // 每秒旋转30度，可在Inspector调整

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        closeButton.onClick.AddListener(Close);

        //本地化按钮
        closeButton.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("返回");
    }


    void FixedUpdate()
    {
        circleText2.startAngle = (circleText2.startAngle + 1f) % 360;

        // 在本地坐标系下绕Z轴旋转
        制作人员名单图片.transform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime, Space.Self);
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
            () => canvasGroup.gameObject.SetActive(false)
        );


        startMenuCanvasGroup.gameObject.SetActive(true);
        YanGF.Tween.Tween(
            startMenuCanvasGroup,
            cg => cg.alpha,
            1f,
            tweenTime
        );
    }
}
