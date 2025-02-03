using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private bool _ai;
    private string _id;

    public void Setup(bool ai, string id)
    {
        _ai = ai;
        _id = id;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                if (_ai == true)
                {
                    EventsProvider.OnLocalAiHeroWeaponHitEnemyCollider?.Invoke(target, _id);
                }
                else
                { 
                    EventsProvider.OnLocalHeroWeaponHitEnemyCollider?.Invoke(target);
                }
            }
        }
    }
}
