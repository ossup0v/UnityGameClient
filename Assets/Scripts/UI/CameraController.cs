using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerManager Player;
    public float Sensitivity = 100f;
    public float ClampAngle = 65f;

    public float VerticalRotation;
    public float HorizontalRotation;

    private void Start()
    {
        VerticalRotation = transform.localEulerAngles.x;
        HorizontalRotation = transform.localEulerAngles.y;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorMode();
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Look();
        }
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    private void Look()
    {
        var mouseVertical = -Input.GetAxis("Mouse Y");
        var mouseHorizontal = Input.GetAxis("Mouse X");

        VerticalRotation += mouseVertical * Sensitivity * Time.deltaTime;
        HorizontalRotation += mouseHorizontal * Sensitivity * Time.deltaTime;

        VerticalRotation = Mathf.Clamp(VerticalRotation, -ClampAngle, ClampAngle);

        transform.localRotation = Quaternion.Euler(VerticalRotation, 0f, 0f);
        Player.transform.rotation = Quaternion.Euler(0f, HorizontalRotation, 0f);
    }

    private void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
