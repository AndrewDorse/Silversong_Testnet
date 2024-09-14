using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/BuffProvider")]

    public class BuffProviderData : ScriptableObject
    {
        [SerializeField] private Buff[] _buffs;



        public Buff GetBuff(int id)
        {
            if (id > _buffs.Length - 1)
            {
                Debug.LogError("# Get Buff - no Buff with id " + id);
                return null;
            }

            return _buffs[id];
        }


       







        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            Buff[] buffs = Resources.LoadAll<Buff>("5Buffs");

            _buffs = new Buff[buffs.Length];

            for (int i = 0; i < buffs.Length; i++)
            {
                _buffs[i] = buffs[i];
                _buffs[i].Id = i;
            }
#endif
        }

    }
}