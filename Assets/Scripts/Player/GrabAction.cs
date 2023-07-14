using UnityEngine;

namespace AllPlayerActions
{
    public class GrabAction : SinglePlayerAction
    {
        public bool Grabing => _isGrabing || _inHand;
        [SerializeField] private GameObject _handRigTarget;
        [SerializeField] private GameObject _handHint;
        [SerializeField] private GameObject _itemPlacement;
        [SerializeField] private Vector3 _startHandPositionInStoreAnimation;
        [SerializeField] private Vector3 _startHintPositionInStoreAnimation;
        private GameObject _target;
        private bool _isGrabing = false;
        private bool _inHand = false;
        private bool _handInPlace = false;

        public void Grab(GameObject target)
        {
            if (Grabing)
                return;

            _handRigTarget.transform.position = target.transform.position;
            _animator.ResetTrigger("GrabItem");
            _animator.SetTrigger("GrabItem");
            _target = target;
            _isGrabing = true;
        }

        private void Update()
        {
            if (_target == null)
                return;
            if (_isGrabing)
                _handRigTarget.transform.position = _target.transform.position;

            if (_inHand && !_handInPlace)
                BringHandInStoreAnimPosition();
        }

        private void BringHandInStoreAnimPosition()
        {
            _handRigTarget.transform.localPosition =
                                 Vector3.Lerp(_handRigTarget.transform.localPosition,
                                 _startHandPositionInStoreAnimation,
                                 5 * Time.deltaTime);
            _handHint.transform.localPosition =
                                 Vector3.Lerp(_handHint.transform.localPosition,
                                 _startHintPositionInStoreAnimation,
                                 5 * Time.deltaTime);

            if (Vector3.Distance(_handRigTarget.transform.localPosition, _startHandPositionInStoreAnimation) <= 0.05f)
            {
                _animator.ResetTrigger("StoreItem");
                _animator.SetTrigger("StoreItem");
                _handInPlace = true;
            }
        }

        private void PlayerGrabbedItem()
        {
            _isGrabing = false;
            _inHand = true;

            Rigidbody _targetRb = _target.GetComponent<Rigidbody>();
            _targetRb.useGravity = false;
            _targetRb.isKinematic = true;
            _target.transform.SetParent(_itemPlacement.transform, true);
            Vector3 newTargetPos = Vector3.zero;
            newTargetPos.z += _target.GetComponent<Collider>().bounds.size.y / 2;
            _target.transform.localPosition = newTargetPos;
        }

        private void PlayerStoredItem()
        {
            Destroy(_target);
            _target = null;
            _isGrabing = false;
            _inHand = false;
            _handInPlace = false;
        }
    }
}
