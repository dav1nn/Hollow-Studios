using UnityEngine;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationObject;

    void Start()
    {
        if (notificationObject) notificationObject.SetActive(false);
        StartCoroutine(NotificationRoutine());
    }

    IEnumerator NotificationRoutine()
    {
        yield return new WaitForSeconds(10f);
        if (notificationObject) notificationObject.SetActive(true);

        yield return new WaitForSeconds(5f);
        if (notificationObject) notificationObject.SetActive(false);
    }
}



