using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private string zoneTag = "zone";
    [SerializeField] private string taskbarLayerName = "taskbar";
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    private RectTransform[] zoneRects;

    private void Awake()
    {
        if (!panelRectTransform) panelRectTransform = GetComponent<RectTransform>();
        var zoneObjs = GameObject.FindGameObjectsWithTag(zoneTag);
        zoneRects = new RectTransform[zoneObjs.Length];
        for (int i = 0; i < zoneObjs.Length; i++)
        {
            zoneRects[i] = zoneObjs[i].GetComponent<RectTransform>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CompareTag(taskbarLayerName)) SetAsLastSiblingExcludingTaskbar();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (!CompareTag(taskbarLayerName)) SetAsLastSiblingExcludingTaskbar();
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
        if (!panelRectTransform) return;
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelRectTransform.parent as RectTransform,
            data.position,
            data.pressEventCamera,
            out localPointerPosition
        );
        Vector3 offset = localPointerPosition - originalLocalPointerPosition;
        Vector3 newPos = originalPanelLocalPosition + offset;
        Vector3 oldPos = panelRectTransform.localPosition;
        panelRectTransform.localPosition = newPos;
        if (IsOverlappingZone()) panelRectTransform.localPosition = oldPos;
    }

    private bool IsOverlappingZone()
    {
        Bounds panelBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(
            panelRectTransform.parent,
            panelRectTransform
        );
        Rect rectA = new Rect(panelBounds.min, panelBounds.size);
        foreach (var z in zoneRects)
        {
            if (!z) continue;
            Bounds zoneBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(z.parent, z);
            Rect rectB = new Rect(zoneBounds.min, zoneBounds.size);
            if (rectA.Overlaps(rectB)) return true;
        }
        return false;
    }

    private void SetAsLastSiblingExcludingTaskbar()
    {
        Transform parentTransform = panelRectTransform.parent;
        if (!parentTransform) return;
        panelRectTransform.SetAsLastSibling();
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = parentTransform.GetChild(i);
            if (child.CompareTag(taskbarLayerName))
            {
                if (panelRectTransform.GetSiblingIndex() > i) panelRectTransform.SetSiblingIndex(i);
                return;
            }
        }
    }
}
