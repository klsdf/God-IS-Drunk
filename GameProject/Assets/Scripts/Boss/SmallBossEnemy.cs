using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class 资本家一阶段攻击 : AttackMode
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


public class 资本家二阶段攻击 : AttackMode
{
    public override void RhythmAttack(Sprite enemySprite)
    {
        EnemyCreator.Instance.SpawnEnemiesWithInterval(
     enemySprite: enemySprite,
     spawnMode: new SpawnInSpiral(),
     spawnParameters: new SpawnInSpiralParameters(spiralTurns: 4, enemyCount: 150, radiusIncrement: 2f),
     movementCommand: new PlayerFollowMovementCommand(playerTransform: GameManager.Instance.playerTransform, zSpeed: 20f),
     interval: 0.1f);
    }

    public override void FirstAttack(Sprite enemySprite)
    {
        // 实现二阶段攻击逻辑
    }
}




public class SmallBossEnemy : BossBase
{


    public float 目标时间  = 0f;
    protected override void OnRhythm(RhythmType rhythmType)
    {
        if (isDead) return; // 如果已经死亡，不再切换图片
        // Debug.Log("OnRhythm");

        // attackMode.RhythmAttack();

    }

    public override void Show()
    {
        base.Show();

        StartCoroutine(AttackSequence());

        目标时间 = DataConfig.smallBossProgress * DataConfig.targetTime;
        //展示对话


    }



    private IEnumerator AttackSequence()
    {
        debugTotalTime = 目标时间;

        yield return StartCoroutine(FunDialogController.Instance.ShowBossDialogCoroutine(
            DialogType.SmallBossBattle,
            BossDialogPanel,
            BossDialogText));
        yield return StartCoroutine(FirstAttackCoroutine());
        yield return new WaitForSeconds(firstAttackInterval);
        yield return StartCoroutine(SecondAttackCoroutine());
        yield return new WaitForSeconds(secondAttackInterval);
        Die();

    }




    private IEnumerator FirstAttackCoroutine()
    {
        debugStatus = "第一次攻击";
        float attackTimeInterval = 3f;
        attackMode = new 资本家一阶段攻击();



        attackMode.FirstAttack(enemySprite: GetRandomBulletSprite());

        for (; survivalTime < 目标时间 / 2; survivalTime += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第一次攻击");
        }
        // 实现第一次攻击逻辑

        // 这里可以添加具体的攻击实现

    }

    private IEnumerator SecondAttackCoroutine()
    {
        debugStatus = "第二次攻击";
        float attackTimeInterval = 3f;
        attackMode = new 资本家二阶段攻击();
        // 实现第二次攻击逻辑
        attackMode.FirstAttack(enemySprite: GetRandomBulletSprite());
        for (; survivalTime < 目标时间; survivalTime += attackTimeInterval)
        {
            attackMode.RhythmAttack(enemySprite: GetRandomBulletSprite());
            yield return new WaitForSeconds(attackTimeInterval);
            Debug.Log("第二次攻击");
        }
    }



}



