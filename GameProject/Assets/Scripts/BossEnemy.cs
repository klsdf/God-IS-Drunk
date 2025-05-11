using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Sprite deathSprite; // 死亡时的sprite

    private int currentSpriteIndex = 0;
    private bool isDead = false; // 死亡状态标志

    private float survivalTime = 0f; // 存活时间
    public float survivalThreshold = 10f; // 存活时间阈值

    private Vector3 startPosition = new Vector3(0.89f, -10.3f, 83);
    private Vector3 targetPosition = new Vector3(0.89f, 4.8f, 83);
    private float moveTime = 3f;


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


    private bool isMoveing = false;

    void Start()
    {
        YanGF.Event.AddListener<RhythmType>(RhythmEvent.OnRhythm, OnRhythm);
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startPosition;
    }


    [ContextMenu("Show")]
    public void Show()
    {
        transform.position = startPosition;


        isMoveing = true;
        YanGF.Tween.Tween(transform, t => t.position, targetPosition, moveTime, () =>
        {
            isMoveing = false;
        });
    }




    void Update()
    {
        if (isDead) return; // 如果已经死亡，不再更新

        survivalTime += Time.deltaTime;
        if (survivalTime >= survivalThreshold)
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

    void OnRhythm(RhythmType rhythmType)
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
