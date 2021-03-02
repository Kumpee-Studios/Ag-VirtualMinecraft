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
    public playerInventory playerinventory;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        playerinventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
        seedButtons[0].onClick.AddListener(() => playerinventory.plantMethod(0));
        seedButtons[1].onClick.AddListener(() => playerinventory.plantMethod(1));
        seedButtons[2].onClick.AddListener(() => playerinventory.plantMethod(2));
        seedButtons[3].onClick.AddListener(() => playerinventory.plantMethod(3));
        seedButtons[4].onClick.AddListener(() => playerinventory.plantMethod(4));
        seedButtons[5].onClick.AddListener(() => playerinventory.plantMethod(5));
        produceButtons[0].onClick.AddListener(() => playerinventory.sellProduce(0));
        produceButtons[1].onClick.AddListener(() => playerinventory.sellProduce(1));
        produceButtons[2].onClick.AddListener(() => playerinventory.sellProduce(2));
        produceButtons[3].onClick.AddListener(() => playerinventory.sellProduce(3));
        produceButtons[4].onClick.AddListener(() => playerinventory.sellProduce(4));
        produceButtons[5].onClick.AddListener(() => playerinventory.sellProduce(5));
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
            //interactableProduce(); //remember to make sure they can click the produce buttons
            //this should've been called from the interaction controller, hrmm
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
        Debug.Log("Interactable Produce called");
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
