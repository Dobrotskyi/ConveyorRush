using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllsCanvas : MonoBehaviour
{
    public static event Action MoveRightButtonPressed;
    public static event Action MoveLeftButtonPressed;

    [SerializeField] private GameObject _content;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private void OnEnable()
    {
        MainMenu.StartGame += ShowContent;
        SingletonTask.Instance.TaskCompleted += HideContent;
        TouchHandler.ControllsButtonPressed += RecognizeDirection;
    }

    private void OnDisable()
    {
        MainMenu.StartGame -= ShowContent;
        SingletonTask.Instance.TaskCompleted -= HideContent;
        TouchHandler.ControllsButtonPressed -= RecognizeDirection;
    }

    private void ShowContent()
    {
        if (!_content.activeSelf)
            _content.SetActive(true);
    }

    private void HideContent()
    {
        if (_content.activeSelf)
            _content.SetActive(false);
    }

    private void RecognizeDirection(Button button)
    {
        if (button.Equals(_leftButton))
            MoveLeftButtonPressed?.Invoke();
        else
            MoveRightButtonPressed?.Invoke();
    }
}
