
using UnityEngine;

public class QuestManager: MonoBehaviour
{
    public static QuestManager instance;

    public Quest currentQuest;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}