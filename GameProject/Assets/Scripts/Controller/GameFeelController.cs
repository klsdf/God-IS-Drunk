using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeelController : MonoBehaviour
{
    private float timer = 0f; // 计时器变量
    public float logInterval = 2f; // 日志间隔时间
    public List<Transform> imagesToShake; // 需要震动的图像列表

    void Update()
    {
        timer += Time.deltaTime; // 增加计时器

        if (timer >= logInterval)
        {
            Debug.Log("每隔2秒记录一次日志。"); // 记录日志
            ShakeImages(); // 震动图像
            timer = 0f; // 重置计时器
        }
    }

    /// <summary>
    /// 震动图像的方法
    /// </summary>
    private void ShakeImages()
    {
        foreach (var image in imagesToShake)
        {
            StartCoroutine(ShakeImage(image));
        }
    }

    /// <summary>
    /// 协程：平滑缩放和恢复图像
    /// </summary>
    /// <param name="image">需要震动的图像</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator ShakeImage(Transform image)
    {
        Vector3 originalScale = image.localScale;
        Vector3 targetScale = new Vector3(originalScale.x, originalScale.y * 1.5f, originalScale.z);
        float elapsedTime = 0f;
        float duration = 0.25f; // 缩放持续时间

        // 平滑缩放到目标大小
        while (elapsedTime < duration)
        {
            image.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.localScale = targetScale;

        // 等待0.5秒
        yield return new WaitForSeconds(0.5f);

        // 平滑恢复到原始大小
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            image.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.localScale = originalScale;
    }
}
