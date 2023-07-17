using UnityEngine;

namespace AllPlayerActions
{
    public abstract class MovementAction : SinglePlayerAction
    {
        [SerializeField] protected float _runningSpeed = 3f;

        public virtual void StopMoving()
        {
            TurnOffMovingAnimations();
        }

        protected void MoveToTheSide(bool moveRight)
        {
            int direction = moveRight ? 1 : -1;
            ApplyMoveAnimations(moveRight);
            transform.Translate(-transform.right * direction * Time.deltaTime * _runningSpeed);

        }

        protected void TurnOffMovingAnimations()
        {
            if (_animator.GetBool("MoveRight"))
                _animator.SetBool("MoveRight", false);
            if (_animator.GetBool("MoveLeft"))
                _animator.SetBool("MoveLeft", false);
        }

        protected void ApplyMoveAnimations(bool moveRight)
        {
            if (moveRight)
            {
                if (_animator.GetBool("MoveLeft"))
                    _animator.SetBool("MoveLeft", false);
                if (!_animator.GetBool("MoveRight"))
                    _animator.SetBool("MoveRight", true);
            }
            else
            {
                if (_animator.GetBool("MoveRight"))
                    _animator.SetBool("MoveRight", false);
                if (!_animator.GetBool("MoveLeft"))
                    _animator.SetBool("MoveLeft", true);
            }
        }
    }
}
