using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
public abstract class BossBase : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Vector3 startPosition = new Vector3(0.89f, -10.3f, 83);
    protected Vector3 targetPosition = new Vector3(0.89f, 4.8f, 83);

    protected bool isMoveing = false;

    protected float moveTime = 3f;


    public bool isShow = false;


    [Header("子弹的图片")]
    public List<Sprite> bulletSprites;

    [Header("Boss对话框")]
    public RectTransform BossDialogPanel;

    [Header("玩家对话框")]
    public TMP_Text BossDialogText;



    void Start()
    {
        YanGF.Event.AddListener<RhythmType>(RhythmEvent.OnRhythm, OnRhythm);
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startPosition;
    }


    protected abstract void OnRhythm(RhythmType rhythmType);


    [Button("显示")]
    public  virtual void Show()
    {
        isShow = true;
        transform.position = startPosition;


        isMoveing = true;
        YanGF.Tween.Tween(transform, t => t.position, targetPosition, moveTime, () =>
        {
            isMoveing = false;
        });
    }



    public void Hide()
    {
        isShow = false;
        YanGF.Tween.Tween(transform, t => t.position, startPosition, moveTime, () =>
       {
           isMoveing = false;
       });
    }



    public Sprite GetRandomBulletSprite()
    {
        int randomIndex = Random.Range(0, bulletSprites.Count);
        return bulletSprites[randomIndex];
    }



}

