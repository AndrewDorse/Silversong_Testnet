using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/StoryProvider")]

    public class StoryProviderData : ScriptableObject
    {
        [SerializeField] private Story[] _stories;



        public Story GetStory(int id)
        {
            if (id > _stories.Length - 1)
            {
                Debug.LogError("# Get Story - no Story with id " + id);
                return null;
            }

            return _stories[id];
        }


       







        [ContextMenu("# Initialize")]
        private void Initialize()
        {
#if UNITY_EDITOR
            Story[] stories = Resources.LoadAll<Story>("9Stories");

            _stories = new Story[stories.Length];

            for (int i = 0; i < stories.Length; i++)
            {
                _stories[i] = stories[i];
                _stories[i].Id = i;
            }
#endif
        }

    }
}