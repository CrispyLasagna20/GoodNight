using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //cursor variables
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorClick;
    private Vector2 cursorHotSpot;

    //input variables
    InputAction click;

    //runs on start
    void Start()
    {
        cursorHotSpot = new Vector2(cursorDefault.width, cursorDefault.height);
        Cursor.SetCursor(cursorDefault, cursorHotSpot, CursorMode.Auto);

        click = InputSystem.actions.FindAction("Click");
    }

    void Update()
    {
        if (click.IsPressed())
        {
            Cursor.SetCursor(cursorClick, cursorHotSpot, CursorMode.Auto);
        }

        else if (click.WasReleasedThisFrame())
        {
                Cursor.SetCursor(cursorDefault, cursorHotSpot, CursorMode.Auto);
        }
    }
}
    