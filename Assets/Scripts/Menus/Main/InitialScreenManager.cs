using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ACTIVE_MENU
{
    MAIN,
    SETTINGS,
    CREDITS,
    PLAYMENU
}

public class InitialScreenManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject playMenu;
    public GameObject creditsMenu;
    public GameObject mainMenuFirstButton;
    public GameObject settingsMenuFirstButton;
    public GameObject playMenuFirstButton;

    GameObject lastSelect;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        playMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null) EventSystem.current.SetSelectedGameObject(lastSelect);
        else lastSelect = EventSystem.current.currentSelectedGameObject;
    }

    public void OpenMenu(ACTIVE_MENU menuToOpen)
    {
        CloseAllMenus();

        switch (menuToOpen)
        {
            case ACTIVE_MENU.MAIN:
                //CloseAllMenus();
                mainMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
                break;
            case ACTIVE_MENU.SETTINGS:
                //CloseAllMenus();
                settingsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(settingsMenuFirstButton);
                break;
            case ACTIVE_MENU.CREDITS:
                //CloseAllMenus();
                creditsMenu.SetActive(true);
                break;
            case ACTIVE_MENU.PLAYMENU:
                playMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(playMenuFirstButton);
                break;
            default:
                break;
        }
    }

    public void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        playMenu.SetActive(false);
    }
}
