using UnityEngine;

/// <summary>
/// 敌人，或者说碰到后会掉血的障碍物
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
    public SpriteRenderer enemySpriteFront;
    public SpriteRenderer enemySpriteBack;

    public GameObject Cube;

    public float length;



    private bool hasInit = false;

    private Vector3 originalScale; // 原始缩放比例


    public void Init(IMovementCommand movementCommand, Material material, Sprite sprite, float length)
    {
        this.movementCommand = movementCommand;
        this.length = length;
        notification.SetActive(false);

        // 记录原始缩放比例
        originalScale = enemySpriteFront.transform.localScale;

        // 设置精灵
        enemySpriteFront.sprite = sprite;
        enemySpriteBack.sprite = sprite;

        // 调整缩放比例以保持大小不变
        AdjustSpriteScale(enemySpriteFront);
        AdjustSpriteScale(enemySpriteBack);

        Cube.GetComponent<MeshRenderer>().material = material;
        hasInit = true;

        YanGF.Timer.SetTimeOut(() =>
        {
            if (this != null) // 检查当前对象是否为 null
            {
                EnemyCreator.Instance.DestroyItem(this);
            }
        }, 40f);
    }

    private void AdjustSpriteScale(SpriteRenderer spriteRenderer)
    {
        // 计算缩放比例
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector3 scale = new Vector3(originalScale.x / spriteSize.x, originalScale.y / spriteSize.y, originalScale.z);
        spriteRenderer.transform.localScale = scale;
    }

    void Update()
    {
        if (!hasInit)
        {
            return;
        }
        // 执行移动命令
        movementCommand?.Execute(this);

        // 检查enemySpriteFront和notification的z坐标
        if (enemySpriteFront.transform.position.z <= notification.transform.position.z)
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
        enemySpriteFront.transform.localPosition = new Vector3(0, 0, -length / 2);
        enemySpriteBack.transform.localPosition = new Vector3(0, 0, length / 2);
    }

    // 在场景视图中绘制射线
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * DataConfig.raycastDistance);
    }
}
