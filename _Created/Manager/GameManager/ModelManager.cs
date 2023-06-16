
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelManager: MonoBehaviour
{
    public static ModelManager instance;

    [SerializeField] Material _matDefault;
    [SerializeField] Material _matSelected;
    [SerializeField] List<Material> _allSlotsMaterial;
    // file saved material
    [SerializeField] string[] nameFilesMaterial = new string[4] {"slot1.json", "slot2.json", "slot3.json", "slot4.json"};
    private void Awake()
    {
        if(instance)
        {
            Debug.LogWarning("Has more than one ModelManager");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // check slot material of model has init or not ?
        for(int i=0; i<nameFilesMaterial.Length; i++)
        {
            string path = Application.dataPath + "/" + nameFilesMaterial[i];
            if (!File.Exists(path))
            {
                // get values of default material 
                Color hairColor = _matDefault.GetColor("_HairTint");
                Color hoodColor = _matDefault.GetColor("_HoodTint");
                Color upperColor = _matDefault.GetColor("_UpperTint");
                Color glovesColor = _matDefault.GetColor("_GlovesTint");
                Color strapColor = _matDefault.GetColor("_StrapTint");
                Color pantColor = _matDefault.GetColor("_PantTint");
                Color bootsColor = _matDefault.GetColor("_BootsTint");
                // set json value same default material
                SLotMatData newSlot = new SLotMatData(hairColor, hoodColor, upperColor, glovesColor, strapColor, pantColor, bootsColor);
                DataManager.SaveData(newSlot, path);

                Debug.Log("init new file: " + nameFilesMaterial[i]);
                // add to slots list
                Material matInstance = new Material(_matDefault);
                _allSlotsMaterial.Add(matInstance);
            }
            else
            {
                // load data
                SLotMatData slot = DataManager.LoadData<SLotMatData>(path);
                Material matInstance = new Material(_matDefault);
                matInstance.name = "slot " + (i+1);
                matInstance.SetColor(slot.hair.nameParameter, slot.hair.colorData.ToColor());
                matInstance.SetColor(slot.hood.nameParameter, slot.hood.colorData.ToColor());
                matInstance.SetColor(slot.gloves.nameParameter, slot.gloves.colorData.ToColor());
                matInstance.SetColor(slot.upper.nameParameter, slot.upper.colorData.ToColor());
                matInstance.SetColor(slot.strap.nameParameter, slot.strap.colorData.ToColor());
                matInstance.SetColor(slot.pant.nameParameter, slot.pant.colorData.ToColor());
                matInstance.SetColor(slot.boots.nameParameter, slot.boots.colorData.ToColor());

                // add to slots list
                _allSlotsMaterial.Add(matInstance);
            }
        }
        foreach (Material mat in this.GetAllSlotsMaterial())
        {
            Debug.Log("manager added upper: " + mat.GetColor("_UpperTint"));
        }
    }
    public Material DefaultMaterial { get => this._matDefault; }
    public Material CurrentMaterial
    {
        get => _matSelected;
        set
        {
            _matSelected = value;
            // update plyaer's material if found
            UpdateCharacterRenderer();
        }
    }
    public List<Material> GetAllSlotsMaterial()
    {
        return this._allSlotsMaterial;
    }
    public void UpdateCharacterRenderer()
    {
        PlayerGFXControl player = FindObjectOfType<PlayerGFXControl>();
        if (player)
        {
            player.SetMaterial(_matSelected);
        }
    }
    public string GetDirectionToSlot(int index)
    {
        return Application.dataPath + "/" + nameFilesMaterial[index];
    }
    public void SaveValueOfSlot(int index, Material instanceMat)
    {
        string path = GetDirectionToSlot(index);
        // get values of instance material 
        Color hairColor = instanceMat.GetColor("_HairTint");
        Color hoodColor = instanceMat.GetColor("_HoodTint");
        Color upperColor = instanceMat.GetColor("_UpperTint");
        Color glovesColor = instanceMat.GetColor("_GlovesTint");
        Color strapColor = instanceMat.GetColor("_StrapTint");
        Color pantColor = instanceMat.GetColor("_PantTint");
        Color bootsColor = instanceMat.GetColor("_BootsTint");
        // set json value same instance material
        SLotMatData newSlot = new SLotMatData(hairColor, hoodColor, upperColor, glovesColor, strapColor, pantColor, bootsColor);
        DataManager.SaveData(newSlot, path);
        // update for this mat index
        _allSlotsMaterial[index] = instanceMat;
    }
}