using UnityEngine;

public class SceneCursorManager : MonoBehaviour
{
    public Texture2D customCursor; 
    public Vector2 hotSpot = Vector2.zero; 
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        ChangeCursor(customCursor);
    }

    void ChangeCursor(Texture2D cursorTexture)
    {
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
