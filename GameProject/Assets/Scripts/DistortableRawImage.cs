using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class DistortableRawImage : RawImage
{
    [Header("偏移距离")]
    public float offsetAmount = 500f; // 偏移距离

    [Header("变化速度")]
    public float recoverySpeed = 10f; // 变化速度
    [Header("角落判定阈值")]
    public float inputThreshold = 0.1f; // 角落判定阈值

    [Header("四角偏移")]
    public Vector2 topLeftOffset;
    public Vector2 topRightOffset;
    public Vector2 bottomLeftOffset;
    public Vector2 bottomRightOffset;

    private Gamepad gamepad;

    private MeshCollider meshCollider;   // MeshCollider
    private Mesh mesh;  // 自定义 Mesh



    public TMP_Text tMP_Text;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Gamepad.all.Count > 0)
        {
            gamepad = Gamepad.current;
        }

        // 添加 MeshCollider 并初始化 Mesh
        meshCollider = gameObject.GetComponent<MeshCollider>();
        mesh = new Mesh();
        meshCollider.sharedMesh = mesh; // 初始化 MeshCollider 使用自定义 Mesh
        UpdateUI();
    }

    void Update()
    {
        if (gamepad == null) return;

        Vector2 leftStick = gamepad.leftStick.ReadValue();

        // 默认偏移为 Vector2.zero
        Vector2 tl = Vector2.zero;
        Vector2 tr = Vector2.zero;
        Vector2 bl = Vector2.zero;
        Vector2 br = Vector2.zero;

        // 判断摇杆方向在哪个角落
        if (leftStick.x < -inputThreshold && leftStick.y > inputThreshold)
        {
            tl = new Vector2(leftStick.x, leftStick.y) * offsetAmount;
            print("左上角");
        }
        else if (leftStick.x > inputThreshold && leftStick.y > inputThreshold)
        {
            tr = new Vector2(leftStick.x, leftStick.y) * offsetAmount;
            print("右上角");
        }
        else if (leftStick.x < -inputThreshold && leftStick.y < -inputThreshold)
        {
            bl = new Vector2(leftStick.x, leftStick.y) * offsetAmount;
            print("左下角");
        }
        else if (leftStick.x > inputThreshold && leftStick.y < -inputThreshold)
        {
            br = new Vector2(leftStick.x, leftStick.y) * offsetAmount;
            print("右下角");
        }

        // 平滑插值更新偏移量
        topLeftOffset = Vector2.Lerp(topLeftOffset, tl, Time.deltaTime * recoverySpeed);
        topRightOffset = Vector2.Lerp(topRightOffset, tr, Time.deltaTime * recoverySpeed);
        bottomLeftOffset = Vector2.Lerp(bottomLeftOffset, bl, Time.deltaTime * recoverySpeed);
        bottomRightOffset = Vector2.Lerp(bottomRightOffset, br, Time.deltaTime * recoverySpeed);



        // 更新碰撞器的顶点
        UpdateMesh();
        // 强制刷新 UI
        SetVerticesDirty();
    }


    void UpdateMesh()
    {
        // 获取 RawImage 的 RectTransform
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 获取 UI 对象四个角的世界坐标
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        // 获取四个顶点的局部位置
        Vector3 bottomLeft = rectTransform.InverseTransformPoint(worldCorners[0]);
        Vector3 topLeft = rectTransform.InverseTransformPoint(worldCorners[1]);
        Vector3 topRight = rectTransform.InverseTransformPoint(worldCorners[2]);
        Vector3 bottomRight = rectTransform.InverseTransformPoint(worldCorners[3]);

        // 使用偏移量来计算拉伸后的四个顶点位置
        bottomLeft += new Vector3(bottomLeftOffset.x, bottomLeftOffset.y, bottomLeft.z);
        topLeft += new Vector3(topLeftOffset.x, topLeftOffset.y, topLeft.z);
        topRight += new Vector3(topRightOffset.x, topRightOffset.y, topRight.z);
        bottomRight += new Vector3(bottomRightOffset.x, bottomRightOffset.y, bottomRight.z);

        // 将这些顶点传递给 Mesh
        Vector3[] vertices = new Vector3[4];
        vertices[0] = bottomLeft;
        vertices[1] = topLeft;
        vertices[2] = topRight;
        vertices[3] = bottomRight;

        // 确保顶点不重复
        if (vertices[0] == vertices[1] || vertices[0] == vertices[2] || vertices[0] == vertices[3] ||
            vertices[1] == vertices[2] || vertices[1] == vertices[3] || vertices[2] == vertices[3])
        {
            Debug.LogError("Mesh has overlapping vertices. Ensure that the corners are distinct.");
            return;
        }

        // 为 Mesh 设置顶点数据
        mesh.vertices = vertices;

        // 设置每个面的三角形索引
        int[] triangles = new int[6];
        triangles[0] = 0; triangles[1] = 1; triangles[2] = 2;  // 上三角形
        triangles[3] = 2; triangles[4] = 3; triangles[5] = 0;  // 下三角形

        mesh.triangles = triangles;

        // 更新 MeshCollider 的碰撞网格
        meshCollider.sharedMesh = mesh;
    }




    private int hp = 10;
    void OnTriggerEnter(Collider other)
    {
         print("碰撞了"+other.gameObject.name);
         hp--;
         UpdateUI();
    }


    private void  UpdateUI () {
           tMP_Text.text="HP:"+hp;
    }


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Rect rect = GetPixelAdjustedRect();

        Vector2[] corners = new Vector2[4];
        corners[0] = new Vector2(rect.xMin, rect.yMin) + bottomLeftOffset;   // Bottom Left
        corners[1] = new Vector2(rect.xMin, rect.yMax) + topLeftOffset;      // Top Left
        corners[2] = new Vector2(rect.xMax, rect.yMax) + topRightOffset;     // Top Right
        corners[3] = new Vector2(rect.xMax, rect.yMin) + bottomRightOffset;  // Bottom Right

        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;

        vert.position = corners[0];
        vert.uv0 = new Vector2(0f, 0f);
        vh.AddVert(vert);

        vert.position = corners[1];
        vert.uv0 = new Vector2(0f, 1f);
        vh.AddVert(vert);

        vert.position = corners[2];
        vert.uv0 = new Vector2(1f, 1f);
        vh.AddVert(vert);

        vert.position = corners[3];
        vert.uv0 = new Vector2(1f, 0f);
        vh.AddVert(vert);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
}
