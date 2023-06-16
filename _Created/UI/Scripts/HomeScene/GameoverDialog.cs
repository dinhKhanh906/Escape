

using System;
using UnityEngine;
namespace HomeScene
{
    public class GameoverDialog : DialogHomeScene
    {
        public BaseButton btnAccept;
        [SerializeField] int homeSceneIndex;

        Canvas _canvas;
        private void OnEnable()
        {
            if(_canvas == null)
            {
                _canvas = FindObjectOfType<Canvas>();
            }
            if (btnAccept)
                btnAccept.onClick.AddListener(BackHome);
        }
        private void OnDisable()
        {
            if(btnAccept)
                btnAccept.onClick.RemoveListener(BackHome);
        }
        public override void ShowDialog()
        {
            base.ShowDialog();

            PlayerUIInput.UnlockCursor();
            PlayerThirdPersonInput playerInput = FindObjectOfType<PlayerThirdPersonInput>();
            if (playerInput)
            {
                playerInput.listening = false;
                playerInput.virtualCam.enabled = false;
            }
        }
        public void BackHome()
        {
            MySceneManger sceneManger = MySceneManger.instance;
            if (sceneManger)
            {
                sceneManger.LoadScene(homeSceneIndex, _canvas);
            }
        }
    }
}
