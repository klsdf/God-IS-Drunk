using UnityEngine;
using MoreMountains.Feedbacks;

/// <summary>
/// 所有可以接受节奏的物体
/// </summary>
[RequireComponent(typeof(MMF_Player))]
public class GameRhythmItem : MonoBehaviour
{

    MMF_Player mmfPlayer;
    public void Start()
    {
        mmfPlayer = GetComponent<MMF_Player>();
        YanGF.Event.AddListener(GameEventType.OnGameRhythm.ToString(), OnGameRhythm);
    }
    public void OnGameRhythm()
    {
        mmfPlayer.PlayFeedbacks();
    }
}
