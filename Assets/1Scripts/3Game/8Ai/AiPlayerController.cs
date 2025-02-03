using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiPlayerController
{
    private NavMeshAgent _navMeshAgent;
    private Enemy _target;

    public AiPlayerController(OtherHeroMovementController movementController)
    {

        _navMeshAgent = movementController.GetComponent<NavMeshAgent>();

        EventsProvider.EverySecond += Tick;
    }



    private void Tick()
    {
        _target = _target != null ? _target : TargetProvider.GetClosestEnemy(_navMeshAgent.transform.position);

        if (_target == null)
        {
            return;
        }

        if (Vector3.Distance(_navMeshAgent.transform.position, _target.transform.position) > 1.2f)
        {
            _navMeshAgent.SetDestination(_target.GetPosition());
        }
        else
        {
            _navMeshAgent.ResetPath();
        }
    }



    public void Dispose()
    {
        EventsProvider.EverySecond -= Tick;
    }
}
