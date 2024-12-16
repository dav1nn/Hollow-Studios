using UnityEngine;
using System.Collections.Generic;

public class ResetObjectPositions : MonoBehaviour
{
    [System.Serializable]
    private class ObjectData
    {
        public GameObject targetObject;
        [HideInInspector]
        public Vector3 originalPosition; 
    }

    [SerializeField]
    private List<ObjectData> objectsToReset = new List<ObjectData>();

    private void Start()
    {
        foreach (var obj in objectsToReset)
        {
            if (obj.targetObject != null)
            {
                obj.originalPosition = obj.targetObject.transform.position;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var obj in objectsToReset)
            {
                if (obj.targetObject != null)
                {
                    obj.targetObject.transform.position = obj.originalPosition;
                }
            }
        }
    }
}
