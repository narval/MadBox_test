using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuUIController mainMenuUIController;
    [SerializeField] private string gameSceneName;

    void Start()
    {
        mainMenuUIController.ButtonClickedRegister(StartGame);
    }

    void Update()
    {
        
    }

    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
}
