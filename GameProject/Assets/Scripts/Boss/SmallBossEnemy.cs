using UnityEngine;
using System.Collections;

public class SmallBossEnemy : BossBase
{


    [SerializeField]
    private float survivalTime = 0f; // 存活时间

    private bool isDead = false; // 死亡状态标志


    public Sprite deathSprite; // 死亡时的sprite

    private float triggerInterval = 3f; // 触发间隔时间
    private Coroutine triggerCoroutine;

    [SerializeField]
    private float firstAttackInterval = 3f; // 第一次攻击后的间隔时间
    [SerializeField]
    private float secondAttackInterval = 4f; // 第二次攻击后的间隔时间
    [SerializeField]
    private float thirdAttackInterval = 5f; // 第三次攻击后的间隔时间

    private Coroutine attackCoroutine;


    protected override void OnRhythm(RhythmType rhythmType)
    {        
        if (isDead) return; // 如果已经死亡，不再切换图片
        Debug.Log("OnRhythm");
    }

    public override void Show()
    {
        base.Show();
        // 启动攻击协程
        attackCoroutine = StartCoroutine(AttackSequence());
    }

    void Update()
    {
        if (isDead) return; // 如果已经死亡，不再更新

        if (isShow == false) return;

        survivalTime += Time.deltaTime;
        if (survivalTime >= DataConfig.smallBossBattleTargetTime)
        {
            Die(); // 超过阈值，自动死亡
        }
    }

    private IEnumerator AttackSequence()
    {
        while (!isDead)
        {
            FirstAttack();
            yield return new WaitForSeconds(firstAttackInterval);

            SecondAttack();
            yield return new WaitForSeconds(secondAttackInterval);

            ThirdAttack();
            yield return new WaitForSeconds(thirdAttackInterval);
        }
    }

    private void FirstAttack()
    {
        // 实现第一次攻击逻辑
        Debug.Log("第一次攻击");
        // 这里可以添加具体的攻击实现
        // EnemyCreator
    }

    private void SecondAttack()
    {
        // 实现第二次攻击逻辑
        Debug.Log("第二次攻击");
        // 这里可以添加具体的攻击实现
    }

    private void ThirdAttack()
    {
        // 实现第三次攻击逻辑
        Debug.Log("第三次攻击");
        // 这里可以添加具体的攻击实现
    }

    public void Die()
    {
        isDead = true;
        spriteRenderer.sprite = deathSprite; // 切换为死亡画面
        Hide();

        // 停止协程
        if (triggerCoroutine != null)
        {
            StopCoroutine(triggerCoroutine);
        }
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }
}



