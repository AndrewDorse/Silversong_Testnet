using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/MobProvider")]

    public class MobProviderData : ScriptableObject
    {
        [SerializeField] private Mob[] _mobs;



        public Mob GetMob(int id)
        {
            if (id > _mobs.Length - 1)
            {
                Debug.LogError("# Get Mob - no Mob with id " + id);
                return null;
            }

            return _mobs[id];
        }


       







        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            Mob[] mobs = Resources.LoadAll<Mob>("7Mobs");

            _mobs = new Mob[mobs.Length];

            for (int i = 0; i < mobs.Length; i++)
            {
                _mobs[i] = mobs[i];
                _mobs[i].Id = i;
            }
#endif
        }

    }
}