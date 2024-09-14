using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                EventsProvider.OnLocalHeroWeaponHitEnemyCollider?.Invoke(target);
            }
        }
    }
}
