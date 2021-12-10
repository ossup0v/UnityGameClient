using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            NetworkClientSend.PlayerShoot(camTransform.forward);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            NetworkClientSend.PlayerThrowItem(camTransform.forward);
        }

        if (Input.mouseScrollDelta == Vector2.down || Input.mouseScrollDelta == Vector2.up)
        {
            Debug.Log($"Mouse! {Input.mouseScrollDelta}");
            NetworkClientSend.PlayerChangeWeapon((int)Input.mouseScrollDelta.x);
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
                Input.GetKey(KeyCode.Space)
            };

        NetworkClientSend.PlayerMovement(input);
    }
}
