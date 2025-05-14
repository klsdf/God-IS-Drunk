using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.green;

    void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            image.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = highlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = normalColor;
        }
    }
}
