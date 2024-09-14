using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Data.Providers
{
    [CreateAssetMenu(menuName = "Scriptable/Providers/RewardsProvider")]

    public class RewardsProviderData : ScriptableObject
    {
        [SerializeField] private RewardHelperSlot[] _slots;


        public Sprite GetIconByRewardType(Enums.RewardType rewardType, int value)
        {
            foreach(RewardHelperSlot slot in _slots)
            {
                if(slot.RewardType == rewardType)
                {
                    return slot.Icon;
                }
            }

            if(rewardType == Enums.RewardType.Item)
            {
                return InfoProvider.instance.GetItem(value).Icon;
            }

            else if (rewardType == Enums.RewardType.Passive )
            {
                return InfoProvider.instance.GetPassive(value).Icon;
            }

            return null;
        }
    }


    [System.Serializable]
    public class RewardHelperSlot
    {
        public Enums.RewardType RewardType;
        public Sprite Icon;
    }
}