using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CalendarUI : MonoBehaviour
{
    public TMP_Text dateText;
    public GameObject calendarPopup;
    public GameObject extraPanel;
    public Transform daysParent;
    public Color highlightColor;
    public Color normalColor;

    private RectTransform popupRect;
    private RectTransform dateTextRect;
    private bool isPopupOpen;

    void Start()
    {
        calendarPopup.SetActive(false);
        extraPanel.SetActive(false);
        popupRect = calendarPopup.GetComponent<RectTransform>();
        dateTextRect = dateText.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isPopupOpen && Input.GetMouseButtonDown(0))
        {
            bool clickOnPopup = RectTransformUtility.RectangleContainsScreenPoint(popupRect, Input.mousePosition, Camera.main);
            bool clickOnDateText = RectTransformUtility.RectangleContainsScreenPoint(dateTextRect, Input.mousePosition, Camera.main);
            if (!clickOnPopup && !clickOnDateText)
            {
                TogglePopup(false);
            }
        }
    }

    public void OnDateClicked()
    {
        bool newState = !isPopupOpen;
        if (newState) PopulateCalendar();
        TogglePopup(newState);
    }

    void TogglePopup(bool show)
    {
        isPopupOpen = show;
        calendarPopup.SetActive(show);
        extraPanel.SetActive(show);
    }

    void PopulateCalendar()
    {
        DateTime d = DateTime.ParseExact(dateText.text, "dd/MM/yyyy", null);
        int daysInMonth = DateTime.DaysInMonth(d.Year, d.Month);
        for (int i = 0; i < daysParent.childCount; i++)
        {
            Transform c = daysParent.GetChild(i);
            bool withinMonth = i < daysInMonth;
            c.gameObject.SetActive(withinMonth);
            if (withinMonth)
            {
                TMP_Text t = c.GetComponentInChildren<TMP_Text>();
                Image img = c.GetComponent<Image>();
                int dayNum = i + 1;
                t.text = dayNum.ToString();
                if (img) img.color = (dayNum == d.Day) ? highlightColor : normalColor;
            }
        }
    }
}




