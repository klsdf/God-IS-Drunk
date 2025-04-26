using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameFeelController : MonoBehaviour
{
    private float timer = 0f; // 计时器变量
    public float rhythmInterval = 2f; // 日志间隔时间


    void Update()
    {
        timer += Time.deltaTime; // 增加计时器

        if (timer >= rhythmInterval)
        {
            Debug.Log("每隔2秒记录一次日志。"); // 记录日志
            YanGF.Event.TriggerEvent(GameEventType.OnGameRhythm.ToString());
            timer = 0f; // 重置计时器
        }
    }


}
