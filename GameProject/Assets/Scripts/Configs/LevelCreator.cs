using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 用于创建关卡的常驻生成
/// </summary>
public static class LevelCreator
{




    public static List<IMovementCommand> movementCommands;


    static LevelCreator()
    {
        movementCommands = new List<IMovementCommand>()
        {


        };
    }


    private static void SpawnRandomEnemy(Sprite chooseSprite)
    {
        int random = Random.Range(0, 5);

        switch (random)
        {
            case 0:
                //生成正常敌人
                ISpawnMode spawnMode0 = new SpawnRandomPosition();
                SpawnRandomPositionParameters spawnParameters0 = new SpawnRandomPositionParameters();
                IMovementCommand movementCommand0 = new ZMovementCommand(20f);
                EnemyCreator.Instance.SpawnEnemy(chooseSprite, spawnMode0, spawnParameters0, movementCommand0);
                break;
            case 1:

                //生成会圆形转动的方形
                ISpawnMode spawnMode1 = new SpawnInSquare();
                float sideLength = Random.Range(3f, 15f);
                SpawnInSquareParameters spawnParameters1 = new SpawnInSquareParameters(
                    sideLength: sideLength,
                    enemyCount: 100,
                    squareOffsetX: 5f,
                    squareOffsetY: 5f
                );
                IMovementCommand movementCommand1 = new ZMovementCommand(
                    zSpeed: 10f
                );
                EnemyCreator.Instance.SpawnEnemy(chooseSprite, spawnMode1, spawnParameters1, movementCommand1);
                break;
            case 2:

                //生成会碰撞的三角形
                ISpawnMode spawnMode2 = new SpawnEnemiesInTriangle();

                SpawnEnemiesInTriangleParameters spawnParameters2 = new SpawnEnemiesInTriangleParameters(
                    sideLength: 5,
                    enemyCount: 100
                );
                IMovementCommand movementCommand2 = new BouncingBallMovementCommand(
                    direction: new Vector2(1, 1),
                    enemyCreator: EnemyCreator.Instance,
                    speed: 1.5f,
                    zSpeed: 15  
                );

                break;
            case 3:

                ISpawnMode spawnMode3 = new SpawnInWave();
                SpawnInWaveParameters spawnParameters3 = new SpawnInWaveParameters(
                    waveCount: 5,
                    enemiesPerWave: 10,
                    radiusIncrement: 2f
                );
                IMovementCommand movementCommand3 = new ZMovementCommand(20f);
                EnemyCreator.Instance.SpawnEnemiesWithInterval(chooseSprite, spawnMode3, spawnParameters3, movementCommand3, 0.1f);
                break;
            case 4:

                ISpawnMode spawnMode4 = new SpawnInSquare();

                var sideLength4 = Random.Range(3f, 15f);
                SpawnInSquareParameters spawnParameters4 = new SpawnInSquareParameters(
                    sideLength: sideLength4,
                    enemyCount: 80,
                    squareOffsetX: 6f,
                    squareOffsetY: 5f
                );
                IMovementCommand movementCommand4 = new ZMovementCommand(20f);
                EnemyCreator.Instance.SpawnEnemiesWithInterval(chooseSprite, spawnMode4, spawnParameters4, movementCommand4, 0.1f);
                break;
        }

    }



    private static void SpawnRandomWine(Sprite chooseSprite)
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                //生成正常酒
                ISpawnMode spawnMode0 = new SpawnRandomPosition();
                SpawnRandomPositionParameters spawnParameters0 = new SpawnRandomPositionParameters();
                IMovementCommand movementCommand0 = new ZMovementCommand(20f);
                EnemyCreator.Instance.SpawnWine(chooseSprite, spawnMode0, spawnParameters0, movementCommand0);
                break;
            case 1:

                ISpawnMode spawnMode1 = new SpawnInSquare();
                SpawnInSquareParameters spawnParameters1 = new SpawnInSquareParameters(
                    sideLength: 5,
                    enemyCount: 100,
                    squareOffsetX: 5f,
                    squareOffsetY: 5f
                );
                IMovementCommand movementCommand1 = new ZMovementCommand(20f);
                EnemyCreator.Instance.SpawnWine(chooseSprite, spawnMode1, spawnParameters1, movementCommand1);
                break;
            case 2:

                ISpawnMode spawnMode2 = new SpawnEnemiesInTriangle();
                SpawnEnemiesInTriangleParameters spawnParameters2 = new SpawnEnemiesInTriangleParameters(
                    sideLength: 5,
                    enemyCount: 100
                );
                IMovementCommand movementCommand2 = new ZMovementCommand(20f);
                EnemyCreator.Instance.SpawnWine(chooseSprite, spawnMode2, spawnParameters2, movementCommand2);
                break;
        }

    }

    public static void SpanNoramlItems()
    {

        float random = Random.Range(0f, 1f); // 生成一个0到1之间的随机数

        // 使用enemySpawnRate作为生成敌人的概率
        if (random < EnemyCreator.Instance.enemySpawnRate)
        {
            // 获取障碍物们
            Sprite[] enemySprites = GameManager.Instance.障碍物们;
            Sprite chooseSprite = enemySprites[Random.Range(0, enemySprites.Length)];
            SpawnRandomEnemy(chooseSprite);

        }
        else
        {
            // 获取酒们
            Sprite[] enemySprites = GameManager.Instance.酒们;
            Sprite chooseSprite = enemySprites[Random.Range(0, enemySprites.Length)];

            SpawnRandomWine(chooseSprite);
        }

    }
}



