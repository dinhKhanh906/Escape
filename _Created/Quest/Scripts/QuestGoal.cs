
using UnityEngine;
using UnityEngine.Events;

public abstract class QuestGoal: ScriptableObject
{
    public UnityEvent onReached;
    public abstract string GetProcess();
}
