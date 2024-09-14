using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/RaceProvider")]

    public class RaceProviderData : ScriptableObject
    {
        [SerializeField] private Race[] _races;
        [SerializeField] private Subrace[] _subraces;


        public Subrace GetSubrace(int id)
        {
            if (id > _subraces.Length - 1)
            {
                Debug.LogError("# Get Subrace - no Subrace with id " + id);
                return null;
            }

            return _subraces[id];
        }

        public Race[] GetRaces()
        {
            return _races;
        }



        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            Subrace[] subraces = Resources.LoadAll<Subrace>("2Races");

            _subraces = new Subrace[subraces.Length];

            for (int i = 0; i < subraces.Length; i++)
            {
                _subraces[i] = subraces[i];
                _subraces[i].id = i;
            }

            Race[] races = Resources.LoadAll<Race>("2Races");

            _races = new Race[races.Length];

            for (int i = 0; i < races.Length; i++)
            {
                _races[i] = races[i];
                _races[i].id = i;
            }
#endif
        }
    }
}