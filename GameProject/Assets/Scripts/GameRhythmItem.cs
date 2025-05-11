using UnityEngine;
using MoreMountains.Feedbacks;

/// <summary>
/// 所有可以接受节奏的物体
/// </summary>
[RequireComponent(typeof(MMF_Player))]
public class GameRhythmItem : RhythmItemBase
{
    MMF_Player mmfPlayer;
    private void Awake()
    {
        mmfPlayer = GetComponent<MMF_Player>();
 
    }
    public override void OnRhythm(RhythmType rhythmType)
    {
        mmfPlayer.PlayFeedbacks();
    }
}
