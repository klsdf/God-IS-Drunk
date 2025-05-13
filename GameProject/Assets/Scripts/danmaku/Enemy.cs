using UnityEngine;
using UnityEngine.UI;
using System;





public class EnemyParameters
{
    public Material material;
    public Sprite sprite;
    public float length;
    public Action onCollision;

    public EnemyParameters(Material material, Sprite sprite, float length, Action onCollision)
    {
        this.material = material;
        this.sprite = sprite;
        this.length = length;
        this.onCollision = onCollision;
    }
}



/// <summary>
/// 所有会飞的东西，包括敌人和酒
/// </summary>
public class Enemy : MonoBehaviour
{
    // 新增变量
    // public float angle; // 当前的角度
    // public float radius = 5f; // 圆周运动的半径
    // public float rotationSpeed = 1f; // 旋转速度

    private IMovementCommand movementCommand; // 移动命令

    [Header("提示墙")]
    public GameObject notification;
    // public SpriteRenderer enemySpriteFront;
    // public SpriteRenderer enemySpriteBack;


    public Image enemyImageFront;
    private GameObject enemyImageFrontObj;
    public Image enemyImageBack;
    private GameObject enemyImageBackObj;

    public GameObject Cube;

    public float length;



    private bool hasInit = false;

    private Vector3 originalScale; // 原始缩放比例


    public Action onCollision;



    public Sprite alertEnemySprite;
    public Sprite alertWineSprite;


    public void Init(IMovementCommand movementCommand,  EnemyParameters enemyParameters, int type)
    {
        this.movementCommand = movementCommand;
        this.length = enemyParameters.length;
        notification.SetActive(false);

        // 记录原始缩放比例
        // originalScale = enemySpriteFront.transform.localScale;


        // 调整缩放比例以保持大小不变
        // AdjustSpriteScale(enemySpriteFront);
        // AdjustSpriteScale(enemySpriteBack);
        // 设置精灵
        enemyImageFront.sprite = enemyParameters.sprite;
        enemyImageBack.sprite = enemyParameters.sprite;

        enemyImageFrontObj = enemyImageFront.transform.parent.gameObject;
        enemyImageBackObj = enemyImageBack.transform.parent.gameObject;

        onCollision = enemyParameters.onCollision;



        Cube.GetComponent<MeshRenderer>().material = enemyParameters.material;
        hasInit = true;

        YanGF.Timer.SetTimeOut(() =>
        {
            if (this != null) // 检查当前对象是否为 null
            {
                EnemyCreator.Instance.DestroyItem(this);
            }
        }, 40f);

        if (type == 0)
        {
            notification.GetComponent<SpriteRenderer>().sprite = alertEnemySprite;
        }
        else
        {
            notification.GetComponent<SpriteRenderer>().sprite = alertWineSprite;
        }
    }

    // private void AdjustSpriteScale(SpriteRenderer spriteRenderer)
    // {
    //     // 计算缩放比例
    //     Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
    //     Vector3 scale = new Vector3(originalScale.x / spriteSize.x, originalScale.y / spriteSize.y, originalScale.z);
    //     spriteRenderer.transform.localScale = scale;
    // }

    void Update()
    {
        if (!hasInit)
        {
            return;
        }
        // 执行移动命令
        movementCommand?.Execute(this);

        // 检查enemySpriteFront和notification的z坐标
        if (enemyImageFrontObj.transform.position.z <= notification.transform.position.z)
        {
            notification.SetActive(false); // 隐藏notification
        }
        else
        {

            // 射线检测
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, DataConfig.raycastDistance))
            {
                // 检查是否碰到提示墙
                if (hit.collider.gameObject.tag == "提示墙")
                {
                    notification.SetActive(true);
                    // 将notification移动到射线打在提示墙的地方
                    notification.transform.position = hit.point;

                    // 计算notification透明度，距离越近透明度越高
                    float distance = hit.distance;
                    // 使用平方根函数使透明度变化在距离较近时更明显
                    float alpha = Mathf.Clamp01(1 - Mathf.Sqrt(distance / DataConfig.raycastDistance));
                    // 获取SpriteRenderer组件并设置透明度
                    SpriteRenderer spriteRenderer = notification.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        Color color = spriteRenderer.color;
                        color.a = alpha;
                        spriteRenderer.color = color;
                    }
                }
            }

        }



        Cube.transform.localScale = new Vector3(
            Cube.transform.localScale.x,
            Cube.transform.localScale.y,
            length
        );
        enemyImageFrontObj.transform.localPosition = new Vector3(0, 0, -length / 2);
        enemyImageBackObj.transform.localPosition = new Vector3(0, 0, length / 2);
    }

    // 在场景视图中绘制射线
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * DataConfig.raycastDistance);
    }
}
