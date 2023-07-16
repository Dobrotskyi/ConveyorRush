using System;
using UnityEngine;

namespace AllPlayerActions
{
    public class MoveAction : SinglePlayerAction
    {
        public event Action<GameObject> ReachedPosition;

        private const float Left_Border_X = -2.8f;
        private const float Right_Border_X = 0.2f;

        [SerializeField] private float _runningSpeed = 5f;
        private FoodMarker _targetFood;
        private Vector3 _newPos;
        private bool _movingToTarget;
        private bool _moveRight = false;

        public void StartMovingTo(Transform target)
        {
            TurnOffMovingAnimations();

            _targetFood = target.GetComponent<FoodMarker>();
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
            _targetFood = null;
            TurnOffMovingAnimations();
        }

        private void Update()
        {
            if (_movingToTarget)
            {
                if (!_targetFood.OnConveyor)
                {
                    StopMoving();
                    return;
                }

                int direction = _moveRight ? 1 : -1;
                transform.Translate(-transform.right * direction * Time.deltaTime * _runningSpeed);
                if (Vector3.Distance(transform.position, _newPos) <= 0.1f)
                {
                    ReachedPosition?.Invoke(_targetFood.gameObject);
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
