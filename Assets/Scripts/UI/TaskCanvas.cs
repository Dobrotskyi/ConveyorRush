using System;
using System.Text;
using TMPro;
using UnityEngine;

public class TaskCanvas : MonoBehaviour
{
    private const int MIN_FOOD_AMT = 1;
    private const int MAX_FOOD_AMT = 5;

    [SerializeField] private GameObject _content;
    [SerializeField] private TextMeshProUGUI _taskText;

    private void OnEnable()
    {
        MainMenu.Instance.StartGame += ShowTask;
        Task.Instance.FoodAmtUpdated += ShowTask;
        Task.Instance.TaskCompleted += HideTask;
        if (_content.activeSelf)
            _content.SetActive(false);
    }

    private void OnDisable()
    {
        MainMenu.Instance.StartGame -= ShowTask;
        Task.Instance.FoodAmtUpdated -= ShowTask;
        Task.Instance.TaskCompleted -= HideTask;
    }

    private void ShowTask()
    {
        if (!_content.activeSelf)
            _content.SetActive(true);

        _taskText.text = Task.Instance.ToString();
    }

    private void HideTask()
    {
        _content.SetActive(false);
    }
}
