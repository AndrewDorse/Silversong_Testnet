using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModelController : MonoBehaviour
{
    public Animator Animator => _animator;


    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material[] materials;
    [SerializeField] private Animator _animator;

    

    private Enemy _enemy;

    public void ChangeMaterial(int value)
    {
        _renderer.material = materials[value];
    }

    public void AttackAnimation()
    {
        if (_enemy == null) { if (transform.parent != null) _enemy = transform.parent.GetComponent<Enemy>(); }

        if (_enemy != null)
        {
            //_enemy.CheckPassive(EnumsHandler.PassiveTrigger.attackAnimation);
           // _enemy.Hit();
        }
    }

    public void CastAnimation()
    {
        if (_enemy == null)
        {
            _enemy = transform.parent.GetComponent<Enemy>();
        }
        else
        {
         //   _enemy.CastSpellTriggerFromAnimation();
        }
    }

    public void Death()
    {
        Destroy(gameObject, 1f);
    }
}
