using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button startButton;

    public delegate void ButtonClicked();
    public event ButtonClicked ButtonClickedEvent;

    public void Start()
    {
        startButton.onClick.AddListener(OnStartPressed);
    }

    void Update()
    {
        
    }

    public void ButtonClickedRegister(ButtonClicked startCallback)
    {
        ButtonClickedEvent += startCallback;
    }

    public void OnStartPressed()
    {
        ButtonClickedEvent.Invoke();
    }
}
