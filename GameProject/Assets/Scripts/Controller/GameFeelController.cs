using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameFeelController : MonoBehaviour
{
  
    public float rhythmInterval = 2f; // 日志间隔时间

    private void Start()
    {
        YanGF.Timer.SetInterval( () =>
        {
            YanGF.Event.TriggerEvent(GameEventType.OnGameRhythm.ToString());
        }, rhythmInterval);
    }

}
