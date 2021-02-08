using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject seedsCanvas;
    public Button[] seedButtons = new Button[6];
    public GameObject produceCanvas;
    public Button[] produceButtons = new Button[6];
    public GameObject buySeedsCanvas;
    public playerController playercontroller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas.activeInHierarchy && (seedsCanvas.activeSelf || produceCanvas.activeSelf || buySeedsCanvas.activeSelf))
                {
                    seedsCanvas.SetActive(false);
                    produceCanvas.SetActive(false);
                    buySeedsCanvas.SetActive(false);
                    hideSeeds();
                    hideProduce();
                    hideSeedSeller();
                }
            else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !pauseCanvas.activeInHierarchy)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
    }
    public void showSeeds()
    {
        if (seedsCanvas.activeSelf== false)
        {
            seedsCanvas.SetActive(true); //making sure there's only one overlay on screen on a time to avoid clutter
            produceCanvas.SetActive(false);
        } else
        {
            hideSeeds();
        }
    }
    public void showProduce()
    {
        if (produceCanvas.activeSelf== false)
        {
            produceCanvas.SetActive(true);
            seedsCanvas.SetActive(false);
            playercontroller.canDoStuff = false;
        } else
        {
            hideProduce();
        }
    }
    public void showSeedSeller()
    {
        hideSeeds(); //making sure they don't have any other tabs open
        hideProduce();
        buySeedsCanvas.SetActive(true);
        playercontroller.canDoStuff = false;
    }
    public void hideSeeds() //splitting these methods up so that I can use them here and call them whenever an item is selected from one of these menus
    {
        playercontroller.seedMenu = false;
        playercontroller.canDoStuff = true;
        seedsCanvas.SetActive(false);
        uninteractableSeeds();
    }
    public void hideProduce()
    {
        playercontroller.canDoStuff = true;
        produceCanvas.SetActive(false);
        uninteractableProduce();
    }
    public void hideSeedSeller()
    {
        buySeedsCanvas.SetActive(false);
        playercontroller.canDoStuff = true;
    }
    public void interactableSeeds()
    {
        foreach (Button button in seedButtons)
        {
            button.interactable = true;
        }
    }
    public void uninteractableSeeds()
    {
        foreach(Button button in seedButtons)
        {
            button.interactable = false;
        }
    }
    public void interactableProduce()
    {
        foreach (Button button in produceButtons)
        {
            button.interactable = true;
        }
    }
    public void uninteractableProduce()
    {
        foreach(Button button in produceButtons)
        {
            button.interactable = false;
        }
    }
}
