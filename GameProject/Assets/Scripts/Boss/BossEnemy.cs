using UnityEngine;
using System.Collections;

public class 贞子一阶段攻击 : AttackMode
{
    public override void RhythmAttack(Sprite enemySprite)
    {
        //生成正方形形状的敌人
        EnemyCreator.Instance.SpawnEnemy(
            enemySprite: enemySprite,
            spawnMode: new SpawnInSquare(),
            spawnParameters: new SpawnInSquareParameters(
                sideLength: 5f,
                enemyCount: 100,
                squareOffsetX: 4f,
                squareOffsetY: 4f
            ),
            movementCommand: new ZMovementCommand(zSpeed: 10f));
    }

    public override void FirstAttack(Sprite enemySprite)
    {
        // 实现一阶段攻击逻辑
    }
}


public class 贞子二阶段攻击 : AttackMode
{
    public override void RhythmAttack(Sprite enemySprite)
    {

        // 生成圆形形状的敌人
        EnemyCreator.Instance.SpawnEnemy(
            enemySprite: enemySprite,
            spawnMode: new SpawnEnemiesInCircle(),
            spawnParameters: new SpawnEnemiesInCircleParameters(radius: 5f, enemyCount: 20),
            movementCommand: new ZMovementCommand(zSpeed: 10f));
    }

    public override void FirstAttack(Sprite enemySprite)
    {
        // 实现二阶段攻击逻辑
    }
}




public class 贞子三阶段攻击 : AttackMode
{
    public override void RhythmAttack(Sprite enemySprite)
    {
        // 生成等边三角形形状的敌人
        EnemyCreator.Instance.SpawnEnemy(
              enemySprite: enemySprite,
              spawnMode: new SpawnEnemiesInTriangle(),
              spawnParameters: new SpawnEnemiesInTriangleParameters(sideLength: 5f, enemyCount: 20),
              movementCommand: new ZMovementCommand(zSpeed: 10f));

    }

    public override void FirstAttack(Sprite enemySprite)
    {
        // 实现三阶段攻击逻辑
    }
}





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





    override public void Show()
    {
        base.Show();

        StartCoroutine(AttackSequence());

    }

    private IEnumerator AttackSequence()
    {
        debugTotalTime = DataConfig.bossBattleTargetTime;
 
        debugStatus = "第一次对话";
        // 显示第一次攻击前的对话
        yield return StartCoroutine(FunDialogController.Instance.ShowBossDialogCoroutine(
            DialogType.贞子对话1,
            GameManager.Instance.bossEnemy.BossDialogPanel,
            GameManager.Instance.bossEnemy.BossDialogText));

        // 执行第一次攻击
        debugStatus = "第一次攻击";
        yield return StartCoroutine(FirstAttackCoroutine());

        // 第一次休息
        debugStatus = "第一次休息";
        yield return new WaitForSeconds(firstAttackInterval);

        // 显示第二次攻击前的对话
        debugStatus = "第二次对话";
        yield return StartCoroutine(FunDialogController.Instance.ShowBossDialogCoroutine(
            DialogType.贞子对话2,
            GameManager.Instance.bossEnemy.BossDialogPanel,
            GameManager.Instance.bossEnemy.BossDialogText));

        // 执行第二次攻击
        debugStatus = "第二次攻击";
        yield return StartCoroutine(SecondAttackCoroutine());

        // 第二次休息
        debugStatus = "第二次休息";
        yield return new WaitForSeconds(secondAttackInterval);

        // 显示第三次攻击前的对话
        debugStatus = "第三次对话";
        yield return StartCoroutine(FunDialogController.Instance.ShowBossDialogCoroutine(
            DialogType.贞子对话3,
            GameManager.Instance.bossEnemy.BossDialogPanel,
            GameManager.Instance.bossEnemy.BossDialogText));

        // 执行第三次攻击
        debugStatus = "第三次攻击";
        yield return StartCoroutine(ThirdAttackCoroutine());

        // 第三次休息
        debugStatus = "第三次休息";
        yield return new WaitForSeconds(thirdAttackInterval);

        // 最后的对话
        debugStatus = "第四次对话";
        yield return StartCoroutine(FunDialogController.Instance.ShowBossDialogCoroutine(
            DialogType.贞子对话4,
            GameManager.Instance.bossEnemy.BossDialogPanel,
            GameManager.Instance.bossEnemy.BossDialogText));

        Die();
    }


    private IEnumerator FirstAttackCoroutine()
    {

        float attackTimeInterval = 3f;
        attackMode = new 贞子一阶段攻击();
        SetHappyState(false);


        attackMode.FirstAttack(enemySprite: GetRandomBulletSprite());

        for (; survivalTime < DataConfig.bossBattleTargetTime / 3; survivalTime += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第一次攻击");
        }
    }

    private IEnumerator SecondAttackCoroutine()
    {

        float attackTimeInterval = 3f;
        attackMode = new 贞子二阶段攻击();
        // 实现第二次攻击逻辑


        SetHappyState(true);
        attackMode.FirstAttack(enemySprite: GetRandomBulletSprite());
        for (; survivalTime < DataConfig.bossBattleTargetTime * 2 / 3; survivalTime += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第二次攻击");
        }
    }

    private IEnumerator ThirdAttackCoroutine()
    {

        float attackTimeInterval = 3f;
        attackMode = new 贞子三阶段攻击();
        // 实现第三次攻击逻辑

        attackMode.FirstAttack(enemySprite: GetRandomBulletSprite());

        for (; survivalTime < DataConfig.bossBattleTargetTime; survivalTime += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第三次攻击");
        }

    }


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

    override protected void Die()
    {
        base.Die();

        GameManager.Instance.OnBossBattleWin();
    }
}
