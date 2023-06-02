
using UnityEngine;

[CreateAssetMenu(fileName = "Goal Collect Item", menuName = "Quest/Goals/Collect Item")]
public class GoalCollectItem : QuestGoal
{
    public int requireAmount;
    public int currentAmount;
    public void Add(int amount)
    {
        currentAmount += amount;
        if(currentAmount >= requireAmount)
        {
            // update process if > require amount
            QuestManager manager = QuestManager.instance;
            if(manager != null )
            {
                manager.currentQuest.UpdateProcess(this);
            }
        }
    }
    public override string GetProcess()
    {
        return "This is the process of GoalColectItem";
    }
}