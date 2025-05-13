using UnityEngine;
using UnityEngine.Rendering.Universal;
using YanGameFrameWork.Singleton;
public class FeverController : Singleton<FeverController>
{

    #region fever相关属性
    [Header("fever阈值")]
    [Range(0.1f, 0.8f)]
    public float feverThreshold = 0.7f;

    public float FeverMax = 1000;

    [SerializeField]
    private float _fever = 0;
    public float Fever
    {
        get
        {
            return _fever;
        }
        set
        {
            _fever = value;
            UIController.Instance.UpdateFever(_fever, FeverMax);

            if (_fever >= FeverMax * feverThreshold)
            {
                YanGF.Event.TriggerEvent(GameEventType.OnFever.ToString());
            }
            else
            {
                YanGF.Event.TriggerEvent(GameEventType.OnNotFever.ToString());
            }
        }
    }


    private bool _isFever = false;
    #endregion



    #region fever修改的物体参数
    public Light2D FeverLight;


    public Transform God;
    public Transform Jupiter;

    /// <summary>
    /// 迪斯科球
    /// </summary>
    public Transform DiscoBall;


    #endregion


    private void Start()
    {
        YanGF.Event.AddListener(GameEventType.OnFever.ToString(), () =>
        {
            if (_isFever == true) return;
            OnFever();
            _isFever = true;
        });
        YanGF.Event.AddListener(GameEventType.OnNotFever.ToString(), () =>
        {
            if (_isFever == false) return;
            OnNotFever();
            _isFever = false;
        });

        YanGF.Event.AddListener<RhythmType>(RhythmEvent.OnRhythm, OnRhythm);
        HideAllFeverItem();
    }


    private void ShowAllFeverItem()
    {
        God.gameObject.SetActive(true);
        Jupiter.gameObject.SetActive(true);
        DiscoBall.gameObject.SetActive(true);
    }
    private void HideAllFeverItem()
    {
        God.gameObject.SetActive(false);
        Jupiter.gameObject.SetActive(false);
        DiscoBall.gameObject.SetActive(false);
    }


    public void LoseFever(float amount)
    {
        Fever = Mathf.Max(Fever - amount, 0);
    }

    public void GainFever(float amount)
    {
        Fever = Mathf.Min(Fever + amount, FeverMax);
    }

    void Update()
    {
        if (GameManager.Instance.IsGamePause) return;
        // Fever值每秒增加10
        Fever = Mathf.Min(_fever + 10 * Time.deltaTime, FeverMax);
    }

    // 定义彩虹色数组
    private Color[] rainbowColors = new Color[] {
        new Color(1, 0.48f, 0.48f),
        new Color(1, 0.9f, 0.48f),
        new Color(0.7f, 1f, 0.48f),
        new Color(0.48f, 1f, 0.55f),
        new Color(0.48f, 0.89f, 1f),
        new Color(0.49f, 0.48f, 1),
        new Color(0.9f, 0.48f, 1f)
    };

    // 当前颜色索引
    private int currentColorIndex = 0;

    private void OnRhythm(RhythmType rhythmType)
    {
        if (_isFever == false) return;

        // 切换灯光颜色
        FeverLight.color = rainbowColors[currentColorIndex];

        // 更新DiscoBall颜色
        DiscoBall.GetComponent<SpriteRenderer>().color = rainbowColors[currentColorIndex];

        // 更新索引
        currentColorIndex = (currentColorIndex + 1) % rainbowColors.Length;
    }

    private void OnFever()
    {
        Debug.Log("触发fever事件");

        ShowAllFeverItem();
        YanGF.Tween.Tween(God, t => t.position, new Vector3(-87, -12, 109.5f), 3);

        YanGF.Tween.Tween(Jupiter, t => t.position, new Vector3(70.2f, 46.5f, 122.4f), 3);
    }

    private void OnNotFever()
    {
        Debug.Log("触发notfever事件");
        FeverLight.color = Color.white;

        HideAllFeverItem();

        YanGF.Tween.Tween(God, t => t.position, new Vector3(-92.2f, -49.8f, 109.5f), 3);
        YanGF.Tween.Tween(Jupiter, t => t.position, new Vector3(121.9f, 52.4f, 122.4f), 3);
    }
}
