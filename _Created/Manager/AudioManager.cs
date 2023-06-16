
using UnityEngine;

public class AudioManager: MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip btnClick;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("has more than one AudioManager");
            Destroy(this);
            return;
        }
        instance = this;
    }
}