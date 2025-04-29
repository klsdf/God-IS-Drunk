using YanGameFrameWork.Singleton;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FunDialogController : Singleton<FunDialogController>
{

    [SerializeField]
    private TMP_Text _dialogText;
    void Start()
    {

        List<Dialog> dialogs = new List<Dialog>
        {
            new Dialog
            {
                dialog = "别撞了！",
                speaker = "黑手"
            },
            new Dialog
            {
                dialog = "你撞到我了！",
                speaker = "玩家"
            },
            new Dialog
            {
                dialog = "怎么开车的？",
                speaker = "黑手"
            },
            new Dialog
            {
                dialog = "对不起，我下次会注意的。",
                speaker = "玩家"
            },
            new Dialog
            {
                dialog = "没关系，下次注意就好。",
                speaker = "黑手"
            },
            new Dialog
            {
                dialog = "谢谢你，下次注意。",
                speaker = "玩家"
            },
            new Dialog
            {
                dialog = "不客气，下次注意。",
                speaker = "黑手"
            },
        };

        DialogBlock dialogBlock = new DialogBlock("test", dialogs);
        DialogController.Instance.RegisterDialogBlock(dialogBlock);
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
