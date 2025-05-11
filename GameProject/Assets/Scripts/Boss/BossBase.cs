using UnityEngine;

public abstract class BossBase : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;

    protected bool isMoveing = false;

    protected float moveTime = 3f;



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
        transform.position = startPosition;


        isMoveing = true;
        YanGF.Tween.Tween(transform, t => t.position, targetPosition, moveTime, () =>
        {
            isMoveing = false;
        });
    }



}

