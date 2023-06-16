using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : Button
{
    AudioManager audioManager;
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(SetSoundClick);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveListener(SetSoundClick);
    }
    protected virtual void SetSoundClick()
    {
        if (!audioManager)
        {
            audioManager = AudioManager.instance;
        }

        AudioSource audioSource = null;
        AudioClip btnSound = null;
        if (audioManager)
        {
             audioSource = audioManager.audioSource;
             btnSound = audioManager.btnClick;
        }
        if (btnSound && audioSource)
        {
            audioSource.PlayOneShot(btnSound);
        }
    }
}
