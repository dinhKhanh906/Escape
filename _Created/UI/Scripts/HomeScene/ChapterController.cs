
using UnityEngine;

namespace HomeScene
{
    public class ChapterController : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        private void Awake()
        {
            if (canvas == null) canvas = FindObjectOfType<Canvas>();
        }
        public void GoToChapter(int index)
        {
            MySceneManger sceneManger = MySceneManger.instance;
            if (sceneManger)
            {
                sceneManger.LoadScene(index, canvas);
            }
        }
    }
}