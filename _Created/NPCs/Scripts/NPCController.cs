
using UnityEngine;

public class NPCController : BaseInteraction
{
    [SerializeField] TalkingController talking;
    public override void Interact()
    {
        Conversation conversation = talking.GetConversation();
        MessageWindow messageWindow = UIWindowManager.instance.GetWindowByKey<MessageWindow>("message");

        // set conversation for message window
        if (conversation != null)
        {
           messageWindow.SetConversation(conversation);
        }
        else
        {
            messageWindow.SetConversation(talking.defaultConversation);
        }
        messageWindow.ShowDialog(false);

    }
}