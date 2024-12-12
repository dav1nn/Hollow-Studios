using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform panelRectTransform;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        panelRectTransform.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        panelRectTransform.SetAsLastSibling();
        originalPanelLocalPosition = panelRectTransform.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelRectTransform.parent as RectTransform,
            data.position,
            data.pressEventCamera,
            out originalLocalPointerPosition
        );
    }

    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelRectTransform.parent as RectTransform,
            data.position,
            data.pressEventCamera,
            out localPointerPosition
        );

        Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
        panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
    }
}
