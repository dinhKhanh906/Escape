

using UnityEngine;

public class PlayerUIInput: MonoBehaviour
{

    public bool inventory { get => Input.GetKeyDown(KeyCode.I); }
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}