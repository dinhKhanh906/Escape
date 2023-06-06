using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoticeForm : MonoBehaviour
{
    public TMP_Text tmpTitle;
    public TMP_Text tmpContent;

    [SerializeField] Color colorWarning;
    [SerializeField] Color colorLog;
    [SerializeField] Image background;
    public void Setup(Notice notice)
    {
        switch (notice.type)
        {
            case TypeNotice.warning:
                {
                    background.color = colorWarning;
                    tmpContent.text = notice.content;
                    break;
                }
            case TypeNotice.log:
                {
                    background.color = colorLog;
                    tmpContent.text = notice.content;
                    break;
                }
        }
    }
}
