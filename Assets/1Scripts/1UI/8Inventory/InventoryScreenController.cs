using Silversong.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreenController : ScreenController
{
    private readonly InventoryScreenView _view;


    private int _currentItemId;



    public InventoryScreenController(InventoryScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        SubcribeTopButtons();

        OpenItemsByTier(1);

        SetInventory(DataController.LocalPlayerData.heroData.items);


        EventsProvider.OnInventoryItemsChanged += SetInventory;
    }

    

   
    private void OpenItemsByTier(int tier) // 1 - 4
    {
        SetItems(DataProvider.instance.GetItemsByTier(tier));
    }

    private void SetItems(List<InventoryItem> items)
    {
        for(int i = 0; i < _view.shopLootSlots.Length; i++)
        {
            if(i < items.Count)
            {
                _view.shopLootSlots[i].gameObject.SetActive(true);
                _view.shopLootSlots[i].Setup(items[i], ItemInShopClick);
            }
            else
            {
                _view.shopLootSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetInventory(List<ItemSlot> items)
    {
        for (int i = 0; i < _view.inventorySlots.Length; i++)
        {
            if (i < items.Count)
            {
                _view.inventorySlots[i].gameObject.SetActive(true);
                _view.inventorySlots[i].Setup(DataProvider.instance.GetItem(items[i].Id), ItemInShopClick);
            }
            else
            {
                _view.inventorySlots[i].SetEmpty();
            }
        }
    }

    private void ItemInShopClick(int id)
    {
        ItemPopup itemPopup = Master.instance.GetPopup(Enums.PopupType.item) as ItemPopup;

        InventoryItem item = DataProvider.instance.GetItem(id);


        (Enums.UniversalButtonType, Action) openInfo = GetItemOpenTypeInShop(item);

        itemPopup.Setup(item, openInfo.Item1, openInfo.Item2, item.Cost);
        _currentItemId = id;
    }


    private (Enums.UniversalButtonType, Action) GetItemOpenTypeInShop(InventoryItem item)
    {
        Enums.UniversalButtonType result = Enums.UniversalButtonType.notEnoughMoney;
        Action callback = null;


        if (DataController.instance.IsEnoughPlaceInInventory())
        {
            result = Enums.UniversalButtonType.noPlace;
        }
        else if (!DataController.instance.IsEnoughMoney(item.Cost))
        {
            result = Enums.UniversalButtonType.notEnoughMoney;
        }
        else if(DataController.instance.IsEnoughMoney(item.Cost))
        {
            result = Enums.UniversalButtonType.canBuy;
            callback = BuyItem;
        }


        return (result, callback);
    }


    private void BuyItem()
    {
        DataController.instance.AddItem(_currentItemId);
    }























    private void SubcribeTopButtons()
    {
        _view.botButtons[0].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.camp));
        _view.botButtons[1].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.inventory));
        _view.botButtons[2].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.abilities));
        _view.botButtons[3].Setup(()=> Master.instance.ChangeGameStage(Enums.GameStage.heroStats));
    } 




    public override void Dispose()
    {
        base.Dispose();
        EventsProvider.OnInventoryItemsChanged -= SetInventory;
    }
}
