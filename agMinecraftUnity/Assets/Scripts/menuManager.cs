using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    public Button backyard;
    public Button management;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        backyard.onClick.AddListener(goToBackyard);
        management.onClick.AddListener(goToManagement);
        management.onClick.AddListener(quitGame);
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
}
