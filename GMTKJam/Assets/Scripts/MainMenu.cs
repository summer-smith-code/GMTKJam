using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainMenu != null) 
        ReturnMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameFirst()
    {
        SceneManager.LoadScene("Cinematic");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }   

    public void ReturnMainMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void ReturnMenuScene()
    {
        SceneManager.LoadScene(0);
    }


}
