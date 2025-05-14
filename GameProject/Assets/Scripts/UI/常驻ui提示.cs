
using TMPro;
using UnityEngine;
public class 常驻ui提示 : MonoBehaviour
{
    public TMP_Text 免责文本;

    public TMP_Text 教程文本;




    private void Start()
    {
        Localize();
    }



    private void Localize()
    {
        免责文本.text = YanGF.Localization.Translate("本游戏不提倡酒后驾车、抽烟等不良行为。游戏内容请勿模仿。");
        教程文本.text = YanGF.Localization.Translate("1.使用鼠标点击ui开始游戏\n2.使用手柄的左摇杆来控制玩家移动，否则没有效果\n3.推荐使用陀螺仪来操作玩家");
    }

    private void OnEnable()
    {
        YanGF.Localization.OnLanguageChanged += OnLanguageChanged;
    }

    private void OnDisable()
    {
        YanGF.Localization.OnLanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        Localize();
    }
}
