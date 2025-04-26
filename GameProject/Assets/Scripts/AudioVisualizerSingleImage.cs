using UnityEngine;

public class AudioVisualizerSingleImage : MonoBehaviour
{
    public AudioSource audioSource;
    public Material visualizerMaterial;
    private float[] spectrumData = new float[64];

    void Start()
    {
        // 实例化材质以避免共享
        visualizerMaterial = new Material(visualizerMaterial);
    }

    void Update()
    {
        // 只有在游戏运行中才执行喵！
        if (!Application.isPlaying)
            return;

        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 使用 SetVectorArray 方法设置 _BarsData 属性
        Vector4[] barsData = new Vector4[64];
        for (int i = 0; i < 64; i++)
        {
            barsData[i] = new Vector4(spectrumData[i] * 10f, 0, 0, 0);
        }
        visualizerMaterial.SetVectorArray("_BarsData", barsData);
    }
}
