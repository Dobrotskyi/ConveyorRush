using System;
using UnityEngine;

namespace AllPlayerActions
{
    public class MoveToTargetAction : MovementAction
    {
        private const float Left_Border_X = -2.8f;
        private const float Right_Border_X = 0.2f;

        public event Action<GameObject> ReachedPosition;
        public bool MovingToTarget { private set; get; }

        private FoodMarker _targetFood;
        private Vector3 _newPos;

        private bool _movingToTargetRight = false;

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
                _movingToTargetRight = false;
            }
            else
            {
                _movingToTargetRight = true;
            }

            MovingToTarget = true;
        }

        public override void StopMoving()
        {
            MovingToTarget = false;
            _targetFood = null;
            TurnOffMovingAnimations();
        }

        private void Update()
        {
            if (MovingToTarget)
            {
                if (_targetFood == null || !_targetFood.OnConveyor)
                {
                    StopMoving();
                    return;
                }

                MoveToTheSide(_movingToTargetRight);

                if (Vector3.Distance(transform.position, _newPos) <= 0.1f)
                {
                    ReachedPosition?.Invoke(_targetFood.gameObject);
                    StopMoving();
                }
            }
        }
    }
}
