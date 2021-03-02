using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public playerInventory PlayerInventory;
    // Start is called before the first frame update
    void Awake()
    {
        //pauseCanvas.SetActive(false);
        PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
        Debug.Log(PlayerInventory);
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
        saveGame(); //saving game before closing, hopefully it doesn't close too fast
        Application.Quit();
    }
    public void saveGame()
    {
        PlayerInventory.saveInfo();
    }
    public void mainmenu()
    {
        saveGame(); //saving game before loading main menu
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
