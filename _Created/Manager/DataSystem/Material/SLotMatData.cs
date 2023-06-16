using UnityEngine;

[System.Serializable]
public class SLotMatData: DataObject
{
    public PartMatData hair;
    public PartMatData hood;
    public PartMatData upper;
    public PartMatData gloves;
    public PartMatData strap;
    public PartMatData pant;
    public PartMatData boots;

    public SLotMatData(PartMatData hair, PartMatData hood, PartMatData upper, PartMatData gloves, PartMatData strap, PartMatData pant, PartMatData boots)
    {
        this.hair = hair;
        this.hood = hood;
        this.upper = upper;
        this.gloves = gloves;
        this.strap = strap;
        this.pant = pant;
        this.boots = boots;
    }
    public SLotMatData(Color colorHair, Color colorHood, Color colorUpper, Color colorGloves, Color colorStrap, Color colorPant, Color colorBoots)
    {
        this.hair = new PartMatData("_HairTint", colorHair);
        this.hood = new PartMatData("_HoodTint", colorHood);
        this.upper = new PartMatData("_UpperTint", colorUpper);
        this.gloves = new PartMatData("_GlovesTint", colorGloves);
        this.strap = new PartMatData("_StrapTint", colorStrap);
        this.pant = new PartMatData("_PantTint", colorPant);
        this.boots = new PartMatData("_BootsTint", colorBoots);

    }
}