using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camTransform;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            NetworkClientSendRoom.PlayerShoot(camTransform.forward);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            NetworkClientSendRoom.PlayerThrowItem(camTransform.forward);
        }

        if (Input.mouseScrollDelta == Vector2.down || Input.mouseScrollDelta == Vector2.up)
        {
            Debug.Log($"Mouse! {Input.mouseScrollDelta}");
            NetworkClientSendRoom.PlayerChangeWeapon((int)Input.mouseScrollDelta.x);
        }
    }
    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] input = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D),
                Input.GetKey(KeyCode.Space),
                Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift),
                Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl),
            };

        NetworkClientSendRoom.PlayerMovement(input);
    }
}
