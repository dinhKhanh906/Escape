
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog: MonoBehaviour
{
    [SerializeField] PlayerThirdPersonInput _input;
    [SerializeField] float _delayText = 1f;
    [SerializeField] Image _avatar;
    [SerializeField] TMP_Text _nameActor;
    [SerializeField] TMP_Text _content;
    [SerializeField] Queue<Message> _allMessages;
    [SerializeField] Message _currentMessage;

    [SerializeField] TextAnimationDisplay textAnimation;
    private void Start()
    {
        _allMessages = new Queue<Message>();
        HideDialog();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && textAnimation.completed)
        {
            NextMessage();
        }
    }
    public void ShowDialog(Conversation conversation)
    {
        _input.listening = false;
        gameObject.SetActive(true);

        _allMessages = conversation.messagesQueue;

        NextMessage();
    }
    public void NextMessage()
    {
        if (_allMessages.TryDequeue(out _currentMessage) == false) _currentMessage = null;

        DisplayMessage(_currentMessage);
    }
    public void DisplayMessage(Message newMessage)
    {
        // change content
        if(newMessage != null)
        {
            _avatar.sprite = newMessage.actor.avatar;
            _nameActor.text = newMessage.actor.name;
            //_content.text = newMessage.content;
            StartCoroutine(textAnimation.Sequence(_content, newMessage.content, _delayText));
        }
        else
        {
            HideDialog();
        }

        // maybe little animation here
    }
    public void HideDialog()
    {
        _currentMessage = null;

        _input.listening = true;
        gameObject.SetActive(false);
    }
}