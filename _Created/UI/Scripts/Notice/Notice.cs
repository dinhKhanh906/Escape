
public enum TypeNotice { warning, log }
[System.Serializable]
public class Notice
{
    public TypeNotice type;
    public string content;
}