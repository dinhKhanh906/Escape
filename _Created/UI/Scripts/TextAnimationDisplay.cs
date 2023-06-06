using System;
using System.Collections;
using TMPro;
using UnityEngine;

public delegate void Notification();

[System.Serializable]
public class TextAnimationDisplay
{
    public bool completed { get; private set; }
    public Notification onCompleted;
    public Notification onStart;
    public TextAnimationDisplay()
    {
        completed = true;
    }
    public IEnumerator Sequence(TMP_Text tmp, string input, float delay)
    {
        // make sure do not anything now
        if (completed)
        {
            // clear old characters
            completed = false;
            tmp.text = "";

            // display
            onStart.Invoke();
            for (int i = 0; i < input.Length; i++)
            {
                yield return new WaitForSeconds(delay);
                tmp.text += input[i];
            }
            onCompleted.Invoke();
            completed = true;
        }

    }
}
