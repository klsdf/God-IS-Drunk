using System.Collections.Generic;
using YanGameFrameWork.DialogSystem;

public static class StoryConfig
{


    public static DialogCharacter left = new DialogCharacter("左");
    public static DialogCharacter center = new DialogCharacter("中");
    public static DialogCharacter right = new DialogCharacter("右");
    public static DialogCharacter 打工人 = new DialogCharacter("神秘打工人");


    public static DialogCharacter LandmineGirl = new DialogCharacter("地雷妹");
    public static DialogCharacter Capitalist = new DialogCharacter("资本家");

    public static DialogCharacter 贞子 = new DialogCharacter("贞子");


    public static DialogBlock startDialogBlock = new DialogBlock(DialogType.GameStart.ToString(), new List<Dialog>
    {
        new Dialog("嘿——嘿！三——擦三弟！你丫的终于醒啦！嗝！昨晚才喝两桶伏特加就睡过去了，没出息！来来来，快整瓶啤的漱漱口，嗯？醒、醒酒！", left),
            new Dialog("呜哇——呕——咕噜咕噜。二哥，我这，我这真不行了，眼睛都花了……你瞧瞧，那太阳，太阳怎么……嗝。怎么飞到我脸上来啦？", right),
            new Dialog("嘿嘿…你压根儿没醉！那太阳，就是，就是……嗝！就是飞到你脸上啦！嗯？你没、没听车上那破电台吗？说，太阳爆。爆炸了。哈哈哈！他妈的外星人……肯定是他们喝多啦！", left),
            new Dialog("那这是，呕。世界末日？！擦大哥，呃。那咱们，咱们这是去哪儿呢……瞧着也不像，不像是逃命的路啊？你咋…咋还朝着太阳开过去啦？", right),
            new Dialog("逃个屁。嗝！世界、世界都没了。咱们还能去哪儿？哈哈哈！",center),
            new Dialog("那大哥，我们仨酒还没。咕噜。酒还没醒呢，这是去…去哪儿呀……？",right),
            new Dialog("嗝~去四弟家里。他妈的乔治！从结婚后就再没出来……没出来喝过酒了！狗屎，这都多少年了。什么信仰、老婆、工作。去他妈的吧！",center),
            new Dialog("现在…现在世界都都他妈毁灭啦！我倒要看看。嗝…他还有什么借口不喝酒！咱们一定要…找到他…然后。嗝，把他的脑袋按在酒桶里！咱们要！要喝个天昏地暗！",center),
            new Dialog("擦大大哥！说得，说得太好啦！但咱们，咱们喝酒不能…不能开车啊",right),
            new Dialog("废……废话！大哥会不知道吗？嗝！咱们这不是，这不是在推车吗？你现在醒了。咱们，一起推！来！使劲儿！",left),
            new Dialog("但我……我推不动。咋整啊。嗝。而且我感觉，感觉好不舒服。是不是，是不是那个什么……辐射来啦？",right),
            new Dialog("放屁。你这是酒劲下来了！",center),


            new Dialog("你推不动，嗝，不会喝两口酒吗？人跟车一样。没燃料。嗝…怎么能动弹？看见，看见这个条没？这就是咱车上还剩多少好酒！", center,()=>{
                UIController.Instance.ShowHPSlider();
            }),
            new Dialog("至于服……福社是啥……啥玩意儿……？嗝！总之！喝酒，喝酒包治百病！瞧瞧。瞧瞧这些慌里慌张的人，都在往外逃命。路上满地都是好酒……随便捡！嗝！捡起来就能喝！", center),
            new Dialog("只要。只要保持住这个条，咱们就能。就能冲到终点！你们……你们要注意……嗝！可别撞到别的东西……不然我，我车上放的好酒，可就全撒啦！", center),

            new Dialog("没事，嗝……多看着点屏幕上那个导航。这个进度条会……会告诉咱们，还得跑多远。", center,()=>{
                UIController.Instance.ShowTimeSlider();
            }),

            new Dialog("满了，就说明……咱到达终点了。就能和乔治那个狗东西喝个痛快！", center),

            new Dialog("看……看看这个条！要是路上，路上看到来劲儿的酒！记得……嗝……捡起来！等条，条走满了……哥几个就能嗨得……嗝。飞起来！", left,()=>{
                UIController.Instance.ShowFeverImage();
            }),



            new Dialog("要是，要是真跑不动了……就按下这个钮。歇一歇！整口……整口淡的，继续冲！", center,()=>{
                UIController.Instance.ShowPauseButton();
            }),

            new Dialog("现在……拿上……拿上咱们的酒！抓好摩托的把——手！看到前面那个太太太……太阳了吗，冲!锋！", center),


        });

    public static DialogBlock collisionEnemyDialogBlock = new DialogBlock(DialogType.OnCollisionEnemy.ToString(), new List<Dialog>
    {
        new Dialog("大哥！你……你别晃啊！撞。撞上啦……撞上啦！", left),
        new Dialog("老三……嗝。你别左右……左右乱动！往，往前走！", center),
        new Dialog("哎呀！酒酒撒啦！二哥！我不行啦！帮……帮帮我！", right),

        });

    public static DialogBlock drinkDialogBlock = new DialogBlock(DialogType.OnDrink.ToString(), new List<Dialog>
    {

            new Dialog("好...好酒！嗝...大伙快来...快来整两口！", center),


        });

    public static DialogBlock bossDialogBlock = new DialogBlock(DialogType.EnterBossBattle.ToString(), new List<Dialog>
    {
        new Dialog("1", left),
        new Dialog("2", center),
        new Dialog("我是地雷妹", LandmineGirl),
        new Dialog("4", left),
        new Dialog("5", center),
        new Dialog("我是地雷妹", LandmineGirl),
        new Dialog("7", left),
        new Dialog("8", center),
        new Dialog("我是地雷妹", LandmineGirl),

    });

    public static DialogBlock smallBossDialogBlock = new DialogBlock(DialogType.SmallBossBattle.ToString(), new List<Dialog>
    {
        new Dialog("卧槽，这不是峰川集团的老板吗", left),
        new Dialog("经常能在电视上看到他呢", center),
        new Dialog("要不要去要个签名？", right),
        new Dialog("小家伙们，你们好吗？", Capitalist),
        new Dialog("卧槽，说话了！", left),
        new Dialog("你们想要钱吗？", Capitalist),
        new Dialog("我有很多钱，现在全都给你们吧~~", Capitalist),
    });


    public static DialogBlock smallBossRandomDialogBlock = new DialogBlock(DialogType.SmallBossRandomDialog.ToString(), new List<Dialog>
    {
        new Dialog("哈哈哈哈哈哈，我是撒币", Capitalist),
        new Dialog("来一起拿钱吧！！！", Capitalist),
        new Dialog("我最爱分享财富了", Capitalist),
        new Dialog("世界末日真好啊！！可以狠狠花钱哈哈哈哈哈哈", Capitalist),
    });



    public static DialogBlock 贞子对话1 = new DialogBlock(DialogType.贞子对话1.ToString(), new List<Dialog>
    {
        new Dialog("卧槽，前面那个是？！", left),
        new Dialog("卧槽，前面那个是贞子？！", center),
        new Dialog("喂，你们这群小鬼，", 贞子),
        new Dialog("吵吵嚷嚷什么，是没见过我吗？", 贞子),
        new Dialog("原来贞子是会说话的吗？", left),
        new Dialog("比起这个，我更在意原来鬼也抽烟啊", center),
        new Dialog("你这烟，是哪里买的？", right),
        new Dialog("这个啊，我从电视机里面爬出来后，从被害人家里顺的", 贞子),
        new Dialog("不错哦，下次给我也顺两条", right),
        new Dialog("好啊，我给你顺两条，，", 贞子),
        new Dialog("才怪啊！", 贞子),
        new Dialog("你看到忧伤的美少女居然不来安慰一下吗！", 贞子),
        new Dialog("你这人，真没情趣", 贞子),
        new Dialog("那好吧，老妹你少抽点烟，对身体不好", right),
        new Dialog("？？？", 贞子),
        new Dialog("就让你们看看我的怨念吧！", 贞子),

    });

    public static DialogBlock 贞子对话2 = new DialogBlock(DialogType.贞子对话2.ToString(), new List<Dialog>
    {
        new Dialog("老妹，你刚才仍的都是什么玩意啊", left),
        new Dialog("都是人家的化妆品哦~", 贞子),
        new Dialog("鬼要什么化妆品？", left),
        new Dialog("就是鬼才需要化妆品的啊！不然脸不够白，就吓不到人了！", 贞子),
        new Dialog("有道理", left),
        new Dialog("所以说你的怨念就是不够白吗？", center),
        new Dialog("当然不是了！", 贞子),
        new Dialog("我的怨念是尖叫啊！，尖叫！", 贞子),
        new Dialog("这年头大家恐怖电影都看太多了", 贞子),
        new Dialog("看到我钻出来都不怎么尖叫了", 贞子),
        new Dialog("反倒是天天想着把我卡到两个电视里面", 贞子),
        new Dialog("嘛，反正也世界末日了，我就跑出来享受一下世界的尖叫吧~", 贞子),
        new Dialog("话说，你有没有想过当偶像啊", left),
        new Dialog("偶像？", 贞子),
        new Dialog("就是那种，站在舞台上，被大家崇拜的偶像", left),
        new Dialog("当偶像的尖叫可比你吓人得到的声音大多了", left),
        new Dialog("欸？，人家有点害羞呢", 贞子),
        new Dialog("怕什么，一起嗨！", left),
    new Dialog("有道理，反正也世界末日了，我也下海吧！！", 贞子),
   new Dialog("这个词不应该是出道吗？", left),


    });


    public static DialogBlock 贞子对话3 = new DialogBlock(DialogType.贞子对话3.ToString(), new List<Dialog>
    {
        new Dialog("啊，太嗨了！！", 贞子),
        new Dialog("说不定我才是天生的偶像", 贞子),
        new Dialog("米娜桑，接下来是我的绝唱！", 贞子),
        new Dialog("这个词不应该是压轴戏吗，，", left),
    });

    public static DialogBlock 贞子对话4 = new DialogBlock(DialogType.贞子对话4.ToString(), new List<Dialog>
    {
  new Dialog("啊，好爽！！", 贞子),
        new Dialog("还是第一次有这么多人为我尖叫", 贞子),
        new Dialog("感觉鬼生无憾了！", 贞子),
        new Dialog("贞子老妹，爽吧！", left),
        new Dialog("鬼生短暂，就应该痛痛快快地过", left),
        new Dialog("你们之后打算干什么？", 贞子),
        new Dialog("我们要去市中心喝酒，要一起来吗？", left),
        new Dialog("来！我先去了，我在市中心等你们！", 贞子),   
        new Dialog("好嘞，到时候见吧！", left),
    });




}
