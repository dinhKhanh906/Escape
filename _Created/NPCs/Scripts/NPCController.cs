
using UnityEngine;

public class NPCController : BaseInteraction
{
    [SerializeField] TalkingController talking;
    public override void Interact()
    {
        Conversation conversation = talking.GetConversation();

        if (conversation != null)
        {
            talking.dialog.ShowDialog(conversation);
        }
        else
        {
            talking.dialog.ShowDialog(talking.defaultConversation);
        }
    }
}