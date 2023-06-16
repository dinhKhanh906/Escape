using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupForm : MonoBehaviour
{
    public TMP_Text tmpTitle;
    public TMP_Text tmpContent;
    public void CloseForm()
    {
        Destroy(gameObject);
    }
}
