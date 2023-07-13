using UnityEngine;

namespace AllPlayerActions
{
    public abstract class SinglePlayerAction : MonoBehaviour
    {
        protected Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
