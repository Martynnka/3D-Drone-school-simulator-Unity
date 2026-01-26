using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject TutorialPanel;
    public Button TutorialpanelExit;
    public Button TutorialpanelOn;



    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

   public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenTutorial()
    {
        TutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        TutorialPanel.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
