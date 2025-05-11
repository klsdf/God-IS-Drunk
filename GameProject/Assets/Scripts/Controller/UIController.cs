using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YanGameFrameWork.Singleton;
using System;

public class UIController : Singleton<UIController>
{

    public TMP_Text hpText;
    public Slider hpSlider;

    public TMP_Text timeText;
    public Image timeSlider;

    public Button pauseButton;

    public Image feverImage;
    



    [Header("关卡的名字")]
    public TMP_Text levelText;



    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClick);

        HideHPSlider();
        HideTimeSlider();
        HidePauseButton();
        HideFeverImage();
    }

    public void ShowHPSlider()
    {
        hpSlider.gameObject.SetActive(true);
    }

    public void HideHPSlider()
    {
        hpSlider.gameObject.SetActive(false);
    }

    public void ShowTimeSlider()
    {
        timeSlider.transform.parent.gameObject.SetActive(true);
    }

    public void HideTimeSlider()
    {
        timeSlider.transform.parent.gameObject.SetActive(false);
    }

    public void ShowPauseButton()
    {
        pauseButton.gameObject.SetActive(true);
    }

    public void HidePauseButton()
    {
        pauseButton.gameObject.SetActive(false);
    }


    private void OnPauseButtonClick()
    {
        YanGF.UI.PushPanel<PausePanel>();
        Time.timeScale = 0;
    }



    public void ShowFeverImage()
    {
        feverImage.gameObject.SetActive(true);
    }

    public void HideFeverImage()
    {
        feverImage.gameObject.SetActive(false);
    }


    public void UpdateFever(float fever, float maxFever)
    {
        feverImage.fillAmount = fever / maxFever;
    }


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
        timeSlider.fillAmount = time / maxTime;
    }



    public void ShowLevelText(string levelName, Action onComplete)
    {
        levelText.text = YanGF.Localization.Translate(levelName);
        YanGF.Tween.Tween(levelText, levelText => levelText.alpha, 1, 2, () =>
        {
            
            YanGF.Tween.Tween(levelText, levelText => levelText.alpha, 0, 3, onComplete);
        });
    }



}
