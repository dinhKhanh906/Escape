
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow: BaseWindowControl
{
    [SerializeField] PlayerThirdPersonInput _input;
    [SerializeField] float _delayText = 1f;
    [SerializeField] Image _avatar;
    [SerializeField] TMP_Text _nameActor;
    [SerializeField] TMP_Text _content;
    [SerializeField] BaseButton _btnNext;
    [SerializeField] Queue<Message> _allMessages;
    [SerializeField] Message _currentMessage;

    [SerializeField] TextAnimationDisplay textAnimation;
    private void OnEnable()
    {
        _btnNext.onClick.AddListener(() => NextMessage());
    }
    protected override void Start()
    {
        base.Start();
        _allMessages = new Queue<Message>();
    }
    protected override void Update()
    {
        if (_uiInput.accept && textAnimation.completed)
        {
            NextMessage();
        }
    }
    public void SetConversation(Conversation conversation)
    {
        _allMessages = conversation.messagesQueue;
    }
    public override void ShowDialog(bool isContinueAction)
    {
        base.ShowDialog(isContinueAction);

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
            textAnimation.onStart += () => _btnNext.gameObject.SetActive(false); 
            textAnimation.onCompleted += () => _btnNext.gameObject.SetActive(true);
            StartCoroutine(textAnimation.Sequence(_content, newMessage.content, _delayText));
        }
        else
        {
            CloseDialog();
        }

    }
    public override void CloseDialog()
    {
        base.CloseDialog();

        _currentMessage = null;
    }
}