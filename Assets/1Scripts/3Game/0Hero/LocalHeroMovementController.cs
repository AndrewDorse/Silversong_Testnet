using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Silversong.Game
{
    public class LocalHeroMovementController
    {
        private CharacterController _characterController;
        private NavMeshAgent _navMeshAgent;
        private HeroAnimatorController _animatorController;


        private float _speed = 5f; //Temp TODO

        private float _currentSpeed;

        private Vector3 _moveDirection;
        private Vector3 _previousPosition;

        public LocalHeroMovementController(CharacterController characterController, NavMeshAgent navMeshAgent, HeroAnimatorController animatorController)
        {
            _characterController = characterController;
            _navMeshAgent = navMeshAgent;
            _animatorController = animatorController;

            EventsProvider.OnJoystickMove += Move;

        }


        private void Move(Vector2 direction)
        {


            _moveDirection = new Vector3(direction.x, 0, direction.y);

            Quaternion targetRotation = _moveDirection != Vector3.zero ? Quaternion.LookRotation(_moveDirection)
                : _characterController.transform.rotation;


            _characterController.transform.rotation = targetRotation;

            _moveDirection = _moveDirection * _speed * 50 * Time.deltaTime;





            _moveDirection.y = _moveDirection.y - (10f * Time.fixedDeltaTime);

            _characterController.Move(_moveDirection * Time.fixedDeltaTime);


            CountCurrentSpeed();
        }

        private void CountCurrentSpeed()
        {
            Vector3 curMove = _characterController.transform.localPosition - _previousPosition;
            _currentSpeed = curMove.magnitude / Time.fixedDeltaTime;
            _previousPosition = _characterController.transform.localPosition;

            _animatorController.Speed = _currentSpeed;

        }

        public void Dispose()
        {
            EventsProvider.OnJoystickMove -= Move;
        }
    }
}