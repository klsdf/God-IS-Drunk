using UnityEngine;

public class BossEnemy : BossBase
{

    public Sprite[] sprites;
    public Sprite deathSprite; // 死亡时的sprite

    public Sprite normalSprite;// 正常状态的sprite
    public Sprite normal2Sprite;// 正常状态2的sprite

    private int currentSpriteIndex = 0;
    private bool isDead = false; // 死亡状态标志

    private float survivalTime = 0f; // 存活时间
    



    /// <summary>
    /// 左边的装饰
    /// </summary>
    public Transform leftDecoration;

    /// <summary>
    /// 右边的装饰
    /// </summary>
    public Transform rightDecoration;

    private float shakeAmplitude = 0.3f; // 震动幅度
    private float shakeSpeed = 15f; // 震动速度









    void Update()
    {
        if (isDead) return; // 如果已经死亡，不再更新

        survivalTime += Time.deltaTime;
        if (survivalTime >= DataConfig.smallBossBattleTargetTime)
        {
            Die(); // 超过阈值，自动死亡
        }

        // 模拟跳跃的震动效果

        if (isMoveing == false)
        {
            float shakeOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmplitude;
            Vector3 leftNewPosition = leftDecoration.localPosition + new Vector3(0, shakeOffset, 0);
            Vector3 rightNewPosition = rightDecoration.localPosition + new Vector3(0, shakeOffset, 0);
            leftDecoration.localPosition = leftNewPosition;
            rightDecoration.localPosition = rightNewPosition;
        }
    }

    protected override void OnRhythm(RhythmType rhythmType)
    {
        if (isDead) return; // 如果已经死亡，不再切换图片

        spriteRenderer.sprite = sprites[currentSpriteIndex];
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
    }

    public void Die()
    {
        isDead = true;
        spriteRenderer.sprite = deathSprite; // 切换为死亡画面
        YanGF.Tween.Tween(transform, t => t.position, startPosition, moveTime, () =>
        {
            isMoveing = false;
        });
    }
}
