using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/LevelProvider")]

    public class LevelProviderData : ScriptableObject
    {
        [SerializeField] private Level[] _levels;



        public Level GetLevel(int id)
        {
            if (id > _levels.Length - 1)
            {
                Debug.LogError("# Get Level - no Level with id " + id);
                return null;
            }

            return _levels[id];
        }


       







        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            Level[] levels = Resources.LoadAll<Level>("8Levels");

            _levels = new Level[levels.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                _levels[i] = levels[i];
                _levels[i].Id = i;
            }
#endif
        }

    }
}