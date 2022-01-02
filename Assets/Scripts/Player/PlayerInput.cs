using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool ForwardKey { get; protected set; }
    public bool BackwardKey { get; protected set; }
    public bool StrafeLeftKey { get; protected set; }
    public bool StrafeRightKey { get; protected set; }
    public bool JumpKey { get; protected set; }
    public bool SprintKey { get; protected set; }
    public bool DuckKey { get; protected set; }

    protected virtual void Update()
    {
        ForwardKey = Input.GetKey(KeyCode.W);
        BackwardKey = Input.GetKey(KeyCode.S);
        StrafeLeftKey = Input.GetKey(KeyCode.A);
        StrafeRightKey = Input.GetKey(KeyCode.D);
        JumpKey = Input.GetKey(KeyCode.Space);
        SprintKey = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        DuckKey = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);        
    }
}