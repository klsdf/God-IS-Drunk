using YanGameFrameWork.Singleton;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FunDialogController : Singleton<FunDialogController>
{





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

    [SerializeField]
    private TMP_Text _dialogText;

    [SerializeField]
    private RectTransform _dialogPanel;


    void Start()
    {

        DialogBlock dialogBlock = new DialogBlock(DialogType.GameStart.ToString(), new List<Dialog>
        {
            new Dialog
            {
                dialog = "准备好嗨起来了嘛？",
                speaker = "左"
            },
            new Dialog
            {
                dialog = "拿好你的手柄，准备嗨起来吧！",
                speaker = "右"
            },

        });

        DialogBlock dialogBlock2 = new DialogBlock(DialogType.OnCollisionEnemy.ToString(), new List<Dialog>
        {
            new Dialog
            {
                dialog = "卧槽，小心点开车啊！",
                speaker = "左"
            },
            new Dialog
            {
                dialog = "喂喂喂，悠着点哥们！",
                speaker = "中"
            },
            new Dialog
            {
                dialog = "兄弟，你行不行啊！",
                speaker = "右"
            },
            new Dialog
            {
                dialog = "老哥，你驾照是从海绵宝宝那里考的吗？",
                speaker = "左"
            },
            new Dialog
            {
                dialog = "小心点哥们，别撞到我了！",
                speaker = "中"
            },
        });



        DialogController.Instance.RegisterDialogBlock(dialogBlock);
        DialogController.Instance.RegisterDialogBlock(dialogBlock2);
    }



    private void ShowDialog(Dialog dialog)
    {
        _dialogPanel.gameObject.SetActive(true);
        _dialogText.text = dialog.dialog;
        if (dialog.speaker == "左")
        {
            _dialogPanel.anchoredPosition = new Vector2(0, 85);
        }
        else if (dialog.speaker == "中")
        {
            _dialogPanel.anchoredPosition = new Vector2(60, 85);
        }
        else if (dialog.speaker == "右")
        {
            _dialogPanel.anchoredPosition = new Vector2(120, 85);
        }

        YanGF.Timer.SetTimeOut(() =>
        {
            CloseDialog();
        }, 2f);
    }


    public void ShowGameStartDialog()
    {

        Dialog dialog = DialogController.Instance.GetDialogBlockByName(DialogType.GameStart.ToString()).GetRandomDialog();
        ShowDialog(dialog);
    }


    public void ShowOnCollisionEnemyDialog()
    {
        Dialog dialog = DialogController.Instance.GetDialogBlockByName(DialogType.OnCollisionEnemy.ToString()).GetRandomDialog();
        ShowDialog(dialog);
    }


    public void CloseDialog()
    {
        _dialogPanel.gameObject.SetActive(false);
    }


    [ContextMenu("ShowDialog")]
    public void ShowDialog()
    {
        Dialog dialog = DialogController.Instance.GetDialogBlockByName("test").GetNextDialog();
        _dialogText.text = dialog.dialog;
    }

    [ContextMenu("ShowRandomDialog")]
    public void ShowRandomDialog()
    {
        Dialog dialog = DialogController.Instance.GetDialogBlockByName("test").GetRandomDialog();
        _dialogText.text = dialog.dialog;
    }

}
