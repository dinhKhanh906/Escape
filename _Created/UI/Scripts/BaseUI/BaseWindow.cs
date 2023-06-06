

using UnityEngine;
using UnityEngine.Windows;

public abstract class BaseWindowControl: MonoBehaviour
{
    protected PlayerThirdPersonInput thirdPersonInput;
    protected PlayerUIInput _uiInput;
    public GameObject dialog;
    public bool isShowingDialog { get => dialog.activeSelf; }
    protected virtual void Start()
    {
        UIWindowManager manager = UIWindowManager.instance;
        _uiInput = manager.input;
        CloseDialog();
    }
    protected virtual void Update()
    {
        if (_uiInput.escape && isShowingDialog)
        {
            CloseDialog();
        }
    }
    public virtual void ShowDialog(bool isContinueAction)
    {
        UIWindowManager.instance.amountCurrenWindows++;

        if(thirdPersonInput == null)
            thirdPersonInput = FindObjectOfType<PlayerThirdPersonInput>();

        // allow player continue move, interact, jump.. or not
        thirdPersonInput.listening = isContinueAction;
        thirdPersonInput.virtualCam.enabled = false;
        // unlock cursor
        PlayerUIInput.UnlockCursor();

        // show window
        dialog.SetActive(true);
    }
    public virtual void CloseDialog()
    {
        Debug.Log($"close {gameObject.name}");
        UIWindowManager.instance.amountCurrenWindows--;
        // keep not allow player move, interact, jump.... if still has a window
        if (UIWindowManager.instance.amountCurrenWindows <= 0)
        {
            UIWindowManager.instance.amountCurrenWindows = 0;
            if (thirdPersonInput == null)
                thirdPersonInput = FindObjectOfType<PlayerThirdPersonInput>();

            // make sure allow player move, interact, jump, ..
            thirdPersonInput.listening = true;
            thirdPersonInput.virtualCam.enabled = true;
            // lock cursor
            PlayerUIInput.LockCursor();
        }

        // disable window
        dialog.SetActive(false);
    }
}