using UnityEngine;

namespace AllPlayerActions
{
    public class PlayerActions : MonoBehaviour
    {
        private const float MAX_REACH_DIST = 1f;

        [SerializeField] private Transform _shoulderPlacement;
        private GrabAction _grabAction;
        private MoveAction _moveAction;
        private TouchHandler _touchHandler;

        private void Awake()
        {
            _touchHandler = TouchHandler.Instance;
            _grabAction = GetComponent<GrabAction>();
            _moveAction = GetComponent<MoveAction>();
        }

        private void OnEnable()
        {
            _touchHandler.TryGrab += TryGrab;
            _moveAction.ReachedPosition += TryGrab;
        }

        private void OnDisable()
        {
            _touchHandler.TryGrab -= TryGrab;
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
    }
}
