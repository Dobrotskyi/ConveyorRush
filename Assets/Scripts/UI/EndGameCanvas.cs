using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _content;

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        if (_content.activeSelf)
            _content.SetActive(false);
    }

    private void OnEnable()
    {
        SingletonTask.Instance.TaskCompleted += ShowEndMenu;
    }

    private void OnDisable()
    {
        SingletonTask.Instance.TaskCompleted -= ShowEndMenu;
    }

    private void ShowEndMenu()
    {
        _content.SetActive(true);
    }
}
