using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    public GameObject choices;
    public Button backyard;
    public Button management;
    public Button quit;
    public GameObject options;
    public Button newGame;
    public Button continueGame;
    public Button menuBack;
    // Start is called before the first frame update
    void Start()
    {
        backyard.onClick.AddListener(openOptions);
        management.onClick.AddListener(goToManagement);
        quit.onClick.AddListener(quitGame);
        newGame.onClick.AddListener(startNewGame);
        continueGame.onClick.AddListener(goToBackyard);
        menuBack.onClick.AddListener(mainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void goToBackyard()
    {
        SceneManager.LoadSceneAsync("Backyard");
    }
    private void goToManagement()
    {
        SceneManager.LoadSceneAsync("Management");
    }
    private void quitGame()
    {
        Application.Quit();
    }
    private void mainMenu()
    {
        options.gameObject.SetActive(false);
        choices.gameObject.SetActive(true);
    }
    private void openOptions()
    {
        options.gameObject.SetActive(true);
        choices.gameObject.SetActive(false);
    }
    private void startNewGame()
    {
        SceneManager.LoadSceneAsync("Intro");
    }
}
