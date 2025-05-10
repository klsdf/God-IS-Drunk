using YanGameFrameWork.Singleton;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YanGameFrameWork.DialogSystem;
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
}
public class FunDialogController : Singleton<FunDialogController>
{

    [SerializeField]
    private TMP_Text _dialogText;

    [SerializeField]
    private RectTransform _dialogPanel;



    void Start()
    {

        DialogBlock dialogBlock = StoryConfig.startDialogBlock;

        DialogBlock dialogBlock2 = StoryConfig.collisionEnemyDialogBlock;

        DialogBlock dialogBlock3 = StoryConfig.drinkDialogBlock;



        YanGF.Dialog.RegisterDialogBlock(dialogBlock);
        YanGF.Dialog.RegisterDialogBlock(dialogBlock2);
        YanGF.Dialog.RegisterDialogBlock(dialogBlock3);
    }








    /// <summary>
    /// 显示游戏开始时的对话
    /// </summary>
    public void ShowGameStartDialog()
    {

        YanGF.Dialog.RunSequenceDialog(DialogType.GameStart.ToString(), ShowDialog, () =>
        {
            GameManager.Instance.IsGamePause = false;
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
    }




    private void ShowDialog(Dialog dialog)
    {

        string dialogText = YanGF.Localization.Translate(dialog.dialog);
        print("播放对话：" + dialogText);
        _dialogPanel.gameObject.SetActive(true);
        YanGF.Dialog.StartTypingEffect(dialogText, 0.05f, _dialogText);

        if (dialog.speaker == StoryConfig.左)
        {
            _dialogPanel.anchoredPosition = new Vector2(0, 85);
        }
        else if (dialog.speaker == StoryConfig.center)
        {
            _dialogPanel.anchoredPosition = new Vector2(60, 85);
        }
        else if (dialog.speaker == StoryConfig.right)
        {
            _dialogPanel.anchoredPosition = new Vector2(120, 85);
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
