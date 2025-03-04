using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResetObjectPositions : MonoBehaviour
{
    [System.Serializable]
    private class ObjectData
    {
        public GameObject targetObject;
        public Vector3 originalPosition;
    }

    [SerializeField]
    private List<ObjectData> objectsToReset = new List<ObjectData>();
    [SerializeField]
    private InputField inputField;

    private void Start()
    {
        foreach (var obj in objectsToReset)
        {
            if (obj.targetObject != null)
            {
                obj.originalPosition = obj.targetObject.transform.position;
            }
        }
        if (inputField != null)
        {
            inputField.onEndEdit.AddListener(HandleInputSubmit);
        }
    }

    private void HandleInputSubmit(string input)
    {
        if (input.ToLower().Equals("reset"))
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ResetPositions();
            }
            inputField.text = ""; 
        }
    }

    public void ResetPositions()
    {
        foreach (var obj in objectsToReset)
        {
            if (obj.targetObject != null)
            {
                obj.targetObject.transform.position = obj.originalPosition;
            }
        }
    }

    private void Update()
    {
        if (inputField.isFocused && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            HandleInputSubmit(inputField.text);
        }
    }
}
