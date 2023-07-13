using UnityEngine;

namespace AllPlayerActions
{
    public class GrabAction : SinglePlayerAction
    {
        [SerializeField] private GameObject _handRigTarget;

        public void Grab(GameObject target)
        {
            _handRigTarget.transform.position = target.transform.position;
            _animator.ResetTrigger("GrabItem");
            _animator.SetTrigger("GrabItem");
        }
    }
}
