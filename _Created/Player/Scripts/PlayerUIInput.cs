

using UnityEngine;

public class PlayerUIInput: MonoBehaviour
{

    public bool inventory { get => Input.GetKeyDown(KeyCode.I); }
    public bool accept { get => Input.GetKeyDown(KeyCode.Return); }
    public bool escape { get => Input.GetKeyDown(KeyCode.Escape); }
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}