using YanGameFrameWork.Singleton;
using UnityEngine;
using TMPro;
using YanGameFrameWork.DialogSystem;
using System;


/// <summary>
/// 表示不同类型对话事件的枚举。
/// </summary>
public enum DialogType
{
    /// <summary>游戏开始事件。</summary>
    GameStart,
    /// <summary>玩家被碰撞事件。</summary>
    OnCollisionEnemy,
    /// <summary>玩家喝到酒事件。</summary>
    OnDrink,
    /// <summary>玩家快要通关事件。</summary>
    NearLevelCompletion,
    /// <summary>玩家通关事件。</summary>
    LevelCompletion,
    /// <summary>玩家快要死亡事件。</summary>
    NearDeath,
    /// <summary>玩家死亡事件。</summary>
    OnDeath,
    /// <summary>进入Fever模式事件。</summary>
    EnterFeverMode,
    /// <summary>立刻进入Fever模式事件。</summary>
    InstantFeverMode,
    /// <summary>进入Boss战事件。</summary>
    EnterBossBattle,
}
public class FunDialogController : Singleton<FunDialogController>
{

    [SerializeField]
    [Header("对话框的文本")]
    private TMP_Text _dialogText;

    [SerializeField]
    [Header("对话框面板")]
    private RectTransform _dialogPanel;




    [SerializeField]
    [Header("Boss对话框的文本")]
    private TMP_Text BossDialogText;

    [SerializeField]
    [Header("Boss对话框面板")]
    private RectTransform BossDialogPanel;



    void Start()
    {

        DialogBlock dialogBlock = StoryConfig.startDialogBlock;

        DialogBlock dialogBlock2 = StoryConfig.collisionEnemyDialogBlock;

        DialogBlock dialogBlock3 = StoryConfig.drinkDialogBlock;

        DialogBlock dialogBlock4 = StoryConfig.bossDialogBlock;

        YanGF.Dialog.RegisterDialogBlock(dialogBlock);
        YanGF.Dialog.RegisterDialogBlock(dialogBlock2);
        YanGF.Dialog.RegisterDialogBlock(dialogBlock3);
        YanGF.Dialog.RegisterDialogBlock(dialogBlock4);

        _showOnCollisionEnemyDialogLimited
        = YanGF.Timer.CreateRateLimitedAction(ShowOnCollisionEnemyDialog, 3f);
    }



    private Action _showOnCollisionEnemyDialogLimited;
    /// <summary>
    /// 显示碰撞敌人对话,3秒内只显示一次
    /// </summary>
    public void ShowOnCollisionEnemyDialogLimited(){
        _showOnCollisionEnemyDialogLimited?.Invoke();
    }





    /// <summary>
    /// 显示游戏开始时的对话
    /// </summary>
    public void ShowGameStartDialog()
    {
        YanGF.Dialog.RunSequenceDialog(DialogType.GameStart.ToString(), ShowDialog, () =>
        {
              CloseDialog();
            UIController.Instance.ShowLevelText("第一章:流浪啊流浪", () =>
            {
                GameManager.Instance.ResumeGame();
              
            });
        });
    }


    [ContextMenu("ShowBossDialog")]
    public void ShowBossDialog(){
        YanGF.Dialog.RunSequenceDialog(DialogType.EnterBossBattle.ToString(),ShowDialog,()=>{
            GameManager.Instance.ResumeGame();
            CloseDialog();
        });
    }




    public void ShowOnCollisionEnemyDialog()
    {
        Dialog dialog = YanGF.Dialog.GetDialogBlockByName(DialogType.OnCollisionEnemy.ToString()).GetRandomDialog();
        ShowDialog(dialog);
        YanGF.Timer.SetTimeOut(() =>
        {
            CloseDialog();
        }, 2f);
    }


    public void CloseDialog()
    {
        _dialogPanel.gameObject.SetActive(false);
        BossDialogPanel.gameObject.SetActive(false);
    }




    private void ShowDialog(Dialog dialog)
    {

        string dialogText = YanGF.Localization.Translate(dialog.dialog);

        if(dialog.speaker == StoryConfig.地雷妹){
            BossDialogText.text = dialogText;
            BossDialogPanel.gameObject.SetActive(true);
            YanGF.Dialog.StartTypingEffect(dialogText, 0.05f, BossDialogText);
            return;
        }


        _dialogPanel.gameObject.SetActive(true);
        YanGF.Dialog.StartTypingEffect(dialogText, 0.05f, _dialogText);

        if (dialog.speaker == StoryConfig.左)
        {
            _dialogPanel.anchoredPosition = new Vector2(0, 130);
        }
        else if (dialog.speaker == StoryConfig.center)
        {
            _dialogPanel.anchoredPosition = new Vector2(60, 130);
        }
        else if (dialog.speaker == StoryConfig.right)
        {
            _dialogPanel.anchoredPosition = new Vector2(120, 130);
        }

        dialog.onPlay?.Invoke();
    }


    // [ContextMenu("ShowDialog")]
    // public void ShowDialog()
    // {
    //     Dialog dialog = DialogController.Instance.GetDialogBlockByName("test").GetNextDialog();
    //     _dialogText.text = dialog.dialog;
    //     dialog.onPlay?.Invoke();
    // }

    // [ContextMenu("ShowRandomDialog")]
    // public void ShowRandomDialog()
    // {
    //     Dialog dialog = DialogController.Instance.GetDialogBlockByName("test").GetRandomDialog();
    //     _dialogText.text = dialog.dialog;
    //     dialog.onPlay?.Invoke();
    // }



}
