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
        if (!CompareTag(taskbarLayerName))
        {
            SetAsLastSiblingExcludingTaskbar();
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (!CompareTag(taskbarLayerName))
        {
            SetAsLastSiblingExcludingTaskbar();
        }
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
        Rect panelRect = GetScreenRect(panelRectTransform);
        foreach (var z in zoneRects)
        {
            if (!z) continue;
            Rect zoneRect = GetScreenRect(z);
            if (panelRect.Overlaps(zoneRect)) return true;
        }
        return false;
    }

    private Rect GetScreenRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        float xMin = float.MaxValue;
        float yMin = float.MaxValue;
        float xMax = float.MinValue;
        float yMax = float.MinValue;
        for (int i = 0; i < 4; i++)
        {
            if (corners[i].x < xMin) xMin = corners[i].x;
            if (corners[i].y < yMin) yMin = corners[i].y;
            if (corners[i].x > xMax) xMax = corners[i].x;
            if (corners[i].y > yMax) yMax = corners[i].y;
        }
        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
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
                if (panelRectTransform.GetSiblingIndex() > i)
                {
                    panelRectTransform.SetSiblingIndex(i);
                }
                return;
            }
        }
    }
}
