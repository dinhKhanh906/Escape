
using UnityEngine;

[System.Serializable]
public class ColorData: DataObject
{
    public float r;
    public float g;
    public float b;
    public float a;
    public ColorData(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    public ColorData(Color color)
    {
        this.r = color.r;
        this.g = color.g;
        this.b = color.b;
        this.a = color.a;
    }
    public Color ToColor()
    {
        return new Color(r, g, b, a);
    }
}