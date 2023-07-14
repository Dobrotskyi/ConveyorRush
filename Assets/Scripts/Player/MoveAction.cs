using System;
using UnityEngine;

namespace AllPlayerActions
{
    public class MoveAction : SinglePlayerAction
    {
        private const float Left_Border_X = -2.8f;
        private const float Right_Border_X = 0.2f;

        public event Action<GameObject> ReachedPosition;

        [SerializeField] private float _runningSpeed = 5f;
        private GameObject _target;
        private Vector3 _newPos;
        private bool _movingToTarget;
        private bool _moveRight = false;

        public void StartMovingTo(Transform target)
        {
            TurnOffMovingAnimations();

            _target = target.gameObject;
            _newPos = transform.position;
            _newPos.x = target.position.x;

            if (_newPos.x < Left_Border_X || _newPos.x > Right_Border_X)
            {
                StopMoving();
                return;
            }

            if (transform.position.x < _newPos.x)
            {
                _animator.SetBool("MoveLeft", true);
                _moveRight = false;
            }
            else
            {
                _animator.SetBool("MoveRight", true);
                _moveRight = true;
            }

            _movingToTarget = true;
        }

        public void StopMoving()
        {
            _movingToTarget = false;
            _target = null;
            TurnOffMovingAnimations();
        }

        private void Update()
        {
            if (_movingToTarget)
            {
                int direction = _moveRight ? 1 : -1;
                transform.Translate(-transform.right * direction * Time.deltaTime * _runningSpeed);
                if (Vector3.Distance(transform.position, _newPos) <= 0.1f)
                {
                    ReachedPosition?.Invoke(_target);
                    StopMoving();
                }
            }
        }

        private void TurnOffMovingAnimations()
        {
            if (_animator.GetBool("MoveRight"))
                _animator.SetBool("MoveRight", false);
            if (_animator.GetBool("MoveLeft"))
                _animator.SetBool("MoveLeft", false);
        }
    }
}
