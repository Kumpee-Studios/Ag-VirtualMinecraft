using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endGameController : MonoBehaviour
{
    public string[] words = new string[2];
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button keepPlaying; //not implementing this for now
    [SerializeField] private Button nextArrow;
    [SerializeField] private TextMeshProUGUI dialogue;
    private int word = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextPhrase() //make it public so it can be seen from inspector
    {
        if (word < 2)
        {
            dialogue.text = words[word];
            word++;
        }
        else
        {
            nextArrow.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
    }
    public void returnToTitle()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
