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


    public static DialogBlock smallBossDialogBlock = new DialogBlock(DialogType.SmallBossBattle.ToString(), new List<Dialog>
    {
        new Dialog("嗝…那不是丰川集团的总裁吗？末日来了…西装革履的也在逃命…", left),
        new Dialog("哈！看他那副慌张样！西装革履的……嗝……跑得比谁都快！", center),
        new Dialog("喂！让开！我有急事！", Capitalist),
        new Dialog("一辈子都在急…嗝…现在还在急什么呢？", left),
        new Dialog("我给钱！很多钱！只要让我过去！", Capitalist),
        new Dialog("有些东西…嗝…一直都买不到的…即使你有很多钱…", center),
        new Dialog("钱啊…就像酒一样…嗝…一下子就没了…但至少酒，喝了还快乐…", right),
		
    });


    public static DialogBlock smallBossRandomDialogBlock = new DialogBlock(DialogType.SmallBossRandomDialog.ToString(), new List<Dialog>
    {
        new Dialog("我有那么多钱…为什么…为什么我也要死？", Capitalist),
        new Dialog("他们都走了…我身边的人…拿了我的钱…却不带我一起走…", Capitalist),
        new Dialog("我的飞机还在那里…发动机都热着…可飞行员已经不见了…", Capitalist),
        new Dialog("这一生…买了那么多东西…却没买到一个肯留下的人…", Capitalist),
        new Dialog("如果我们都要死…为什么不一起死呢？我要让你们给我陪葬！", Capitalist),
        new Dialog("擦大哥，末、末日来了！这老东西疯了！", left),
        new Dialog("你、你错了。他早就疯了。有些人…嗝…清醒着比醉着更可怕…", center),
    });



    public static DialogBlock 贞子对话1 = new DialogBlock(DialogType.贞子对话1.ToString(), new List<Dialog>
    {
        new Dialog("嗝…前面那黑影是啥？难道我酒喝多了？大、大哥？", left),
        new Dialog("那是……是珍子吗？我记得那是…什么都市传说来着？", right),
        new Dialog("喂，你们这群醉鬼！", 贞子),
        new Dialog("都世界末日了，你们往哪跑啊？所有人都在逃命，就你们往太阳那边去？", 贞子),
        new Dialog("我靠！她……她真的说话了？嗝。不是我的幻觉？", left),
        new Dialog("比起会说话……嗝……我更好奇她手里的。那是烟吗？", center),
        new Dialog("珍子还会抽烟？大大哥，你喝高了！", right),
        new Dialog("这个是我从电视机里爬出来的时候，看到的烟。这些人，临死前都会点一根…", 贞子),
        new Dialog("嗝…原来你是在收集…最后的回忆？", left),
        new Dialog("切，我只是无聊。世界末日了…都市传说也失业了。", 贞子),
        new Dialog("别急，你闲着也是闲着，给我也来、来一根呗？", right),
        new Dialog("想得美…我花了很多时间收集的！", 贞子),
        new Dialog("你知道吗？以前多少人因为我的录像带吓得尖叫，现在…却没人再记得我了……大家都只顾自己逃命。", 贞子),
        new Dialog("那、那当然。太阳都炸了，谁还有空怕你啊……", center),
        new Dialog("是啊。所以我一看你们不往外跑，反而朝太阳去，就觉得，有意思。", 贞子),
        new Dialog("这样。那要不要和我们一起走？嗝，去找我四弟喝酒？", left),
        new Dialog("哼！你们这是无视我吗？我可是珍子啊！没人能就这么从我面前走过去！", 贞子),
        new Dialog("嗝……妹妹，别、别挡道。我们还赶着……赶着去喝酒呢。你要是有本事，就，就吓死我们啊！", center),
    });

    public static DialogBlock 贞子对话2 = new DialogBlock(DialogType.贞子对话2.ToString(), new List<Dialog>
    {
        new Dialog("妹妹，你刚才那招挺厉害，可惜…嗝…我们这酒劲上来了，反而什么都不怕了。", left),
        new Dialog("呜…我的怨念居然对你们没用…", 贞子),
        new Dialog("那个…你丢的都是什么啊？", left),
        new Dialog("都是…都是我的化妆品啦…平时吓人用的…", 贞子),
        new Dialog("鬼还要化妆品？", left),
        new Dialog("当然要啊！不然脸不够苍白，怎么吓人啊！", 贞子),
        new Dialog("嗝…所以你变成鬼，就是为了吓人？", center),
        new Dialog("不全是…其实我生前…就很想引起别人注意…", 贞子),
        new Dialog("我整天待在电视机后面…却没人看见我…", 贞子),
        new Dialog("变成鬼后，大家总算注意我了…但他们只会害怕，从来不会欣赏我…", 贞子),
        new Dialog("这年头大家恐怖片看太多了，看到我钻出电视都不怎么尖叫了…", 贞子),
        new Dialog("反而只想着玩什么'把我卡在两个电视之间'的恶作剧…", 贞子),
        new Dialog("所以…你一直想引起别人注意，但从没人真正理解你？", left),
        new Dialog("嗯…我其实…从小…就想当偶像来着…", 贞子),
        new Dialog("偶像？", right),
        new Dialog("就是那种，站在舞台上，被很多人尖叫欢呼的…那种被崇拜的感觉…", 贞子),
        new Dialog("那你现在不就是？嗝…你吓人时，不也有很多人尖叫吗？", center),
        new Dialog("那不一样啦！我想要的是…崇拜的尖叫，不是恐惧的尖叫…", 贞子),
        new Dialog("嗝…说实话，你长得挺合适当偶像的。要不…要不你试试？", left),
        new Dialog("欸？可是…人家有点害羞呢…", 贞子),
        new Dialog("害什么羞，世界都末日了，嗝…还有什么不敢的？", left),
        new Dialog("对！就当是…就当是最后一次狂欢！跟咱们一起嗨！", right),
        new Dialog("说得对！反正世界都要完蛋了，我也要圆梦！我要，成为偶像！", 贞子),
    });



    public static DialogBlock 贞子对话3 = new DialogBlock(DialogType.贞子对话3.ToString(), new List<Dialog>
    {
        new Dialog("哇啊啊！太嗨了！原来被人欢呼的感觉这么爽！", 贞子),
        new Dialog("我怎么不早点尝试！浪费了好几十年在电视机里爬来爬去！", 贞子),
        new Dialog("米娜桑～～接下来是我的绝唱表演！", 贞子),
        new Dialog("嗝……这词不是……不是压轴戏的意思吗？", left),
        new Dialog("管他呢！反正世界都要完了！我想怎么唱就怎么唱！", 贞子),
    });

    public static DialogBlock 贞子对话4 = new DialogBlock(DialogType.贞子对话4.ToString(), new List<Dialog>
    {
        new Dialog("呼～～太爽了！从来没感觉这么活着过！虽然我已经死了……", 贞子),
        new Dialog("第一次……有人为我欢呼而不是尖叫着逃跑。", 贞子),
        new Dialog("感觉……圆满了！就算世界毁灭也值了！", 贞子),
        new Dialog("嗝...爽吧！我就说嘛！", left),
        new Dialog("人生、本来就短、短暂。活着时怕这怕那，谈过去、想、想未来，死了又有啥用？", center),
        new Dialog("就像我兄弟。嗝。结了婚就再也没、没出来痛快喝酒过。现在世界末日都来了。", left),
        new Dialog("鬼生也好，人生也罢。就得痛痛快快地过！", center),
        new Dialog("说得对！我后悔死后花那么多时间吓人了！", 贞子),
        new Dialog("你们接下来打算去哪儿？", 贞子),
        new Dialog("市中心！嗝。找我四弟喝酒！一起来吗？", center),
        new Dialog("太棒了！我先过去！在那等你们！", 贞子),   
        new Dialog("好！为了世界末日，干、干杯！", center),
    });


}
