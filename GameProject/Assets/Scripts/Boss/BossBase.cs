using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using DG.Tweening; // 引入DoTween库

public abstract class BossBase : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Vector3 startPosition = new Vector3(0.89f, -10.3f, 83);
    protected Vector3 targetPosition = new Vector3(0.89f, 4.8f, 83);

    protected bool isMoveing = false;

    protected float moveTime = 3f;


    public bool isShow = false;


    [Header("子弹的图片")]
    public List<Sprite> bulletSprites;

    [Header("Boss对话框")]
    public RectTransform BossDialogPanel;

    [Header("玩家对话框")]
    public TMP_Text BossDialogText;


    [SerializeField]
    [Header("存活时间")]
    public float survivalTime = 0f; // 存活时间

    protected Coroutine attackCoroutine;

    [SerializeField]
    public float firstAttackInterval = 3f; // 第一次攻击后的间隔时间
    [SerializeField]
    public float secondAttackInterval = 4f; // 第二次攻击后的间隔时间
    [SerializeField]
    public float thirdAttackInterval = 5f; // 第三次攻击后的间隔时间


    public bool isDead = false; // 死亡状态标志

    public Sprite deathSprite; // 死亡时的sprite



    [SerializeField]
    protected AttackMode attackMode;



    #region Debug

    public string debugStatus = "";

    public float debugTotalTime = 0f;

    #endregion


    void Start()
    {
        YanGF.Event.AddListener<RhythmType>(RhythmEvent.OnRhythm, OnRhythm);
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startPosition;


    }


    protected abstract void OnRhythm(RhythmType rhythmType);



    [Button("显示")]
    public virtual void Show()
    {
        isShow = true;
        transform.position = startPosition;

        isMoveing = true;
        YanGF.Tween.Tween(transform, t => t.position, targetPosition, moveTime, () =>
        {
            isMoveing = false;

            // 使用DoTween在Y轴上循环移动，只移动1个单位
            float moveDistance = 1f; // 移动距离
            transform.DOMoveY(targetPosition.y + moveDistance, 2f)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.InOutSine);
        });
    }



    public void Hide()
    {
        isShow = false;

        // 停止所有与transform相关的DoTween动画
        transform.DOKill();

        YanGF.Tween.Tween(transform, t => t.position, startPosition, moveTime, () =>
        {
            isMoveing = false;
        });
    }



    public Sprite GetRandomBulletSprite()
    {
        int randomIndex = Random.Range(0, bulletSprites.Count);
        return bulletSprites[randomIndex];
    }


    protected virtual void Die()
    {
        isDead = true;
        spriteRenderer.sprite = deathSprite; // 切换为死亡画面
        Hide();

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

    }
}

