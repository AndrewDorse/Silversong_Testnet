using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Silversong.Game
{
    public class OtherHeroMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HeroAnimatorController _animatorController;

        private float _currentSpeed;

        private float _timeElapsed;

        private Vector3 _startPosition = Vector3.zero;
        private Vector3 _previousPosition;
        private Vector3 _expectedPosition;
        private Vector3 _expectedDirection;


        private void Update()
        {
            _timeElapsed += Time.deltaTime;
            float fractionOfJourney = _timeElapsed / 0.4f;


            Vector3 newDirection = Vector3.RotateTowards(transform.forward, _expectedDirection, 0.1f, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            SetSpeed();

            if (Vector3.Distance(_expectedPosition, transform.position) >= 2.5f)
            {
                _agent.Warp(_expectedPosition);
                _previousPosition = transform.position;
                _timeElapsed = 0.4f;
                _startPosition = transform.position;
                return;
            }

            if ((_expectedPosition - _startPosition).sqrMagnitude > 0.05f)
            {
                if (_expectedPosition != _startPosition)
                {
                    transform.position = Vector3.Lerp(_startPosition, _expectedPosition, fractionOfJourney);
                }
                return;
            }
        }

        private void SetSpeed()
        {
            Vector3 curMove = transform.position - _previousPosition;
            _currentSpeed = curMove.magnitude / Time.deltaTime;
            _previousPosition = transform.position;
            _animatorController.Speed = _currentSpeed;
        }

        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            _expectedPosition = position;
            _expectedDirection = direction;
            _startPosition = transform.position;
            _timeElapsed = 0;
        }
    }
}