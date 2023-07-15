using UnityEngine;

namespace AllPlayerActions
{
    public class TaskCompletedAction : SinglePlayerAction
    {
        private void OnEnable()
        {
            SingletonTask.Instance.TaskCompleted += OnTaskCompleted;
        }

        private void OnDisable()
        {
            SingletonTask.Instance.TaskCompleted -= OnTaskCompleted;
        }

        private void OnTaskCompleted()
        {
            Debug.Log(GetComponent<Animator>());
            if (_animator.GetBool("TaskCompleted"))
                return;

            _animator.SetBool("TaskCompleted", true);
        }
    }
}
