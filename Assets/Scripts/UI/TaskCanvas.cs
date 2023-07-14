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
        if (_content.activeSelf)
            _content.SetActive(false);
    }

    private void OnDisable()
    {
        MainMenu.Instance.StartGame -= ShowTask;
    }

    private void ShowTask()
    {
        _content.SetActive(true);
        _taskText.text = GenerateTask();
    }

    private string GenerateTask()
    {
        StringBuilder sb = new();
        string[] foodNames = Enum.GetNames(typeof(FoodTypes));
        System.Random rnd = new();
        string randomFoodName = foodNames[rnd.Next(foodNames.Length)] + "s";
        sb.AppendLine($"Collect {rnd.Next(MIN_FOOD_AMT, MAX_FOOD_AMT + 1)} {randomFoodName}");

        return sb.ToString();
    }


}
