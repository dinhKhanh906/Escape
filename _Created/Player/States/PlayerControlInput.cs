using UnityEngine;

public class PlayerControlInput : MonoBehaviour
{
    public float moveHorizontal { get => Input.GetAxis("Horizontal"); }
    public float moveVertical { get => Input.GetAxis("Vertical"); }
    public bool sprint { get => Input.GetKey(KeyCode.LeftShift); }
    public bool jump { get => Input.GetKeyUp(KeyCode.Space); }
    public bool interact { get => Input.GetMouseButtonDown(0); }
}
