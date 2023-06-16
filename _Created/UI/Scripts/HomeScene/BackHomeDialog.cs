using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeScene
{
    public class BackHomeDialog : DialogHomeScene
    {
        public BaseButton btnYes;
        public BaseButton btnNo;
        [SerializeField] int homeSceneIndex;
        Canvas _canvas;
        private void OnEnable()
        {
            if (!_canvas)
            {
                _canvas = FindObjectOfType<Canvas>();
            }
            if (btnYes)
                btnYes.onClick.AddListener(BackHome);
            if(btnNo)
                btnNo.onClick.AddListener(this.HideDialog);
        }
        private void OnDisable()
        {
            if (btnYes)
                btnYes.onClick.RemoveListener(BackHome);
            if (btnNo)
                btnNo.onClick.RemoveListener(this.HideDialog);
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
