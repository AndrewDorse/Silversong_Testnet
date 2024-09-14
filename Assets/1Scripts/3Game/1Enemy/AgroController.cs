using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Game
{
    public class AgroController
    {

        private List<EnemyRecievedDamageSlot> _attackers;

        private Action<string> _onChangeTargetCallback;
        private string _currentTargetId;


        public AgroController(Action<string> onChangeTargetCallback)
        {
            EventsProvider.EverySecond += TimeTick;
            _onChangeTargetCallback = onChangeTargetCallback;
        }





        public void OnRecievedDamage(string attackerId, float damageAmount)
        {
            if (_attackers == null)
            {
                _attackers = new List<EnemyRecievedDamageSlot>();
                _attackers.Add(new EnemyRecievedDamageSlot(attackerId, damageAmount));
                return;
            }

            foreach (EnemyRecievedDamageSlot slot in _attackers)
            {
                if (slot.attackingId == attackerId)
                {
                    slot.accumulatedDamage += damageAmount;
                    break;
                }
            }
        }

        private void CheckAgro()
        {
            if (_attackers == null)
            {
                return;
            }

            string bestDamageCharacterId = string.Empty;
            float bestDamageResult = 0;


            foreach (EnemyRecievedDamageSlot slot in _attackers)
            {
                if (slot.accumulatedDamage > bestDamageResult)
                {
                    bestDamageCharacterId = slot.attackingId;
                    bestDamageResult = slot.accumulatedDamage;
                }
            }

            if (_currentTargetId != bestDamageCharacterId)
            {
                _onChangeTargetCallback?.Invoke(bestDamageCharacterId);
                _currentTargetId = bestDamageCharacterId;
            }
        }

        private void TimeTick()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                CheckAgro();
            }
            else
            {

            }
        }

        public void Dispose()
        {
            EventsProvider.EverySecond -= TimeTick;
        }



    }
}