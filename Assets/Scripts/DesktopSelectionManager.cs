using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopSelectionManager : MonoBehaviour
{
    public Camera uiCamera;
    public RectTransform selectionBox;
    public List<RectTransform> iconList;
    public GameObject desktopLayer;
    public List<GameObject> folderObjects;
    public float doubleClickThreshold = 0.3f;

    private bool isSelecting = false;
    private Vector2 startPos;
    private Vector2 endPos;
    private HashSet<RectTransform> selectedIcons = new HashSet<RectTransform>();

    void Start()
    {
        if (selectionBox != null)
        {
            selectionBox.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!IsMouseOnDesktopLayer() && !isSelecting)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsMouseOnDesktopLayer() || IsMouseOnFolderObject())
            {
                return;
            }

            ClearAllSelections();
            isSelecting = true;
            startPos = GetMousePositionInCanvas();
            if (selectionBox != null)
                selectionBox.gameObject.SetActive(true);
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

        if (desktopLayer != null)
        {
            int desktopIndex = desktopLayer.transform.GetSiblingIndex();
            selectionBox.transform.SetSiblingIndex(desktopIndex + 1);
        }
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

    private bool IsMouseOnDesktopLayer()
    {
        return desktopLayer.CompareTag("Desktop") && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)desktopLayer.transform, Input.mousePosition, uiCamera);
    }

    private bool IsMouseOnFolderObject()
    {
        foreach (var folder in folderObjects)
        {
            if (folder.activeInHierarchy && RectTransformUtility.RectangleContainsScreenPoint((RectTransform)folder.transform, Input.mousePosition, uiCamera))
            {
                return true;
            }
        }
        return false;
    }
}
