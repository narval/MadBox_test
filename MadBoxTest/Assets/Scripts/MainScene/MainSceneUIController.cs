using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIController : MonoBehaviour
{
    [SerializeField] private Button runButton;

    public delegate void ButtonClicked();
    public event ButtonClicked ButtonClickedEvent;

    public void Start()
    {
        runButton.onClick.AddListener(OnStartPressed);
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
