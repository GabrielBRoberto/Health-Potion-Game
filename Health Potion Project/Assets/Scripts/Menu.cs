using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private MenuType menuType;
    [SerializeField]
    private MenuArea menuArea;

    [SerializeField]
    private GameObject menuGameObject;
    [SerializeField]
    private GameObject settingsGameObject;
    [SerializeField]
    private GameObject creditsGameObject;

    public bool onMenu = false;

    private void Update()
    {
        switch (menuType)
        {
            case MenuType.Main:
                MainMenuLogic();
                break;
            case MenuType.Pause:
                PauseMenuLogic();
                break;
        }
    }

    private void MainMenuLogic()
    {
        switch (menuArea)
        {
            case MenuArea.Menu:
                ActivateGameObject(menuGameObject);
                break;
            case MenuArea.Settings:
                ActivateGameObject(settingsGameObject);
                break;
            case MenuArea.Credits:
                ActivateGameObject(creditsGameObject);
                break;
        }
    }
    private void PauseMenuLogic()
    {
        if (onMenu)
        {
            switch (menuArea)
            {
                case MenuArea.Menu:
                    ActivateGameObject(menuGameObject);
                    break;
                case MenuArea.Settings:
                    ActivateGameObject(settingsGameObject);
                    break;
                case MenuArea.Credits:
                    ActivateGameObject(creditsGameObject);
                    break;
            }
        }
        else
        {
            DesactivateAllGameObject();
        }
    }

    #region Geral Logic

    private void ActivateGameObject(GameObject ObjectToActivate)
    {
        DesactivateAllGameObject();
        ObjectToActivate.SetActive(true);
    }
    private void DesactivateAllGameObject()
    {
        menuGameObject.SetActive(false);
        settingsGameObject.SetActive(false);
        creditsGameObject.SetActive(false);
    }

    #region Button Logic

    public void SettingsClick()
    {
        menuArea = MenuArea.Settings;
    }
    public void CreditsClick()
    {
        menuArea = MenuArea.Credits;
    }
    public void BackClick()
    {
        menuArea = MenuArea.Menu;
    }

    public void PlayClick()
    {
        SceneManager.LoadScene("Level");
    }
    public void MenuClick()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
    public void ExitClick()
    {
        Application.Quit();
    }

    #endregion

    #endregion
}

public enum MenuType
{
    Main,
    Pause
}
public enum MenuArea
{
    Menu,
    Settings,
    Credits
}