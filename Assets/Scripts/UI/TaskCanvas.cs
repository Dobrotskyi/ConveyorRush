using TMPro;
using UnityEngine;

public class TaskCanvas : MonoBehaviour
{
    private const int MIN_FOOD_AMT = 1;
    private const int MAX_FOOD_AMT = 5;

    [SerializeField] private GameObject _content;
    [SerializeField] private TextMeshProUGUI _taskText;

    private void Start()
    {
        if (_content.activeSelf)
            _content.SetActive(false);
    }

    private void OnEnable()
    {
        MainMenu.StartGame += ShowTask;
        SingletonTask.Instance.FoodAmtUpdated += ShowTask;
        SingletonTask.Instance.TaskCompleted += HideTask;
    }

    private void OnDisable()
    {
        MainMenu.StartGame -= ShowTask;
        SingletonTask.Instance.FoodAmtUpdated -= ShowTask;
        SingletonTask.Instance.TaskCompleted -= HideTask;
    }

    private void ShowTask()
    {
        if (!_content.activeSelf)
            _content.SetActive(true);

        _taskText.text = SingletonTask.Instance.ToString();
    }

    private void HideTask()
    {
        _content.SetActive(false);
    }
}
