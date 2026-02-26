using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    //variables
    private Camera mainCamera;
    private Vector3 mouseOrigin;
    private Vector3 theDifference;
    private Vector3 targetPosition;
    private Bounds cameraBounds;
    private bool isDragging;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        //height & width based on camera
        var height = mainCamera.orthographicSize;
        var width = height * mainCamera.aspect;
        //boundaries
        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;
        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;
        //camera boundaries
        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
            );
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) mouseOrigin = GetMousePosition;
        isDragging = ctx.started || ctx.performed;
        print(isDragging);
    }

    private void LateUpdate()
    {
        if (!isDragging) return;

        theDifference = GetMousePosition - transform.position;
        targetPosition = mouseOrigin - theDifference;
        targetPosition = GetCameraBounds();
        transform.position = targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        );
    }

    private Vector3 GetMousePosition => mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}