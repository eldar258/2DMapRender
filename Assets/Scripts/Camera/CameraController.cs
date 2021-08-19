using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public delegate void CameraTransformAction(Camera camera);
    public static CameraTransformAction ControlCameraTransform;
    public float speedZoom = 0.1f;
    public float maxZoom = 7;
    public float minZoom = 1;
    private bool _isMovement = true;
    Vector3 startMousPosition;

    void Awake()
    {
        UIController.ToggleCameraMovement += SetIsMovement;
    }
    

    void Update()
    {
        if (_isMovement)
        {
            MoveCameraByMouse();
            ZoomCamera();
            ControlCameraTransform?.Invoke(Camera.main);
        }
    }

    

    private void MoveCameraByMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else if (Input.GetMouseButton(0))
        {
            var direction = startMousPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += direction;
        }
    }
    private void SetIsMovement(bool isMovement)
    {
        _isMovement = isMovement;
    }
    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float newOrthographicSize = Camera.main.orthographicSize + -Input.mouseScrollDelta.y * speedZoom;
            Camera.main.orthographicSize = Mathf.Clamp(newOrthographicSize, minZoom, maxZoom);
        }
    }
    void OnDestroy()
    {
        UIController.ToggleCameraMovement -= SetIsMovement;
    }
}
