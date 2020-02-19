using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneInputController : MonoBehaviour
{
    [SerializeField] private MainMenuUIController mainMenuUIController;

    private bool canRun;

    void Start()
    {
        mainMenuUIController.ButtonClickedRegister(RunButtonPressed);
    }

    void Update()
    {
        
    }

    void RunButtonPressed()
    {
        if (canRun)
        {

        }
        //Run
    }
}
