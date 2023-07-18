using Cinemachine;
using UnityEngine;

public class EndGameCameraAction : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameCamera;
    [SerializeField] private CinemachineVirtualCamera _endGameCamera;

    private void OnEnable()
    {
        SingletonTask.Instance.TaskCompleted += ToggleEndGameCamera;
    }

    private void OnDisable()
    {
        SingletonTask.Instance.TaskCompleted -= ToggleEndGameCamera;
    }

    private void ToggleEndGameCamera()
    {
        if (_gameCamera.gameObject.activeSelf)
            _gameCamera.gameObject.SetActive(false);
        if (!_endGameCamera.gameObject.activeSelf)
            _endGameCamera.gameObject.SetActive(true);
    }
}
