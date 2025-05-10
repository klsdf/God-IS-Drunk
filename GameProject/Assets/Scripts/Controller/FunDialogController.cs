using YanGameFrameWork.Singleton;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YanGameFrameWork.DialogSystem;
using System.Collections;

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



    DialogCharacter 左 = new DialogCharacter("左");
    DialogCharacter center = new DialogCharacter("中");
    DialogCharacter right = new DialogCharacter("右");
    DialogCharacter 打工人 = new DialogCharacter("神秘打工人");
    void Start()
    {

        DialogBlock dialogBlock = new DialogBlock(DialogType.GameStart.ToString(), new List<Dialog>
        {
            new Dialog("喂，哥们，你会开车吗？", 左),
            new Dialog("没错，就你是。屏幕前这个。", right),
            new Dialog("哥们你会开车吗？", 左),
            new Dialog("开车其实很简单的，我们来给你简单讲讲。", 左),
            new Dialog("点击鼠标左键可以播放下一句对话",center),
            new Dialog("什么？你早就知道了？",center),
            new Dialog("可以的兄弟，你比我们想象的还要牛逼。",right),

            new Dialog("这个条是酒量条，。", right,()=>{
                UIController.Instance.ShowHPSlider();
            }),
            new Dialog("但是我们不是那种随处可见的游戏，你懂吧？", right),
            new Dialog("所以这个条实际上是酒量条，", 左),



            new Dialog("这个条是关卡进度条，", right,()=>{
                UIController.Instance.ShowTimeSlider();
            }),

            new Dialog("满了，就说明咱到达终点了。游戏就胜利了。", right),


            new Dialog("这个条是Fever，也就是fever条。一旦满了，哥们几个就要起飞了。", 左,()=>{
                UIController.Instance.ShowFeverImage();
            }),



            new Dialog("这个是暂停按钮，要是你玩累了，可以按一下。", right,()=>{
                UIController.Instance.ShowPauseButton();
            }),

            new Dialog("好了，哥们，就靠你了。", right),


        });

        DialogBlock dialogBlock2 = new DialogBlock(DialogType.OnCollisionEnemy.ToString(), new List<Dialog>
        {
            new Dialog("卧槽，小心点开车啊！", 左),
            new Dialog("喂喂喂，悠着点哥们！", center),
            new Dialog("兄弟，你行不行啊！", right),
            new Dialog("老哥，你驾照是从海绵宝宝那里考的吗？", 左),
            new Dialog("小心点哥们，别撞到我了！", center),
            new Dialog("老哥，你驾照是从海绵宝宝那里考的吗？", 左),
            new Dialog("小心点哥们，别撞到我了！", center),
        });

        DialogBlock dialogBlock3 = new DialogBlock(DialogType.OnDrink.ToString(), new List<Dialog>
        {

            new Dialog("前面是什么情况啊", 左),
            new Dialog("怎么突然这么多人", center),
            new Dialog("不知道啊，也许是什么派对？", right),
            new Dialog("喂，你们几个", 打工人),
            new Dialog("你们是干啥的，为什么过来？", 左),


            new Dialog("你们要去哪里？为什么往核爆方向跑啊？", 打工人),
            new Dialog("我们准备去前面喝酒！", center),
            new Dialog("哈哈哈，牛逼，真是好主意！", 打工人),
            new Dialog("刚好也发工资了，我们也去喝点！", 打工人),
            new Dialog("那我们先走了！酒馆等你！", 打工人),

        });



        YanGF.Dialog.RegisterDialogBlock(dialogBlock);
        YanGF.Dialog.RegisterDialogBlock(dialogBlock2);
    }






    private void ShowDialog(Dialog dialog)
    {

        print("播放对话：" + dialog.dialog);
        _dialogPanel.gameObject.SetActive(true);
        YanGF.Dialog.StartTypingEffect(dialog, 0.05f, _dialogText);
        
        if (dialog.speaker == 左)
        {
            _dialogPanel.anchoredPosition = new Vector2(0, 85);
        }
        else if (dialog.speaker == center)
        {
            _dialogPanel.anchoredPosition = new Vector2(60, 85);
        }
        else if (dialog.speaker == right)
        {
            _dialogPanel.anchoredPosition = new Vector2(120, 85);
        }

        dialog.onPlay?.Invoke();
    }



    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         ShowOnCollisionEnemyDialog();
    //     }
    // }



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
