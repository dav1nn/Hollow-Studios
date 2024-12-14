using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopSelectionManager : MonoBehaviour
{
    [Header("References")]
    public Camera uiCamera;
    public RectTransform selectionBox;
    public List<RectTransform> iconList;

    [Header("Double Click Settings")]
    public float doubleClickThreshold = 0.3f;

    private bool isSelecting = false;
    private Vector2 startPos;
    private Vector2 endPos;

    private HashSet<RectTransform> selectedIcons = new HashSet<RectTransform>();

    private RectTransform lastClickedIcon = null;
    private float lastClickTime = 0f;

    void Start()
    {
        if (selectionBox != null)
        {
            selectionBox.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RectTransform clickedIcon = GetIconUnderMouse();
            if (clickedIcon != null)
            {
                HandleIconClick(clickedIcon);
            }
            else
            {
                ClearAllSelections();
                isSelecting = true;
                startPos = GetMousePositionInCanvas();
                if (selectionBox != null)
                    selectionBox.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButton(0) && isSelecting)
        {
            endPos = GetMousePositionInCanvas();
            UpdateSelectionBoxVisual();
            UpdateIconHighlights(); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isSelecting)
            {
                isSelecting = false;
                if (selectionBox != null)
                {
                    selectionBox.gameObject.SetActive(false);
                }
            }
        }
    }

    private void HandleIconClick(RectTransform icon)
    {
        float currentTime = Time.time;

        if (icon == lastClickedIcon && (currentTime - lastClickTime) < doubleClickThreshold)
        {
            if (selectedIcons.Contains(icon))
            {
                UnhighlightIcon(icon);
                selectedIcons.Remove(icon);
            }
            lastClickedIcon = null;
        }
        else
        {
            ClearAllSelections();
            HighlightIcon(icon);
            selectedIcons.Add(icon);

            lastClickedIcon = icon;
            lastClickTime = currentTime;
        }
    }

    private void ClearAllSelections()
    {
        foreach (var icon in selectedIcons)
        {
            UnhighlightIcon(icon);
        }
        selectedIcons.Clear();
    }

    private void UpdateIconHighlights()
    {
        Rect selectionRect = GetSelectionRect();

        HashSet<RectTransform> iconsInRect = new HashSet<RectTransform>();
        foreach (var icon in iconList)
        {
            if (IconIsInRect(icon, selectionRect))
            {
                iconsInRect.Add(icon);
            }
        }

        foreach (var icon in new HashSet<RectTransform>(selectedIcons))
        {
            if (!iconsInRect.Contains(icon))
            {
                UnhighlightIcon(icon);
                selectedIcons.Remove(icon);
            }
        }

        foreach (var icon in iconsInRect)
        {
            if (!selectedIcons.Contains(icon))
            {
                HighlightIcon(icon);
                selectedIcons.Add(icon);
            }
        }
    }

    private void UpdateSelectionBoxVisual()
    {
        if (selectionBox == null) return;

        Vector2 start = startPos;
        Vector2 end = endPos;

        float width = Mathf.Abs(end.x - start.x);
        float height = Mathf.Abs(end.y - start.y);
        Vector2 center = (start + end) / 2f;

        selectionBox.anchoredPosition = center;
        selectionBox.sizeDelta = new Vector2(width, height);
    }

    private Rect GetSelectionRect()
    {
        Vector2 start = startPos;
        Vector2 end = endPos;

        float xMin = Mathf.Min(start.x, end.x);
        float xMax = Mathf.Max(start.x, end.x);
        float yMin = Mathf.Min(start.y, end.y);
        float yMax = Mathf.Max(start.y, end.y);

        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }

    private bool IconIsInRect(RectTransform icon, Rect selectionRect)
    {
        Vector3[] corners = new Vector3[4];
        icon.GetWorldCorners(corners);

        RectTransform canvasRect = (RectTransform)selectionBox.parent;
        for (int i = 0; i < 4; i++)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, uiCamera.WorldToScreenPoint(corners[i]), uiCamera, out localPoint);
            corners[i] = localPoint;
        }

        float iconXMin = Mathf.Min(corners[0].x, corners[2].x);
        float iconXMax = Mathf.Max(corners[0].x, corners[2].x);
        float iconYMin = Mathf.Min(corners[0].y, corners[2].y);
        float iconYMax = Mathf.Max(corners[0].y, corners[2].y);

        return !(selectionRect.xMax < iconXMin ||
                 selectionRect.xMin > iconXMax ||
                 selectionRect.yMax < iconYMin ||
                 selectionRect.yMin > iconYMax);
    }

    private RectTransform GetIconUnderMouse()
    {
        Vector2 mousePos = GetMousePositionInCanvas();

        foreach (var icon in iconList)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(icon, Input.mousePosition, uiCamera))
            {
                return icon;
            }
        }
        return null;
    }

    private Vector2 GetMousePositionInCanvas()
    {
        Vector2 mousePos = Input.mousePosition;
        RectTransform canvasRect = selectionBox.parent as RectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePos, uiCamera, out Vector2 localPos);
        return localPos;
    }

    private void HighlightIcon(RectTransform icon)
    {
        Image img = icon.GetComponent<Image>();
        if (img != null)
        {
            img.color = Color.cyan;
        }
    }

    private void UnhighlightIcon(RectTransform icon)
    {
        Image img = icon.GetComponent<Image>();
        if (img != null)
        {
            img.color = Color.white;
        }
    }
}
