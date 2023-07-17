using UnityEngine;

namespace AllPlayerActions
{
    public class PlayerActions : MonoBehaviour
    {
        private const float MAX_REACH_DIST = 1f;

        [SerializeField] private Transform _shoulderPlacement;
        private GrabAction _grabAction;

        private SideMovementAction _sideMovementAction;
        private MoveToTargetAction _toTargetAction;

        private void Awake()
        {
            _grabAction = GetComponent<GrabAction>();
            _sideMovementAction = GetComponent<SideMovementAction>();
            _toTargetAction = GetComponent<MoveToTargetAction>();
        }

        private void OnEnable()
        {
            ControllsCanvas.MoveLeftButtonPressed += MoveLeft;
            ControllsCanvas.MoveRightButtonPressed += MoveRight;
            TouchHandler.TryGrab += TryGrab;
            TouchHandler.ControllButtonReleased += OnControllButtonReleased;
            _toTargetAction.ReachedPosition += TryGrab;
        }

        private void OnDisable()
        {
            ControllsCanvas.MoveLeftButtonPressed -= MoveLeft;
            ControllsCanvas.MoveRightButtonPressed -= MoveRight;
            TouchHandler.TryGrab -= TryGrab;
            TouchHandler.ControllButtonReleased -= OnControllButtonReleased;
        }

        private void TryGrab(GameObject target)
        {
            if (!_grabAction.ActionFinished)
                return;

            if (Vector3.Distance(target.transform.position, _shoulderPlacement.position) < MAX_REACH_DIST)
            {
                _toTargetAction.StopMoving();
                _grabAction.Grab(target);
            }
            else
                _toTargetAction.StartMovingTo(target.transform);
        }

        private void MoveLeft()
        {
            if (_grabAction.ActionFinished && !_toTargetAction.MovingToTarget)
                _sideMovementAction.MoveToTheLeft();
        }

        private void MoveRight()
        {
            if (_grabAction.ActionFinished && !_toTargetAction.MovingToTarget)
                _sideMovementAction.MoveToTheRight();
        }

        private void OnControllButtonReleased() => _sideMovementAction.StopMoving();
    }
}
