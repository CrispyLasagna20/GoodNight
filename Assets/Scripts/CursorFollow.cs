using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class CursorFollow : MonoBehaviour
{
    private void Update()
    {
        //Vector3 other = transform.position; //Mouse.current.position.ReadValue();
        //other.z = -10F;
        //transform.position = other;
        
        float2 screenPosition = Mouse.current.position.ReadValue();
        float2 boundScreenPosition = screenPosition / new float2(Screen.width, Screen.height);
        
    }
}
