using UnityEngine;

public abstract class BossBase : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Vector3 startPosition = new Vector3(0.89f, -10.3f, 83);
    protected Vector3 targetPosition = new Vector3(0.89f, 4.8f, 83);

    protected bool isMoveing = false;

    protected float moveTime = 3f;


    public bool isShow = false;



    void Start()
    {
        YanGF.Event.AddListener<RhythmType>(RhythmEvent.OnRhythm, OnRhythm);
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = startPosition;
    }


    protected abstract void OnRhythm(RhythmType rhythmType);


    [ContextMenu("Show")]
    public void Show()
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



}

