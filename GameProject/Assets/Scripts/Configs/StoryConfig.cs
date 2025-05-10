using System.Collections.Generic;
using YanGameFrameWork.DialogSystem;

public static class StoryConfig
{


    public static DialogCharacter 左 = new DialogCharacter("左");
    public static DialogCharacter center = new DialogCharacter("中");
    public static DialogCharacter right = new DialogCharacter("右");
    public static DialogCharacter 打工人 = new DialogCharacter("神秘打工人");


    public static DialogBlock startDialogBlock= new DialogBlock(DialogType.GameStart.ToString(), new List<Dialog>
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

    public static DialogBlock collisionEnemyDialogBlock = new DialogBlock(DialogType.OnCollisionEnemy.ToString(), new List<Dialog>
    {
        new Dialog("卧槽，小心点开车啊！", 左),
        new Dialog("喂喂喂，悠着点哥们！", center),
        new Dialog("兄弟，你行不行啊！", right),
        new Dialog("老哥，你驾照是从海绵宝宝那里考的吗？", 左),
            new Dialog("小心点哥们，别撞到我了！", center),
            new Dialog("老哥，你驾照是从海绵宝宝那里考的吗？", 左),
            new Dialog("小心点哥们，别撞到我了！", center),
        });

    public static DialogBlock drinkDialogBlock = new DialogBlock(DialogType.OnDrink.ToString(), new List<Dialog>
    {

            new Dialog("卧槽，是酒！", 左),
            new Dialog("兄弟们，快来喝酒！", center),
          

        });
}
