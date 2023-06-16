
using Unity.VisualScripting;
using UnityEngine;

namespace HomeScene
{
    public class DialogHomeScene: MonoBehaviour
    {
        public GameObject dialog;
        public virtual void ShowDialog()
        {
            if (dialog)
            {
                dialog.SetActive(true);
            }
        }
        public virtual void HideDialog()
        {
            if (dialog)
            {
                dialog.SetActive(false);
            }
        }
    }
}