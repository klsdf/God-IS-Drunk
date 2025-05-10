using UnityEngine.UI;
using YanGameFrameWork.UISystem;
using UnityEngine;
using YanGameFrameWork.GameSetting;
using TMPro;
public class PausePanel : UIPanelBase
{
    public Button continueButton;

    public Button restartButton;

    public Button settingButton;

    public Button exitButton;
    
    private void Start() {
        continueButton.onClick.AddListener(OnContinueButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        settingButton.onClick.AddListener(OnSettingButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public override void OnLocalize()
    {
        continueButton.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("继续游戏");
        restartButton.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("重开游戏");
        settingButton.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("设置");
        exitButton.GetComponentInChildren<TMP_Text>().text = YanGF.Localization.Translate("退出游戏");
    }



    private void OnContinueButtonClick()
    {
       YanGF.UI.PopPanel<PausePanel>();
       Time.timeScale = 1;
    }

    private void OnRestartButtonClick()
    {
        
    }

    private void OnSettingButtonClick()
    {
        YanGF.UI.PushPanel<GameSettingPanel>();
    }

    private void OnExitButtonClick()
    {
       YanGF.UI.PopPanel<PausePanel>();
       Time.timeScale = 1;
    }

    public override void OnPause()
    {
        // Time.timeScale = 0;
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        Debug.Log("OnResume");
        // Time.timeScale = 1;
        gameObject.SetActive(true);
    }
}