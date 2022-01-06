using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementViaNetwork : CharacterMovement
{
    public void ForceUpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ForceUpdateRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}