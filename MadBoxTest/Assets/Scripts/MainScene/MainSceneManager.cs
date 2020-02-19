using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private MainSceneUIController mainSceneUIController;
    [SerializeField] private PlayerMovementController playerMovementController;

    void Start()
    {

    }

    void Update()
    {
        playerMovementController.Run(Input.GetMouseButton(0) || Input.touchCount > 0);
        Debug.Log(Input.GetMouseButton(0) || Input.touchCount > 0);
    }
}
