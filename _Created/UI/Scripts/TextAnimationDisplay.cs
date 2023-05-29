using System;
using System.Collections;
using TMPro;
using UnityEngine;

[System.Serializable]
public class TextAnimationDisplay
{
    public bool completed { get; private set; }
    public static Action<int> OnCompleted;
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
            for (int i = 0; i < input.Length; i++)
            {
                yield return new WaitForSeconds(delay);
                tmp.text += input[i];
            }
            completed = true;
        }

    }
}
