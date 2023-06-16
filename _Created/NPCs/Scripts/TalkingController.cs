

using System.Collections.Generic;
using UnityEngine;


public class TalkingController: MonoBehaviour
{
    [SerializeField] Conversation[] allConversation;
    [SerializeField] bool unlockedNextConversation;
    Queue<Conversation> conversationsQueue;

    public Conversation defaultConversation;
    private void Awake()
    {
        InitializeConversations();
    }
    public void InitializeConversations()
    {
        conversationsQueue = new Queue<Conversation>();
        for(int i=0; i<allConversation.Length; i++)
        {
            conversationsQueue.Enqueue(allConversation[i]);
        }
    }

    // this method is temporary
    public Conversation GetConversation()
    {
        Conversation conversation;

        if (conversationsQueue.TryDequeue(out conversation) == false) return null;
        else return conversation;
    }
}