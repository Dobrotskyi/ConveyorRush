using System;
using UnityEngine;

public class MainMenu : MonoBehaviourSingleton<MainMenu>
{
    public event Action StartGame;
    [SerializeField] private GameObject _mainMenuContent;

    public void StartPlaying()
    {
        StartGame?.Invoke();
        _mainMenuContent.gameObject.SetActive(false);
    }
}
