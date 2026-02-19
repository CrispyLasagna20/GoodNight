using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    //variables
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorClick;
    private Vector2 cursorHotSpot;

    //runs on start
    void Start()
    {
        cursorHotSpot = new Vector2(cursorDefault.width, cursorDefault.height);
        Cursor.SetCursor(cursorDefault, cursorHotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorClick, cursorHotSpot, CursorMode.Auto);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(cursorDefault, cursorHotSpot, CursorMode.Auto);
        }
    }
}
    