using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    public ITarget Target;
    public bool CanMove = true;
    public bool CanRotate = true;


    private float _speed = 3f;

    [SerializeField] private NavMeshAgent _navMeshAgent;


    private Vector3 _previousPosition;
    private Vector3 _expectedPosition;

   


    private EnemyAnimatorController _animatorController;
    private float _currentSpeed;


    private void Start()
    {
       
        _expectedPosition = transform.position;
    }


    private void FixedUpdate()
    {
        if(CanRotate)
        {
            if (Target != null)
            {
                _navMeshAgent.updateRotation = false;
                RotateToTarget(); 
            }
            else
            {
                _navMeshAgent.updateRotation = true;
            }
        }
    }

    private void RotateToTarget()
    {
        Vector3 targetDirection = new Vector3(Target.GetPosition().x, transform.position.y, Target.GetPosition().z) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime * 100);
    }



    public void Setup(EnemyAnimatorController animatorController)
    {
        _animatorController = animatorController;
        Subscribe();
    }

    public void UpdatePosition(Vector3 position)
    {
        _expectedPosition = position;
    }

    private void TimeTick()
    {
        if (CanMove == false)
        {
            _navMeshAgent.ResetPath();
            return;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            MoveCurrentEnemy();
        }
        else
        {
            MoveEnemyOther();
        }
    }



    private void MoveCurrentEnemy()
    {
        if(Target == null)
        {
            _navMeshAgent.ResetPath();
            return;
        }

        if(Vector3.Distance(Target.GetPosition(), transform.position) < 1f)
        {
            _navMeshAgent.ResetPath();
        }
        else
        {
            _navMeshAgent.SetDestination(Target.GetPosition());
        }

        CountCurrentSpeed();
    }


    private void MoveEnemyOther()
    {

        if (Target != null)
        {
            float dist = Vector3.Distance(transform.position, _expectedPosition);

            if (dist > 4f)
            {
                //float dist = Vector3.Distance(_expectedPosition, Target.GetPosition());
                //Vector3 averagePostion = (_expectedPosition + Target.GetPosition()) / 2;

                _navMeshAgent.speed = _speed * 1.5f;
                _navMeshAgent.SetDestination(_expectedPosition);
                return;
            }
            else if(dist > 1f)
            {
                _navMeshAgent.speed = _speed * 1.21f;
                _navMeshAgent.SetDestination(_expectedPosition);
            }
            else if (dist <= 1f)
            {
                _navMeshAgent.speed = _speed;
                _navMeshAgent.ResetPath();
            }
        }
        else
        {
            _navMeshAgent.SetDestination(_expectedPosition);
        }

        CountCurrentSpeed();

    }

    private void CountCurrentSpeed()
    {
        Vector3 curMove = transform.localPosition - _previousPosition;
        _currentSpeed = curMove.magnitude / Time.fixedDeltaTime;
        _previousPosition = transform.localPosition;

        _animatorController.Speed = _currentSpeed;

        if (Target != null)
        {
            if (Vector3.Distance(Target.GetPosition(), transform.position) < 1.3f)
            {
                _animatorController.NeedToUseMeleeAttack = true;
            }
            else
            {
                _animatorController.NeedToUseMeleeAttack = false;
            }
        }
        else
        {
            _animatorController.NeedToUseMeleeAttack = false;
        }
    }




    private void Subscribe()
    {
        EventsProvider.ThreeTimesPerSecond += TimeTick;
    }

    private void Dispose()
    {
        EventsProvider.ThreeTimesPerSecond -= TimeTick;
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
