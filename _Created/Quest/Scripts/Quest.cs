
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QuestState { waiting, executing, finished}
public class Quest: ScriptableObject
{
    public string nameQuest;
    public string description;
    public QuestState state;
    public UnityEvent onFinished;
    public List<QuestReward> rewards;
    public List<QuestGoal> goalsRequire;
    public int goalRemaining { get => goalsRequire.Count; }

    public void UpdateProcess(QuestGoal goalReached)
    {
        // ignore this goal if it is not contained in goalsRequire
        if (!goalsRequire.Contains(goalReached)) return;

        // is contained
        goalsRequire.Remove(goalReached);
    }
}