using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainSceneUIController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button tryAgainButton;

    public void Start()
    {
        tryAgainButton.onClick.AddListener(RestartLevel);
    }

    void Update()
    {
        
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }

    public void OnWin()
    {
        winPanel.SetActive(true);
    }

    public void OnLose()
    {
        losePanel.SetActive(true);
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
