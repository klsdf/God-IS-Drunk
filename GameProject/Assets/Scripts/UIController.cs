using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // 单例实例
    public static UIController Instance { get; private set; }

    public TMP_Text hpText;
    public Slider hpSlider;

    public TMP_Text timeText;
    public Slider timeSlider;

    private void Awake()
    {
        // 检查是否已经有一个实例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经有一个实例，销毁这个新的
        }
    }

    public void UpdateHP(float hp, float maxHP)
    {
        hpText.text = "酒量：" + hp.ToString();
        hpSlider.value = hp / maxHP;

        float hpPercentage = hp / maxHP;

        Color hpColor = Color.Lerp(Color.red, Color.green, hpPercentage);
        hpSlider.fillRect.GetComponent<Image>().color = hpColor;
    }

    public void UpdateTime(float time, float maxTime)
    {
        float percentage = (time / maxTime) * 100;
        timeText.text = "关卡进度：" + percentage.ToString("F2") + "%";
        timeSlider.value = time / maxTime;
    }
}
