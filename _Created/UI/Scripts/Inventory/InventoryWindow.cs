using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : BaseWindowControl
{
    public PlayerStorage playerStorage;
    public ItemSlot slotSelected;
    [SerializeField] Transform _contentSlots;
    [SerializeField] GameObject _itemSlotPrefab;
    [SerializeField] Slider _sliderAmountUse;
    [SerializeField] BaseButton _btnUse, _btnClose;
    [SerializeField] TMP_Text _tmpAmount;
    [SerializeField] TMP_Text _tmpDescriptionTitle, _tmpDescriptionContent;
    protected override void Awake()
    {
        playerStorage = FindObjectOfType<PlayerStorage>();
    }
    private void OnEnable()
    {
        _sliderAmountUse.onValueChanged.AddListener(delegate { UpdateValueFollowSlider(); });
        _btnUse.onClick.AddListener(delegate { UseItem(); });
        _btnClose.onClick.AddListener(() => { CloseDialog(); });
    }
    protected virtual void Update()
    {
        if (!isShowingDialog && _uiInput.inventory) ShowDialog(false);
    }
    public override void ShowDialog(bool isContinueAction)
    {
        base.ShowDialog(isContinueAction);
        // now is not any slot selected
        DisplayDefault();

        // update items
        UpdateItems();
    }
    public override void CloseDialog()
    {
        base.CloseDialog();
        if(slotSelected) slotSelected.UnHighLight();
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
            _sliderAmountUse.value = 1;
            _sliderAmountUse.gameObject.SetActive(false);
            // amount always is one if type is "onlyUseSingle"
            _tmpAmount.text = 1.ToString();
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
        // ignore if dont has any changed
        if (!playerStorage.hasChangedElements) return;

        // clear old slots
        ClearOldSlots();

        // respawn slots
        Dictionary<string, ItemsHolder> all = playerStorage.storage;
        foreach(ItemsHolder item in all.Values)
        {
            CreateItemSlot(item);
        }
        // nothing changed
        playerStorage.hasChangedElements = false;
    }
    public void ClearOldSlots()
    {
        int sizeOldContent = _contentSlots.transform.childCount;
        for(int i=0; i<sizeOldContent; i++)
        {
            Destroy(_contentSlots.transform.GetChild(i).gameObject);
        }
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
        int amount = int.Parse(_tmpAmount.text);

        slotSelected.Use(amount);
        if (slotSelected.itemHolder.Amount() <= 0)
            playerStorage.storage.Remove(slotSelected.itemHolder.TypeItem().key);

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
