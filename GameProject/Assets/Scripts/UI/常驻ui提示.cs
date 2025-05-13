
using TMPro;
using UnityEngine;
public class 常驻ui提示 : MonoBehaviour
{
    public TMP_Text text;

    private void Start()
    {
        text.text = YanGF.Localization.Translate("本游戏不提倡酒后驾车、抽烟等不良行为。游戏内容请勿模仿。");
    }
}
