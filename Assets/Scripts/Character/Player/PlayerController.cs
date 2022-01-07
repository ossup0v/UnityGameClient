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
                Input.GetKey(KeyCode.W), //0
                Input.GetKey(KeyCode.S), //1
                Input.GetKey(KeyCode.A), //2
                Input.GetKey(KeyCode.D), //3
                Input.GetKey(KeyCode.Space), //4
                Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift), //5
                Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl), //6
                Input.GetKey(KeyCode.Alpha1), //7
                Input.GetKey(KeyCode.Alpha2), //8
                Input.GetKey(KeyCode.Alpha3), //9
                Input.GetKey(KeyCode.Alpha4), //10
                Input.GetKey(KeyCode.Alpha5), //11
                Input.GetKey(KeyCode.Alpha6), //12
                Input.GetKey(KeyCode.Alpha7), //13
                Input.GetKey(KeyCode.Alpha8), //14
                Input.GetKey(KeyCode.Alpha9), //15
                Input.GetKey(KeyCode.Alpha0), //16
        };

        NetworkClientSendRoom.PlayerInput(input);
    }
}
