using UnityEngine;
public class ButtonLoadScene: BaseButton
{
    [SerializeField] int sceneIndex;
    MySceneManger sceneManger;
    Canvas canvas;
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(LoadScene);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveListener(LoadScene);
    }
    private void LoadScene()
    {
        if (sceneManger == null)
        {
            sceneManger = MySceneManger.instance;
        }
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }
        if (sceneManger && canvas)
        {
            sceneManger.LoadScene(sceneIndex, canvas);
        }
    }
}