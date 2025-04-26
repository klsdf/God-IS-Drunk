using UnityEngine;

public static class AudioController
{
    public static void PlayBGM()
    {
        AudioClip audioClip = ResourceLoader.LoadBGM();
        YanGF.Audio.PlayLoop(audioClip);
    }
}