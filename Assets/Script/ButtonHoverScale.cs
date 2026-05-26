using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScale :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("Scale")]
    public Vector3 normalScale =
        Vector3.one;

    public Vector3 hoverScale =
        new Vector3(1.1f, 1.1f, 1.1f);

    public void OnPointerEnter(
        PointerEventData eventData)
    {
        transform.localScale =
            hoverScale;
    }

    public void OnPointerExit(
        PointerEventData eventData)
    {
        transform.localScale =
            normalScale;
    }
}