namespace AllPlayerActions
{
    public class TaskCompletedAction : SinglePlayerAction
    {
        private void Start()
        {
            Task.Instance.TaskCompleted += OnTaskCompleted;
        }

        private void OnTaskCompleted()
        {
            if (_animator.GetBool("TaskCompleted"))
                return;

            _animator.SetBool("TaskCompleted", true);
        }
    }
}
