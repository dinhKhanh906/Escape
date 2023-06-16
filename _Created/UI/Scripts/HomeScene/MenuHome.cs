
using UnityEngine;

namespace HomeScene
{
    public class MenuHome: MonoBehaviour
    {
        public DialogHomeScene dialogInstruction;
        public DialogHomeScene dialogDetail;
        public DialogHomeScene dialogQuit;
        private void Start()
        {
            if(dialogDetail) dialogDetail.HideDialog();
            if(dialogQuit) dialogQuit.HideDialog();
            if(dialogInstruction) dialogInstruction.HideDialog();
        }
        public void Setting()
        {
            // display setting dialog
        }
        public void Detail()
        {
            // display detail dialog
            dialogDetail.ShowDialog();
        }
        public void Instruction()
        {
            // display instruction dialog
            dialogInstruction.ShowDialog();
        }
        public void Quit()
        {
            // may be display a dialog to make sure player want to quit
            dialogQuit.ShowDialog();
            // quit game
        }
    }
}