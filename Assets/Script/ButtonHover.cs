using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Image hoverImage;

    public void OnPointerEnter(
        PointerEventData eventData)
    {
        Color c = hoverImage.color;
        c.a = 0.35f;
        hoverImage.color = c;
    }

    public void OnPointerExit(
        PointerEventData eventData)
    {
        Color c = hoverImage.color;
        c.a = 0f;
        hoverImage.color = c;
    }
}