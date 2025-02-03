using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/ActiveAbilityProvider")]

    public class ActiveAbilityProviderData : ScriptableObject
    {
        [SerializeField] private ActiveAbility[] _abilities;
        [SerializeField] private List<MagicSchoolAbilitiesSlot> _schoolAbilities;
        [SerializeField] private List<int> _commonAbilitiesIds;

        public ActiveAbility GetAbility(int id)
        {
            if (id > _abilities.Length - 1)
            {
                Debug.LogError("# Get ActiveAbility - no ActiveAbility with id " + id);
                return null;
            }

            return _abilities[id];
        }

        



        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            ActiveAbility[] abilities = Resources.LoadAll<ActiveAbility>("4Abilities/1Active");

            _abilities = new ActiveAbility[abilities.Length];

            for (int i = 0; i < abilities.Length; i++)
            {
                _abilities[i] = abilities[i];
                _abilities[i].Id = i;

                SortSpell(_abilities[i]);
            }

            ResetCommonSpells();
#endif
        }



        private void ResetCommonSpells()
        {
            _commonAbilitiesIds = new List<int>();
            ActiveAbility[] commonSpells = Resources.LoadAll<ActiveAbility>("4Abilities/1Active/0Common");

            for (int i = 0; i < commonSpells.Length; i++)
            {
                _commonAbilitiesIds.Add(commonSpells[i].Id);
            }
        }

        private void SortSpell(ActiveAbility spell)
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


        public List<ActiveAbility> GetSpellsBySchool(Enums.MagicSchools magicSchool)
        {
            List<int> listIds = null;
            List<ActiveAbility> result = new List<ActiveAbility>();

            foreach (MagicSchoolAbilitiesSlot slot in _schoolAbilities)
            {
                if (slot.School == magicSchool)
                {
                    listIds = slot.Ids;
                }
            }            

            for (int i = 0; i < listIds.Count; i++)
            {
                result.Add(GetAbility(listIds[i]));
            }

            return result;
        }

        public List<ActiveAbility> GetCommonSpells()
        {
            List<ActiveAbility> result = new List<ActiveAbility>();

            for (int i = 0; i < _commonAbilitiesIds.Count; i++)
            {
                result.Add(GetAbility(_commonAbilitiesIds[i]));
            }

            return result;
        }












    }





}