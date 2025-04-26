using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YanGameFrameWork.CoreCodes;

public class UIController : Singleton<UIController>
{

    public TMP_Text hpText;
    public Slider hpSlider;

    public TMP_Text timeText;
    public Slider timeSlider;


    public void UpdateHP(float hp, float maxHP)
    {
        hpText.text = YanGF.Localization.Translate("酒量") + ":" + hp.ToString();
        hpSlider.value = hp / maxHP;

        float hpPercentage = hp / maxHP;

        Color hpColor = Color.Lerp(Color.red, Color.green, hpPercentage);
        hpSlider.fillRect.GetComponent<Image>().color = hpColor;
    }

    public void UpdateTime(float time, float maxTime)
    {
        float percentage = (time / maxTime) * 100;
        timeText.text = YanGF.Localization.Translate("关卡进度") + ":" + percentage.ToString("F2") + "%";
        timeSlider.value = time / maxTime;
    }
}
