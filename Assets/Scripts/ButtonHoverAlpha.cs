using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ButtonHoverAlpha : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup canvasGroup;
    private float originalAlpha;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalAlpha = canvasGroup.alpha;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = originalAlpha;
    }

    private void OnDisable()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = originalAlpha;
    }
}