using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelUpController : MonoBehaviour
{
    public string[] words = new string[3];
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button management;
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

    public void nextPhrase()
    {
        if(word < 3)
        {
            dialogue.text = words[word];
            word++;
        } else
        {
            nextArrow.gameObject.SetActive(false);
            management.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);
        }
    }

}
