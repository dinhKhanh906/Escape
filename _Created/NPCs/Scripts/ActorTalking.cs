using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Actor talking", menuName = "Conversation/Actor talking")]
public class ActorTalking: ScriptableObject
{
    public TypeActorTalking type;
    public string nameActor;
    public Sprite avatar;
}
public enum TypeActorTalking
{
    NPC, 
    Player,
}
