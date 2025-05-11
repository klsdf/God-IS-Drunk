using UnityEngine;

public class BossEnemy : BossBase
{

    public Sprite[] HappySprites;

    public Sprite normalSprite;// 正常状态的sprite

    private int currentSpriteIndex = 0;

    [SerializeField]
    private bool isHappy = false; // 标记是否处于happy状态

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
        if (isShow == false) return;

        // 模拟跳跃的震动效果

        if (isMoveing == false && isHappy == true)
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
        if (!isHappy) return; // 只有在happy状态时才进行轮播

        spriteRenderer.sprite = HappySprites[currentSpriteIndex];
        currentSpriteIndex = (currentSpriteIndex + 1) % HappySprites.Length;
    }


    [ContextMenu("SetHappyState")]
    public void SetHappyState(bool happy)
    {
        isHappy = happy;
        if (!isHappy)
        {
            spriteRenderer.sprite = normalSprite; // 切换回正常状态的sprite
        }
    }
}
