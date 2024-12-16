using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private string taskbarLayerName = "taskbar";
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;

    private void Awake()
    {
        if (panelRectTransform == null)
        {
            panelRectTransform = GetComponent<RectTransform>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("folder"))
        {
            SetAsLastSibling();
        }
        else
        {
            SetAsLastSiblingExcludingTaskbar();
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        SetAsLastSiblingExcludingTaskbar();
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

    private void SetAsLastSiblingExcludingTaskbar()
    {
        Transform parentTransform = panelRectTransform.parent;
        if (parentTransform == null)
            return;

        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = parentTransform.GetChild(i);

            if (child.CompareTag(taskbarLayerName))
            {
                panelRectTransform.SetSiblingIndex(i);
                return;
            }
        }

        panelRectTransform.SetAsLastSibling();
    }

    private void SetAsLastSibling()
    {
        panelRectTransform.SetAsLastSibling();
    }
}
