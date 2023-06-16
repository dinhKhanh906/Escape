
using UnityEngine;

public class TestSaveLoad: MonoBehaviour
{
    public string fileName;
    public Renderer gfx;
    public Material matToSave;
    public Material matInstance;
    public void Save()
    {
        PartMatData hair = new PartMatData("_HairTint", matToSave.GetColor("_HairTint"));
        PartMatData hood = new PartMatData("_HoodTint", matToSave.GetColor("_HoodTint"));
        PartMatData upper = new PartMatData("_UpperTint", matToSave.GetColor("_UpperTint"));
        PartMatData gloves = new PartMatData("_GlovesTint", matToSave.GetColor("_GlovesTint"));
        PartMatData strap = new PartMatData("_StrapTint", matToSave.GetColor("_StrapTint"));
        PartMatData pant = new PartMatData("_PantTint", matToSave.GetColor("_PantTint"));
        PartMatData boots = new PartMatData("_BootsTint", matToSave.GetColor("_BootsTint"));

        SLotMatData slot = new SLotMatData(hair, hood, upper, gloves, strap, pant, boots);
        DataManager.SaveData<SLotMatData>(slot, Application.dataPath + "/" + fileName);

        //PartMatData newPart = new PartMatData(nameParameter, colorToSave);
        //DataManager.SaveData<PartMatData>(newPart, Application.dataPath + "/" + fileName);
    }
    public void Load()
    {
        SLotMatData slot = DataManager.LoadData<SLotMatData>(Application.dataPath + "/" + fileName);
        Material newMat = new Material(matToSave);
        // set properties for material instance
        newMat.SetColor(slot.hair.nameParameter, slot.hair.colorData.ToColor());
        newMat.SetColor(slot.hood.nameParameter, slot.hood.colorData.ToColor());
        newMat.SetColor(slot.gloves.nameParameter, slot.gloves.colorData.ToColor());
        newMat.SetColor(slot.upper.nameParameter, slot.upper.colorData.ToColor());
        newMat.SetColor(slot.strap.nameParameter, slot.strap.colorData.ToColor());
        newMat.SetColor(slot.pant.nameParameter, slot.pant.colorData.ToColor());
        newMat.SetColor(slot.boots.nameParameter, slot.boots.colorData.ToColor());

        matInstance = newMat;
        Debug.Log("Changed color");
    }
    public void Show()
    {
        // apply for gfx
        gfx.sharedMaterial = matInstance;

    }
}