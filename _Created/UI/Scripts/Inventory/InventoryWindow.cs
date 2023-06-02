using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    public bool hasChangedContent;
    public ItemStorage playerStorage;
    public ItemSlot slotSelected;
    [SerializeField] Transform _contentSlots;
    [SerializeField] GameObject _itemSlotPrefab;
    [SerializeField] Slider _sliderAmountUse;
    [SerializeField] BaseButton _btnUse;
    [SerializeField] TMP_Text _tmpAmount;
    [SerializeField] TMP_Text _tmpDescriptionTitle, _tmpDescriptionContent;
    private void Awake()
    {
        _sliderAmountUse.onValueChanged.AddListener(delegate { UpdateValueFollowSlider(); });
        _btnUse.onClick.AddListener(delegate { UseItem(); });

    }
    private void OnEnable()
    {
        // now is not any slot selected
        DisplayDefault();

        // update items
        //if(hasChangedContent) UpdateItems();
        UpdateItems();
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
    public void ArrangeSlots()
    {

    }
    public void SelectSlot(ItemSlot newSlot)
    {
        // enable use button
        _btnUse.gameObject.SetActive(true);
        // un highlight old slot
        if(slotSelected) slotSelected.UnHighLight();
        // hightlight new slot
        newSlot.HighLight();
        // active 
        _tmpAmount.gameObject.SetActive(true);
        _sliderAmountUse.gameObject.SetActive(true);
        //
        slotSelected = newSlot;

        BaseItem newItem = newSlot.itemHolder.TypeItem();
        // display description
        _tmpDescriptionTitle.text = newItem.nameItem;
        _tmpDescriptionContent.text = newItem.description;
        // setup amount use slider
        if (newItem.onlyUseSingle)
        {
            _sliderAmountUse.maxValue = 1;
            _sliderAmountUse.minValue = 1;
            _sliderAmountUse.value = 1;
        }
        else
        {
            _sliderAmountUse.maxValue = newSlot.itemHolder.Amount();
            _sliderAmountUse.minValue = 1;
            _sliderAmountUse.value = 1;
        }
    }
    public void UpdateItems()
    {
        Dictionary<string, ItemsHolder> all = playerStorage.storage;
        foreach(ItemsHolder item in all.Values)
        {
            CreateItemSlot(item);
        }

        // update complete
        //hasChangedContent = false;
    }
    public void CreateItemSlot(ItemsHolder info)
    {
        // instantiate new slot from prefab
        GameObject newSlot = Instantiate(_itemSlotPrefab, _contentSlots);
        ItemSlot controller = newSlot.GetComponent<ItemSlot>();

        // set up new slot
        controller.Debut(info);
        controller.btnTrigger.onClick.AddListener(() => SelectSlot(controller));
    }
    public void UseItem()
    {
        int amount = (int) _sliderAmountUse.value;

        slotSelected.Use(amount);

        // un highligt slot selected
        slotSelected.UnHighLight();
        // back to default display
        DisplayDefault();
    }
    public void DisplayDefault()
    {
        slotSelected = null;
        _tmpDescriptionTitle.text = "Description";
        _tmpDescriptionContent.text = "Select a item to view";
        _btnUse.gameObject.SetActive(false);
        _tmpAmount.gameObject.SetActive(false);
        _sliderAmountUse.gameObject.SetActive(false);
    }
    public void UpdateValueFollowSlider()
    {
        _tmpAmount.text = _sliderAmountUse.value.ToString();
    }
}
