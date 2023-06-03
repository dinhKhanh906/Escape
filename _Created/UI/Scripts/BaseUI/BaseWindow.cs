

using UnityEngine;

public abstract class BaseWindow: MonoBehaviour
{
    protected PlayerThirdPersonInput thirdPersonInput;
    public virtual void ShowWindow(bool isContinueAction)
    {
        if(thirdPersonInput == null)
            thirdPersonInput = FindObjectOfType<PlayerThirdPersonInput>();

        // allow player continue move, interact, jump.. or not
        thirdPersonInput.listening = isContinueAction;
        thirdPersonInput.virtualCam.enabled = false;
        // unlock cursor
        PlayerUIInput.UnlockCursor();

        // show window
        gameObject.SetActive(true);
    }
    public virtual void CloseWindow()
    {
        if(thirdPersonInput == null)
            thirdPersonInput = FindObjectOfType<PlayerThirdPersonInput>();

        // make sure allow player move, interact, jump, ..
        thirdPersonInput.listening = true;
        thirdPersonInput.virtualCam.enabled = true;
        // lock cursor
        PlayerUIInput.LockCursor();
        // disable window
        gameObject.SetActive(false);
    }
}