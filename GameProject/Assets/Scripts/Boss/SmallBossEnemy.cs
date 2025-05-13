using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public abstract class AttackMode
{
    public abstract void RhythmAttack(Sprite enemySprite);
    public abstract void FirstAttack(Sprite enemySprite);
}

public class FirstAttackMode : AttackMode
{
    public override void RhythmAttack(Sprite enemySprite)
    {

        // 生成圆形形状的敌人
        // EnemyCreator.Instance.SpawnEnemiesInCircle(radius: 5f, enemyCount: 20);

        // 生成自定义形状的敌人
        // EnemyCreator.Instance.SpawnEnemy(
        //     enemySprite: enemySprite, 
        //     spawnMode: new SpawnCustomShape(), 
        //     spawnParameters: new SpawnCustomShapeParameters(),
        //     movementCommand: new ZMovementCommand(zSpeed: 10f));


        // 生成等边三角形形状的敌人
        //   EnemyCreator.Instance.SpawnEnemy(
        //         enemySprite: enemySprite, 
        //         spawnMode: new SpawnEnemiesInTriangle(), 
        //         spawnParameters: new SpawnEnemiesInTriangleParameters(sideLength: 5f, enemyCount: 20),
        //         movementCommand: new ZMovementCommand(zSpeed: 10f));



        // 生成正方形形状的敌人
        // EnemyCreator.Instance.SpawnEnemy(
        //     enemySprite: enemySprite, 
        //     spawnMode: new SpawnInSquare(), 
        //     spawnParameters: new SpawnInSquareParameters(
        //         sideLength: 5f,
        //         enemyCount: 100,
        //         squareOffsetX: 4f,
        //         squareOffsetY: 4f
        //     ),
        //     movementCommand: new ZMovementCommand(zSpeed: 10f));

        // 生成圆形形状的敌人
        // EnemyCreator.Instance.SpawnEnemy(
        //     enemySprite: enemySprite,
        //     spawnMode: new SpawnEnemiesInCircle(),
        //     spawnParameters: new SpawnEnemiesInCircleParameters(radius: 5f, enemyCount: 20),
        //     movementCommand: new ZMovementCommand(zSpeed: 10f));


        //   EnemyCreator.Instance.SpawnEnemy(
        //     enemySprite: enemySprite,
        //     spawnMode: new SpawnEnemiesInCircle(),
        //     spawnParameters: new SpawnEnemiesInCircleParameters(radius: 5f, enemyCount: 20),
        //     movementCommand: new PlayerFollowMovementCommand(playerTransform: GameManager.Instance.playerTransform, zSpeed: 20f));



        // 生成螺旋形状的敌人
        EnemyCreator.Instance.SpawnEnemiesWithInterval(
            enemySprite: enemySprite,
            spawnMode: new SpawnInSpiral(),
            spawnParameters: new SpawnInSpiralParameters(spiralTurns: 4, enemyCount: 150, radiusIncrement: 2f),
            movementCommand: new ZMovementCommand(zSpeed: 10f),
            interval: 0.1f);


        // 生成圆形波纹形状的敌人




    }


    public override void FirstAttack(Sprite enemySprite)
    {
        // EnemyCreator.Instance.SpawnEnemiesInSpiral(spiralTurns: 4, enemyCount: 150, radiusIncrement: 2f, spawnDelay: 0.1f);


        // EnemyCreator.Instance.SpawnEnemiesWithInterval(
        // enemySprite: enemySprite,
        // spawnMode: new SpawnInWave(),
        // spawnParameters: new SpawnInWaveParameters(waveCount: 5, enemiesPerWave: 100, radiusIncrement: 2f),
        // movementCommand: new ZMovementCommand(zSpeed: 10f),
        // interval: 0.1f);

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



        // EnemyCreator.Instance.SpawnEnemiesWithInterval(
        //          enemySprite: enemySprite,
        //          spawnMode: new SpawnInSquare(),
        //          spawnParameters: new SpawnInSquareParameters(
        //              sideLength: 5f,
        //              enemyCount: 100,
        //              squareOffsetX: 4f,
        //              squareOffsetY: 4f
        //          ),
        //          movementCommand: new ZMovementCommand(zSpeed: 10f),
        //          interval: 0.03f);



    }
}





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




    public string debugStatus = "";

    [SerializeField]
    private AttackMode attackMode = new FirstAttackMode();


    protected override void OnRhythm(RhythmType rhythmType)
    {
        if (isDead) return; // 如果已经死亡，不再切换图片
        // Debug.Log("OnRhythm");

        // attackMode.RhythmAttack();

    }

    public override void Show()
    {
        base.Show();


        //展示对话
        FunDialogController.Instance.ShowBossDialog(
            DialogType.SmallBossBattle,
            BossDialogPanel,
            BossDialogText,
            () =>
            {
                // 启动攻击协程
                attackCoroutine = StartCoroutine(AttackSequence());
            });



    }

    void Update()
    {
        if (isDead) return; // 如果已经死亡，不再更新

        if (isShow == false) return;

        survivalTime += Time.deltaTime;
        if (survivalTime >= DataConfig.smallBossHP)
        {
            Die(); // 超过阈值，自动死亡
        }
    }

    private IEnumerator AttackSequence()
    {
        while (survivalTime < DataConfig.smallBossHP)
        {
            yield return StartCoroutine(FirstAttackCoroutine());
            yield return StartCoroutine(SecondAttackCoroutine());
            yield return StartCoroutine(ThirdAttackCoroutine());
        }
    }




    private IEnumerator FirstAttackCoroutine()
    {
        debugStatus = "第一次攻击";
        float attackTimeInterval = 3f;
        attackMode = new FirstAttackMode();



        attackMode.FirstAttack(enemySprite: GetRandomBulletSprite());




        for (float t = 0; t < DataConfig.smallBossHP / 3; t += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第一次攻击");
        }
        // 实现第一次攻击逻辑

        // 这里可以添加具体的攻击实现
        yield return new WaitForSeconds(firstAttackInterval);
    }

    private IEnumerator SecondAttackCoroutine()
    {
        debugStatus = "第二次攻击";
        float attackTimeInterval = 3f;
        // 实现第二次攻击逻辑

        for (float t = 0; t < DataConfig.smallBossHP / 3; t += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第二次攻击");
        }
        // 这里可以添加具体的攻击实现
        // EnemyCreator.Instance.SpawnEnemiesInSpiral(spiralTurns: 4, enemyCount: 150, radiusIncrement: 2f, spawnDelay: 0.1f);
        yield return new WaitForSeconds(secondAttackInterval);
    }

    private IEnumerator ThirdAttackCoroutine()
    {
        debugStatus = "第三次攻击";
        float attackTimeInterval = 3f;
        // 实现第三次攻击逻辑

        for (float t = 0; t < DataConfig.smallBossHP / 3; t += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第三次攻击");
        }
        // 这里可以添加具体的攻击实现
        // EnemyCreator.Instance.SpawnEnemiesInSpiral(spiralTurns: 4, enemyCount: 150, radiusIncrement: 2f, spawnDelay: 0.1f);
        yield return new WaitForSeconds(thirdAttackInterval);
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



