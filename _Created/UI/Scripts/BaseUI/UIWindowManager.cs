using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    [System.Serializable]
    public class Window
    {
        public string key;
        public BaseWindowControl baseWindow;
    }
    public static UIWindowManager instance;
    public PlayerUIInput input;
    public NoticeWindow notice;
    public int amountCurrenWindows;
    [SerializeField] List<Window> listWindowsPrefab;

    public Dictionary<string, BaseWindowControl> allWindows;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        allWindows = new Dictionary<string, BaseWindowControl>();
        foreach(Window window in listWindowsPrefab)
        {
            allWindows.Add(window.key, window.baseWindow);
        }
    }
    private void Start()
    {
        amountCurrenWindows = 0;
    }
    public T GetWindowByKey<T>(string key) where T : BaseWindowControl
    {
        BaseWindowControl baseWindow = null;
        T targetWindow = null;
        if(allWindows.TryGetValue(key, out baseWindow))
        {
            targetWindow = (T)baseWindow;
        }

        return targetWindow;
    }
    public void ShowNotice(Notice newNotice)
    {
        notice.AddNotice(newNotice);
    }
}
