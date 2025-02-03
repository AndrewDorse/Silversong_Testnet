using Silversong.Data.Providers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    public static DataProvider instance;


    public static HeroClassProviderData HeroClassProviderData => instance._heroClassProviderData;
    public static RewardsProviderData RewardsProviderData => instance._rewardsProviderData;
    public static MobProviderData MobProviderData => instance._mobProviderData;
    public static BattlePrefabsProviderData BattlePrefabsProviderData => instance._battlePrefabsProviderData;





    [SerializeField] private HeroClassProviderData _heroClassProviderData;
    [SerializeField] private RaceProviderData _raceProviderData;

    [SerializeField] private InventoryItemProviderData _inventoryItemProviderData;

    [SerializeField] private MobProviderData _mobProviderData;


    [SerializeField] private ActiveAbilityProviderData _activeAbilityProviderData;
    [SerializeField] private PassiveAbilityProviderData _passiveAbilityProviderData;



    [SerializeField] private BuffProviderData _buffProviderData;



    [SerializeField] private StoryProviderData _storyProviderData;


    [SerializeField] private RewardsProviderData _rewardsProviderData;
    [SerializeField] private BattlePrefabsProviderData _battlePrefabsProviderData;






    [SerializeField] private Sprite _emptyItemSlot;


    



    private void Awake()
    {
        instance = this;
    }
      
    // races

    public Subrace GetSubrace(int id)
    {
        return _raceProviderData.GetSubrace(id);
    }

    public Race[] GetRaces()
    {
        return _raceProviderData.GetRaces();
    }

    // ITEMS


    public InventoryItem GetItem(int id)
    {
        return _inventoryItemProviderData.GetItem(id);
    }

    public List<InventoryItem> GetItemsByTier(int id)
    {
        return _inventoryItemProviderData.GetItemsByTier(id);
    }
     

    // ACTIVE ABILITIES
    public ActiveAbility GetAbility(int id)
    {
        return _activeAbilityProviderData.GetAbility(id);
    }
    public List<ActiveAbility> GetSpellsBySchool(Enums.MagicSchools magicSchool)
    {
        return _activeAbilityProviderData.GetSpellsBySchool(magicSchool);
    }
    public List<ActiveAbility> GetCommonSpells()
    {
        return _activeAbilityProviderData.GetCommonSpells();
    }


    // PASSIVE ABILITIES
    public PassiveAbility GetPassive(int id)
    {
        return _passiveAbilityProviderData.GetPassive(id);
    }
    public List<PassiveAbility> GetPassivesBySchool(Enums.MagicSchools magicSchool)
    {
        return _passiveAbilityProviderData.GetPassivesBySchool(magicSchool);
    }
    public List<PassiveAbility> GetCommonPassives()
    {
        return _passiveAbilityProviderData.GetCommonPassives();
    }


    // BUFF
    public Buff GetBuff(int id)
    {
        return _buffProviderData.GetBuff(id);
    }

    // 
    public Story GetStory(int id)
    {
        return _storyProviderData.GetStory(id);
    }





    public Sprite GetEmptySlotSprite()
    {
        return _emptyItemSlot;
    }
}
