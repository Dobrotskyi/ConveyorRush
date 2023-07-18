using UnityEngine;

namespace AllPlayerActions
{
    public class GrabAction : SinglePlayerAction
    {
        private const float MOVEMENT_SPEED = 5f;

        public bool ActionFinished { private set; get; } = true;

        [SerializeField] private GameObject _handRigTarget;
        [SerializeField] private GameObject _handHint;
        [SerializeField] private GameObject _itemPlacement;
        [SerializeField] private Vector3 _startHandPositionInStoreAnimation;
        [SerializeField] private Vector3 _startHintPositionInStoreAnimation;
        [SerializeField] private FoodBucket _foodBucket;
        private GameObject _target;
        private bool _isGrabing = false;
        private bool _inHand = false;
        private bool _handInPlace = false;

        public void Grab(GameObject target)
        {
            if (!ActionFinished)
                return;

            _handRigTarget.transform.position = target.transform.position;
            _animator.ResetTrigger("GrabItem");
            _animator.SetTrigger("GrabItem");
            _target = target;
            _isGrabing = true;
            ActionFinished = false;
        }

        private void Update()
        {
            if (!ActionFinished)
                if (_target == null)
                {
                    TargetBecameNull();
                    return;
                }

            if (_target == null)
                return;

            if (_isGrabing)
                _handRigTarget.transform.position = _target.transform.position;

            if (_inHand && !_handInPlace)
                BringHandInAnimPosition();
        }

        private void TargetBecameNull()
        {
            _animator.ResetTrigger("TargetBecameNull");
            _animator.SetTrigger("TargetBecameNull");
            SetActionFinished();
        }

        private void BringHandInAnimPosition()
        {
            _handRigTarget.transform.localPosition =
                                 Vector3.Lerp(_handRigTarget.transform.localPosition,
                                 _startHandPositionInStoreAnimation,
                                 MOVEMENT_SPEED * Time.deltaTime);
            _handHint.transform.localPosition =
                                 Vector3.Lerp(_handHint.transform.localPosition,
                                 _startHintPositionInStoreAnimation,
                                 MOVEMENT_SPEED * Time.deltaTime);

            if (Vector3.Distance(_handRigTarget.transform.localPosition, _startHandPositionInStoreAnimation) <= 0.05f)
            {
                _handInPlace = true;

                FoodTypes itemFoodType = _target.GetComponent<FoodMarker>().Type;
                if (itemFoodType == SingletonTask.Instance.FoodToCollect)
                {
                    _animator.ResetTrigger("StoreItem");
                    _animator.SetTrigger("StoreItem");
                }
                else
                {
                    _animator.ResetTrigger("ThrowAwayItem");
                    _animator.SetTrigger("ThrowAwayItem");
                }
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
            _foodBucket.WaitForObject(_target);
            LetGoItem(_target);
        }

        private void PlayerThrowAwayItem() => LetGoItem(_target.gameObject);

        private void LetGoBucket()
        {
            GameObject bucketClone = Instantiate(_foodBucket.gameObject);
            bucketClone.transform.SetParent(null);
            bucketClone.transform.position = _foodBucket.transform.position;
            bucketClone.transform.rotation = _foodBucket.transform.rotation;
            Rigidbody cloneRb = bucketClone.GetComponent<Rigidbody>();
            cloneRb.isKinematic = false;
            cloneRb.useGravity = true;
            _foodBucket.gameObject.SetActive(false);
        }

        private void LetGoItem(GameObject item)
        {
            item.transform.SetParent(null, true);

            if (item.TryGetComponent<Rigidbody>(out Rigidbody itemRb))
            {
                itemRb.useGravity = true;
                itemRb.isKinematic = false;
            }
        }

        private void SetActionFinished()
        {
            ActionFinished = true;
            _target = null;
            _isGrabing = false;
            _inHand = false;
            _handInPlace = false;
        }
    }
}
