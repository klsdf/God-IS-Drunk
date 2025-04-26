using UnityEngine;

public static class ResourceLoader
{
    public static AudioClip  LoadBGM()
    {
        return YanGF.Resources.LoadResource<AudioClip>("Audio/《大力王の小曲》");
    }
}