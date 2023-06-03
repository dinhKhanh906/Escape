using Cinemachine;
using UnityEngine;

public class PlayerThirdPersonInput : MonoBehaviour
{
    public bool listening = true;
    public CinemachineFreeLook virtualCam;
    private void Start()
    {
        listening = true;
    }
    public float moveHorizontal { get => listening ? Input.GetAxis("Horizontal") : 0f; }
    public float moveVertical { get => listening ? Input.GetAxis("Vertical") : 0f; }
    public bool sprint { get => listening ? Input.GetKey(KeyCode.LeftShift) : false; }
    public bool jump { get => listening ? Input.GetKeyUp(KeyCode.Space) : false; }
    public bool interact { get => listening ? Input.GetMouseButtonDown(0) : false; }
    public bool switchTarget { get => listening ? Input.GetMouseButtonDown(1) : false; }
}
