using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{

    [CreateAssetMenu(menuName = "Scriptable/Providers/BattlePrefabsProviderData")] 
    public class BattlePrefabsProviderData : ScriptableObject
    {
        public TMPro.TMP_Text CombatText => _combatText;
        public GameObject[] Effects => _effects;

        [SerializeField] private GameObject[] _effects; // 0 - blood, 1 - white hit 
         

        [SerializeField] private TMPro.TMP_Text _combatText;


    }
}