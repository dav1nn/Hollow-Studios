using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabNavigation : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current != null)
            {
                Selectable next = null;

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    next = current.GetComponent<Selectable>().FindSelectableOnUp();
                }
                else
                {
                    next = current.GetComponent<Selectable>().FindSelectableOnDown();
                }

                if (next != null)
                {
                    next.Select();
                }
            }
        }
    }
}
