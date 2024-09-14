using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/HeroClassProvider")]

    public class HeroClassProviderData : ScriptableObject
    {
        [SerializeField] private HeroClass[] _heroClasses;
        [SerializeField] private HeroClass[] _startHeroClasses;


        public HeroClass GetHeroClass(int id)
        {
            if (id > _heroClasses.Length - 1)
            {
                Debug.LogError("# Get HeroClass - no HeroClass with id " + id);
                return null;
            }

            return _heroClasses[id];
        }

        public HeroClass[] GetStartHeroClasses()
        {
            return _startHeroClasses;
        }


        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            HeroClass[] heroClasses = Resources.LoadAll<HeroClass>("1HeroClasses");

            _heroClasses = new HeroClass[heroClasses.Length];
            _startHeroClasses = new HeroClass[3];

            for (int i = 0; i < heroClasses.Length; i++)
            {
                _heroClasses[i] = heroClasses[i];
                _heroClasses[i].id = i;

                if(i < 3)
                {
                    _startHeroClasses[i] = heroClasses[i];
                }
            }
#endif
        }
    }
}