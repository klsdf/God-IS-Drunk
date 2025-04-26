using UnityEngine;
using System;

public static class AudioController
{
    private static DateTime lastDamageAudioTime = DateTime.MinValue;
    private static DateTime lastDrinkAudioTime = DateTime.MinValue;
    private static readonly TimeSpan throttleDuration = TimeSpan.FromSeconds(1);

    public static void PlayBGM()
    {
        AudioClip audioClip = ResourcesController.Instance.bgm;
        YanGF.Audio.PlayLoop(audioClip);
    }

    public static void PlayDamageAudio()
    {
        if (DateTime.Now - lastDamageAudioTime > throttleDuration)
        {
            AudioClip audioClip = ResourcesController.Instance.damageAudio;
            YanGF.Audio.PlayOnce(audioClip);
            lastDamageAudioTime = DateTime.Now;
        }
    }

    public static void PlayDrinkAudio()
    {
        if (DateTime.Now - lastDrinkAudioTime > throttleDuration)
        {
            AudioClip audioClip = ResourcesController.Instance.drinkAudio;
            YanGF.Audio.PlayOnce(audioClip);
            lastDrinkAudioTime = DateTime.Now;
        }
    }

    public static void PlayWinAudio()
    {
        AudioClip audioClip = ResourcesController.Instance.winAudio;
        YanGF.Audio.PlayOnce(audioClip);
    }

    public static void PlayLoseAudio()
    {
        AudioClip audioClip = ResourcesController.Instance.loseAudio;
        YanGF.Audio.PlayOnce(audioClip);
    }
    
    
}