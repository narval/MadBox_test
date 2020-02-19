using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private MainSceneUIController mainSceneUIController;
    [SerializeField] private PlayerMovementController playerMovementController;

    void Start()
    {
        //Show the initial countdown and then enables player controller
    }

    void Update()
    {

    }

    public void EndGame()
    {

    }
}
