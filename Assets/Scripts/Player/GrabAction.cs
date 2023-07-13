using UnityEngine;

namespace AllPlayerActions
{
    public class GrabAction : SinglePlayerAction
    {
        public bool Grabing { private set; get; }
        [SerializeField] private GameObject _handRigTarget;
        [SerializeField] private GameObject _itemPlacement;
        private GameObject _target;

        public void Grab(GameObject target)
        {
            if (Grabing)
                return;

            Grabing = true;
            _handRigTarget.transform.position = target.transform.position;
            _animator.ResetTrigger("GrabItem");
            _animator.SetTrigger("GrabItem");
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
                return;
            _handRigTarget.transform.position = _target.transform.position;
        }

        private void PlayerGrabbedItem()
        {
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
            Grabing = false;
        }
    }
}
