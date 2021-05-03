using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject seedsCanvas;
    public GameObject cityCanvas;
    public Button[] seedButtons = new Button[6];
    public GameObject produceCanvas;
    public Button[] produceButtons = new Button[6];
    public GameObject buySeedsCanvas;
    public Button[] buySeedsButtons = new Button[6];
    public playerController playercontroller;
    public playerInventory playerinventory;
    public GameObject seedsButton;
    public GameObject produceButton;
    public GameObject endGameButton;
    public GameObject siloCanvas;
    public GameObject siloButton;
    private interactionController hoe;
    [SerializeField] private npcSpawner spawner;
    public bool canEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setUpGame());
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas.activeInHierarchy && (seedsCanvas.activeSelf || produceCanvas.activeSelf || buySeedsCanvas.activeSelf) && !hoe.inTown)
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
            siloCanvas.SetActive(false);
            if (hoe.inTown)
            {
                interactableProduce(); //I'll need to adjust this later to determine whether htye're at the city or not
                                       //just use an 'at city' boolean or something similar
            }
        } else
        {
            hideProduce();
        }

    }
    public void showSeedSeller()
    {
        buySeedsCanvas.SetActive(true);
        siloCanvas.SetActive(false);
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
        if(hoe.inTown)
        {
            siloButton.SetActive(true);
        }
    }
    public void uninteractableProduce()
    {
        foreach(Button button in produceButtons)
        {
            button.interactable = false;
        }
        siloButton.SetActive(false);
    }
    public void goToCity()
    {
        seedsButton.gameObject.SetActive(false); //deactivating pause menu buttons in upper left hand side of screen
        produceButton.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(false); //if it's not available it won't hurt anything
        StartCoroutine(cityMenu(true));
    }    
    public void returnFromCity()
    {
        cityCanvas.SetActive(false);
        StartCoroutine(cityMenu(false));
    }
    private IEnumerator cityMenu(bool toCity)
    {
        yield return new WaitForSeconds(4f); //same amount of time it takes to drive the car to or from the city
        if (toCity)
        {
            cityCanvas.SetActive(true); //...so...this should hypothetically flip it on and off
            hoe.inTown = true;
        }
        else
        {
            if(canEnd)
            {
                endGameButton.SetActive(true);
            }
            seedsButton.gameObject.SetActive(true); //deactivating pause menu buttons in upper left hand side of screen
            produceButton.gameObject.SetActive(true);
            hoe.inTown = false;
        }
    }
    public void goMarket() //method to adjust camera to show market scene
    {
        playercontroller.gameObject.transform.position = new Vector3(playercontroller.gameObject.transform.position.x - 40f, playercontroller.gameObject.transform.position.y, -10);
        cityCanvas.SetActive(false);
        spawner.spawnNPCS();
        StartCoroutine(returnFromMarket());
    }
    public void goSilo() //emthod to adjust camera to show silo scene
    {
        playercontroller.gameObject.transform.position = new Vector3(playercontroller.gameObject.transform.position.x + 35f, playercontroller.gameObject.transform.position.y, -10);
        cityCanvas.SetActive(false);
        siloCanvas.SetActive(true);
    }
    public void returnCity() //only needs to be called from the silo screen
    { //returns player from silo and moves view to city
        playercontroller.gameObject.transform.position = new Vector3(playercontroller.gameObject.transform.position.x - 35f, playercontroller.gameObject.transform.position.y, -10);
        cityCanvas.SetActive(true);
        siloCanvas.SetActive(false);
    }
    public void returnSilo() //don't want it to shift the camera again so creating method to show silo menu after they buy seeds/sell produce
    {
        siloCanvas.SetActive(true);
        buySeedsCanvas.SetActive(false);
        produceCanvas.SetActive(false);
        uninteractableProduce();
    }
    private IEnumerator returnFromMarket()
    {
        yield return new WaitForSeconds(30f); //max time would be 40, seems unlikely
        playercontroller.gameObject.transform.position = new Vector3(playercontroller.gameObject.transform.position.x + 40f, playercontroller.gameObject.transform.position.y, -10);
        cityCanvas.SetActive(true);
    }

    public void endGame()
    {
        playerinventory.saveInfo(); //saving game
        SceneManager.LoadSceneAsync("endGame");
    }

    private IEnumerator setUpGame() //doing it this way thinking maybe there are button errors because it's grabbing the player who is deactivated.
    { //this wasn't the source of the error, error was having an empty onclick list in the inspector
        //I'm leaving this here as a 'best practice.' There is a slim chance that the camera object doesn't deactivate the correct player and it breaks the whole game
        //this acts as insurance against said event happening
        yield return new WaitForSeconds(0.5f);
        hoe = GameObject.FindGameObjectWithTag("hoe").GetComponent<interactionController>();
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        playerinventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
        seedButtons[0].onClick.AddListener(() => playerinventory.plantMethod(0));
        seedButtons[1].onClick.AddListener(() => playerinventory.plantMethod(3));
        seedButtons[2].onClick.AddListener(() => playerinventory.plantMethod(1));
        seedButtons[3].onClick.AddListener(() => playerinventory.plantMethod(4));
        seedButtons[4].onClick.AddListener(() => playerinventory.plantMethod(2));
        seedButtons[5].onClick.AddListener(() => playerinventory.plantMethod(5));
        produceButtons[0].onClick.AddListener(() => playerinventory.sellProduce(0));
        produceButtons[1].onClick.AddListener(() => playerinventory.sellProduce(3));
        produceButtons[2].onClick.AddListener(() => playerinventory.sellProduce(1));
        produceButtons[3].onClick.AddListener(() => playerinventory.sellProduce(4));
        produceButtons[4].onClick.AddListener(() => playerinventory.sellProduce(2));
        produceButtons[5].onClick.AddListener(() => playerinventory.sellProduce(5));
        buySeedsButtons[0].onClick.AddListener(() => playerinventory.buyPlant(0));
        buySeedsButtons[1].onClick.AddListener(() => playerinventory.buyPlant(1));
        buySeedsButtons[2].onClick.AddListener(() => playerinventory.buyPlant(2));
        buySeedsButtons[3].onClick.AddListener(() => playerinventory.buyPlant(3));
        buySeedsButtons[4].onClick.AddListener(() => playerinventory.buyPlant(4));
        buySeedsButtons[5].onClick.AddListener(() => playerinventory.buyPlant(5));
    }
}
