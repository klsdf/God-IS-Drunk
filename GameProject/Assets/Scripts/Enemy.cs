using UnityEngine;

/// <summary>
/// 敌人，或者说碰到后会掉血的障碍物
/// </summary>
public class Enemy : MonoBehaviour
{       
    public Sprite sprite;
    // 新增变量
    // public float angle; // 当前的角度
    // public float radius = 5f; // 圆周运动的半径
    // public float rotationSpeed = 1f; // 旋转速度

    private IMovementCommand movementCommand; // 移动命令

    public void Init(IMovementCommand movementCommand)
    {
        this.movementCommand = movementCommand;
    }

    [Header("提示墙")]
    public GameObject notification;
    public SpriteRenderer enemySpriteFront;
    public SpriteRenderer enemySpriteBack;

    public GameObject Cube;

    public float length;


    void Start()
    {
 
        // movementCommand = new ZMovementCommand(20f);

        // 初始化弹力球运动命令
        // Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        // movementCommand = new BouncingBallMovementCommand(randomDirection, 1f, EnemyCreator.Instance);

        // 5秒后销毁敌人
        Destroy(gameObject, 40f);
        notification.SetActive(false);
        enemySpriteFront.sprite = sprite;
        enemySpriteBack.sprite = sprite;
    }

    void Update()
    {
        // 执行移动命令
        movementCommand?.Execute(this);

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

        Cube.transform.localScale = new Vector3(
            Cube.transform.localScale.x,
            Cube.transform.localScale.y,
            length
        );
        enemySpriteFront.transform.localPosition = new Vector3(0, 0, 0);
        enemySpriteBack.transform.localPosition = new Vector3(0, 0, length);
    }

    // 在场景视图中绘制射线
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * DataConfig.raycastDistance);
    }
}
