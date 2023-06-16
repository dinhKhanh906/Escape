using UnityEngine;

[System.Serializable]
public class PartMatData: DataObject
{
    public string nameParameter;
    public ColorData colorData;

    public PartMatData(string nameParameter, ColorData colorData)
    {
        this.nameParameter = nameParameter;
        this.colorData = colorData;
    }
    public PartMatData(string nameParameter, Color color)
    {
        this.nameParameter = nameParameter;
        this.colorData = new ColorData(color.r, color.g, color.b, color.a);
    }
}