using UnityEngine;
using YanGameFrameWork.Singleton;

public  class ResourcesController:Singleton<ResourcesController>
{
    [Header("背景音乐")]
    public AudioClip bgm;

    [Header("受击音效")]
    public AudioClip damageAudio;

    [Header("喝酒音效")]

    public AudioClip drinkAudio;

    [Header("胜利音效")]
    public AudioClip winAudio;

    [Header("失败音效")]
    public AudioClip loseAudio;
    

}