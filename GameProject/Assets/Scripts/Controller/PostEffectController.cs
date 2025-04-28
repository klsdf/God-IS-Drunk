using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using YanGameFrameWork.Singleton;

/// <summary>
/// 控制后处理效果的控制器类。
/// </summary>
public class PostEffectController : Singleton<PostEffectController>
{

    public Volume volume;

    /// <summary>
    /// Bloom 效果的引用。
    /// </summary>
    private Bloom bloom;

    /// <summary>
    /// 血量阈值，当低于此值时开启红色后处理效果。
    /// </summary>
    private const float healthThreshold = 0.5f; // 例如，30% 的血量

    /// <summary>
    /// 初始化方法，在游戏开始时调用。
    /// </summary>
    void Start()
    {
     
        volume.profile.TryGet(out bloom);
    }

    /// <summary>
    /// 设置 Bloom 效果的强度，输入范围从 0 到 1 映射到 0 到 1.5。
    /// 当血量低于一定值时，开启红色的全屏后处理效果。
    /// </summary>
    /// <param name="intensity">新的 Bloom 强度值，范围为 0 到 1。</param>
    public void SetBloomIntensity(float intensity)
    {
        // 计算真实的强度值，确保 intensity 越大，trueIntensity 越小
        float trueIntensity = intensity;
        if (bloom != null)
        {
            // 定义过渡范围
            float transitionRange = 0.1f; // 过渡范围为 10%
            float targetIntensity = 0f; // 默认强度为 0
            Color targetColor = Color.white; // 默认颜色为白色

            if (trueIntensity < healthThreshold)
            {
                // 计算过渡因子
                float t = Mathf.InverseLerp(healthThreshold - transitionRange, healthThreshold, trueIntensity);
                // 目标颜色和强度
                targetColor = Color.Lerp(Color.red, Color.white, t);
                targetIntensity = Mathf.Lerp(1.5f, trueIntensity * 1.5f, t);
            }

            // 应用平滑过渡后的颜色和强度
            bloom.tint.value = targetColor;
            bloom.intensity.value = targetIntensity;
        }
    }
}
