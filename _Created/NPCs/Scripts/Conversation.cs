

using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu(fileName = "New conversation", menuName = "Conversation/Conversation")]
public class Conversation: ScriptableObject
{
    [SerializeField] Message[] allMessages;
    public Queue<Message> messagesQueue { get => InitializeMessages(); }

    private Queue<Message> InitializeMessages()
    {
        Queue<Message> messages = new Queue<Message>();
        for(int i=0; i<allMessages.Length; i++)
        {
            messages.Enqueue(allMessages[i]);
        }

        return messages;
    }
}