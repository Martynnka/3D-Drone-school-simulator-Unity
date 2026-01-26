using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenuManager : MonoBehaviour
{
    public GameObject ESCMENU;
    public MonoBehaviour playerMovement; 

    private bool isMenuOpen = false;

    void Start()
    {
        ESCMENU.SetActive(false);
        ResumeGame(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
                ResumeGame();
            else
                OpenMenu();
        }
    }

    void OpenMenu()
    {
        ESCMENU.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        if (playerMovement)
            playerMovement.enabled = false;

        isMenuOpen = true;
    }

    public void ResumeGame()
    {
        ESCMENU.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        if (playerMovement)
            playerMovement.enabled = true;

        isMenuOpen = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

