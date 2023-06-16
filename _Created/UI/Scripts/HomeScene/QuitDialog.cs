

using UnityEngine;

namespace HomeScene
{
    public class QuitDialog : DialogHomeScene
    {
        public BaseButton btnYes;
        public BaseButton btnNo;
        protected virtual void OnEnable()
        {
            if (btnYes)
                btnYes.onClick.AddListener(QuitGame);
            if(btnNo)
                btnNo.onClick.AddListener(this.HideDialog);
        }
        protected virtual void OnDisable()
        {
            if (btnYes)
                btnYes.onClick.RemoveListener(QuitGame);
            if (btnNo)
                btnNo.onClick.RemoveListener(this.HideDialog);
        }
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("i quit game");
        }
    }
}
