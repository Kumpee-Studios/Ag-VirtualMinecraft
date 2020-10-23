using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuController : MonoBehaviour
{
    public GameObject pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        //pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
                pauseCanvas.SetActive(false);
                resume();
        }
    }
    public void quitGame()
    {
        saveGame();
        Application.Quit();
    }
    public void saveGame()
    {
        Debug.Log("I'll get there eventually");
    }
    public void mainmenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void resume()
    {
        Time.timeScale = 1f;
    }
}
