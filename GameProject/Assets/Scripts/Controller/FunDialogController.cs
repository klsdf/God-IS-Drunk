using YanGameFrameWork.Singleton;
using UnityEngine;
using TMPro;
using YanGameFrameWork.DialogSystem;
using System;
using System.Collections;


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
    /// <summary>进入小Boss战事件。</summary>
    SmallBossBattle,
    /// <summary>小Boss的随机对话事件。</summary>
    SmallBossRandomDialog,

    贞子对话1,
    贞子对话2,
    贞子对话3
}
public class FunDialogController : Singleton<FunDialogController>
{

    [SerializeField]
    [Header("玩家对话框的文本")]
    private TMP_Text _playerDialogText;

    [SerializeField]
    [Header("玩家对话框面板")]
    private RectTransform _playerDialogPanel;






    void Start()
    {



        YanGF.Dialog.RegisterDialogBlock(StoryConfig.startDialogBlock);
        YanGF.Dialog.RegisterDialogBlock(StoryConfig.collisionEnemyDialogBlock);
        YanGF.Dialog.RegisterDialogBlock(StoryConfig.drinkDialogBlock);
        YanGF.Dialog.RegisterDialogBlock(StoryConfig.bossDialogBlock);

        YanGF.Dialog.RegisterDialogBlock(StoryConfig.smallBossDialogBlock);

        YanGF.Dialog.RegisterDialogBlock(StoryConfig.贞子对话1);
        YanGF.Dialog.RegisterDialogBlock(StoryConfig.贞子对话2);
        YanGF.Dialog.RegisterDialogBlock(StoryConfig.贞子对话3);

        _showOnCollisionEnemyDialogLimited
        = YanGF.Timer.CreateRateLimitedAction(ShowOnCollisionEnemyDialog, 3f);
    }



    private Action _showOnCollisionEnemyDialogLimited;
    /// <summary>
    /// 显示碰撞敌人对话,3秒内只显示一次
    /// </summary>
    public void ShowOnCollisionEnemyDialogLimited()
    {
        _showOnCollisionEnemyDialogLimited?.Invoke();
    }





    /// <summary>
    /// 显示游戏开始时的对话
    /// </summary>
    public void ShowGameStartDialog()
    {
        YanGF.Dialog.RunSequenceDialog(DialogType.GameStart.ToString(), ShowCharacterDialog, () =>
        {
            ClosePlayerDialog();
            UIController.Instance.ShowLevelText("第一章:流浪啊流浪", () =>
            {
                GameManager.Instance.ResumeGame();

            });
        });
    }


    /// <summary>
    /// 显示Boss对话并返回一个协程
    /// </summary>
    /// <param name="panel">boss对话框</param>
    /// <param name="dialogType">对话类型</param>
    public IEnumerator ShowBossDialogCoroutine(DialogType dialogType, RectTransform panel, TMP_Text TMPtext)
    {
        GameManager.Instance.PauseGame();
        bool dialogEnded = false;

        YanGF.Dialog.RunSequenceDialog(dialogType.ToString(), (Dialog dialog) =>
        {
            if (dialog.speaker == StoryConfig.left || dialog.speaker == StoryConfig.right || dialog.speaker == StoryConfig.center)
            {
                ShowCharacterDialog(dialog);
            }
            else
            {
                ShowBossDialog(dialog, panel, TMPtext);
            }

        }, () =>
        {
            GameManager.Instance.ResumeGame();
            CloseDialog(panel);
            dialogEnded = true;
        });

        // 等待对话结束
        while (!dialogEnded)
        {
            yield return null;
        }
    }




    /// <summary>
    /// 显示Boss的随机对话
    /// </summary>
    /// <param name="panel">boss对话框</param>
    /// <param name="dialogType">对话类型</param>
    public void ShowBossRandomDialog(DialogType dialogType, RectTransform panel, TMP_Text TMPtext)
    {
        Dialog dialog = YanGF.Dialog.GetDialogBlockByName(dialogType.ToString()).GetRandomDialog();
        ShowBossDialog(dialog, panel, TMPtext);
        YanGF.Timer.SetTimeOut(() =>
        {
            CloseDialog(panel);
        }, 2f);
    }




    /// <summary>
    /// 显示碰撞敌人对话
    /// </summary>

    public void ShowOnCollisionEnemyDialog()
    {
        Dialog dialog = YanGF.Dialog.GetDialogBlockByName(DialogType.OnCollisionEnemy.ToString()).GetRandomDialog();
        ShowCharacterDialog(dialog);
        YanGF.Timer.SetTimeOut(() =>
        {
            ClosePlayerDialog();
        }, 2f);
    }


    public void ClosePlayerDialog()
    {
        _playerDialogPanel.gameObject.SetActive(false);
    }

    public void CloseDialog(RectTransform panel)
    {
        panel.gameObject.SetActive(false);
    }





    private void ShowBossDialog(Dialog dialog, RectTransform panel, TMP_Text TMPtext)
    {
        panel.gameObject.SetActive(true);
        string dialogText = YanGF.Localization.Translate(dialog.dialog);
        YanGF.Dialog.StartTypingEffect(dialogText, 0.05f, TMPtext);
        dialog.onPlay?.Invoke();
    }



    /// <summary>
    /// 显示角色对话
    /// </summary>
    /// <param name="dialog"></param>
    private void ShowCharacterDialog(Dialog dialog)
    {

        string dialogText = YanGF.Localization.Translate(dialog.dialog);

        _playerDialogPanel.gameObject.SetActive(true);
        YanGF.Dialog.StartTypingEffect(dialogText, 0.05f, _playerDialogText);

        if (dialog.speaker == StoryConfig.left)
        {
            _playerDialogPanel.anchoredPosition = new Vector2(0, 130);
        }
        else if (dialog.speaker == StoryConfig.center)
        {
            _playerDialogPanel.anchoredPosition = new Vector2(60, 130);
        }
        else if (dialog.speaker == StoryConfig.right)
        {
            _playerDialogPanel.anchoredPosition = new Vector2(120, 130);
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
