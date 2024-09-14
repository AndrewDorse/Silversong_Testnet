using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/PassiveAbilityProvider")]

    public class PassiveAbilityProviderData : ScriptableObject
    {
        [SerializeField] private PassiveAbility[] _abilities;
        [SerializeField] private List<MagicSchoolAbilitiesSlot> _schoolAbilities;
        [SerializeField] private List<int> _commonAbilitiesIds;

        public PassiveAbility GetPassive(int id)
        {
            if (id > _abilities.Length - 1)
            {
                Debug.LogError("# Get PassiveAbility - no PassiveAbility with id " + id);
                return null;
            }

            return _abilities[id];
        }

        



        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            PassiveAbility[] abilities = Resources.LoadAll<PassiveAbility>("4Abilities/2Passive");

            _abilities = new PassiveAbility[abilities.Length];

            for (int i = 0; i < abilities.Length; i++)
            {
                _abilities[i] = abilities[i];
                _abilities[i].Id = i;

                SortPassive(_abilities[i]);
            }

           // ResetCommonSpells();
#endif
        }



        private void ResetCommonSpells()
        {
            _commonAbilitiesIds = new List<int>();
            PassiveAbility[] commonSpells = Resources.LoadAll<PassiveAbility>("4Abilities/2Passive/0Common");

            for (int i = 0; i < commonSpells.Length; i++)
            {
                _commonAbilitiesIds.Add(commonSpells[i].Id);
            }
        }

        private void SortPassive(PassiveAbility spell)
        {
            foreach (MagicSchoolAbilitiesSlot slot in _schoolAbilities)
            {
                if (slot.School == spell.MagicSchool)
                {
                    slot.Ids.Add(spell.Id);
                    return;
                }
            }

            _schoolAbilities.Add(new MagicSchoolAbilitiesSlot { School = spell.MagicSchool, Ids = new List<int> { spell.Id } });
        }


        public List<PassiveAbility> GetPassivesBySchool(Enums.MagicSchools magicSchool)
        {
            List<int> listIds = null;
            List<PassiveAbility> result = new List<PassiveAbility>();

            foreach (MagicSchoolAbilitiesSlot slot in _schoolAbilities)
            {
                if (slot.School == magicSchool)
                {
                    listIds = slot.Ids;
                }
            }            

            for (int i = 0; i < listIds.Count; i++)
            {
                result.Add(GetPassive(listIds[i]));
            }

            return result;
        }

        public List<PassiveAbility> GetCommonPassives()
        {
            List<PassiveAbility> result = new List<PassiveAbility>();

            for (int i = 0; i < _commonAbilitiesIds.Count; i++)
            {
                result.Add(GetPassive(_commonAbilitiesIds[i]));
            }

            return result;
        }












    }



    [System.Serializable]
    public class MagicSchoolAbilitiesSlot
    {
        public Enums.MagicSchools School;
        public List<int> Ids;    
    }



}