using UnityEngine;

public class SmallBossEnemy : BossBase
{


    [SerializeField]
    private float survivalTime = 0f; // 存活时间

    private bool isDead = false; // 死亡状态标志


    public Sprite deathSprite; // 死亡时的sprite


    protected override void OnRhythm(RhythmType rhythmType)
    {        
        if (isDead) return; // 如果已经死亡，不再切换图片
        Debug.Log("OnRhythm");
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



    public void Die()
    {
        isDead = true;
        spriteRenderer.sprite = deathSprite; // 切换为死亡画面
        Hide();
    }
}



