using UnityEngine;

namespace AllPlayerActions
{
    public class PlayerActions : MonoBehaviour
    {
        private const float MAX_REACH_DIST = 1f;

        [SerializeField] private Transform _shoulderPlacement;
        private GrabAction _grabAction;
        private MoveAction _moveAction;

        private void Awake()
        {
            _grabAction = GetComponent<GrabAction>();
            _moveAction = GetComponent<MoveAction>();
        }

        private void OnEnable()
        {
            ControllsCanvas.MoveLeftButtonPressed += MoveLeft;
            ControllsCanvas.MoveRightButtonPressed += MoveRight;
            TouchHandler.TryGrab += TryGrab;
            _moveAction.ReachedPosition += TryGrab;
        }

        private void OnDisable()
        {
            ControllsCanvas.MoveLeftButtonPressed -= MoveLeft;
            ControllsCanvas.MoveRightButtonPressed -= MoveRight;
            TouchHandler.TryGrab -= TryGrab;
            _moveAction.ReachedPosition -= TryGrab;
        }

        private void TryGrab(GameObject target)
        {
            if (_grabAction.Grabing)
                return;

            if (Vector3.Distance(target.transform.position, _shoulderPlacement.position) < MAX_REACH_DIST)
            {
                _moveAction.StopMoving();
                _grabAction.Grab(target);
            }
            else
                _moveAction.StartMovingTo(target.transform);
        }

        private void MoveLeft()
        {
            if (!_grabAction.Grabing)
                _moveAction.MoveToTheLeft();
        }

        private void MoveRight()
        {
            if (!_grabAction.Grabing)
                _moveAction.MoveToTheRight();
        }
    }
}
