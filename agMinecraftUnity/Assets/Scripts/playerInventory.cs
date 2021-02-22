using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerInventory : MonoBehaviour
{
    public int money = 0; //they get to start broke.
    public TextMeshProUGUI moneyText;
    public canvasController CanvasController;
    public playerController PlayerController;
    [SerializeField] interactionController InteractionController; //using this sovlely for saving
    //need 6 ints to hold number of each seed, array might be better
    public int[] seedArray = new int[6]; //values of number of seeds they have and text to display them in inventory
    public TextMeshProUGUI[] seedText = new TextMeshProUGUI[6];
    //5 more needed
    public int[] produceArray = new int[6]; //values of how much harvested produce they have and the text that needs to be updated
    public TextMeshProUGUI[] produceText = new TextMeshProUGUI[6];
    [Tooltip("Alphabetical order, bean, carrot, corn, soybean, squash, sugarbeet. Please don't change unless you like making a hard time for yourself.")]
    public GameObject[] plantArray = new GameObject[6]; //in order of prefabs alphabetically: bean, carrot, corn, soybean, squash, sugarbeet
    public List<string> stringCoords = new List<string>();
    private char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //just so you don't have to create an array every time you update produce ect.

    // Start is called before the first frame update
    void Start()
    {
        //remove this later, I need to be able to plant seeds right now
        for (int number = 0; number < seedArray.Length; number++)
        {
            seedArray[number] = 3; //just trying to set all values of seeds to 5 to make sure I test each one
        }
        loadInfo(); //calling this after setting all seeds to 3. If they haven't played before it should return null and give them 3 seeds of each
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void plantMethod(int plant) //same as harvestMethod
    {
        StartCoroutine(plantPlant(plant));
    }
    public IEnumerator plantPlant(int plant)
    { //creating the generic version of plantCarrot that will simply take in an integer value and instatiate the desired prefab
        Vector3Int hoeTile = PlayerController.grid.WorldToCell(PlayerController.hoePoint.transform.position); //figuring out the x,y,z coordinate the of tile to hoe
        if (seedArray[plant] > 0 && PlayerController.seedMenu /*&& !stringCoords.Contains(StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f).ToString())*/)
        {
            CanvasController.showSeeds(); //turning off canvas once they click a button
            CanvasController.uninteractableSeeds(); //making sure if they click the canvas button they can't click on a button again
            PlayerController.canDoStuff = false; //adding this here since it gets flipped to true in the canvas controller script, need to leave it that way so they can 'cancel' planting a seed and not freeze in place
            PlayerController.controller.startTimer(2.5f, "Planting seed");
            //PlayerController.moveAgain(2.5f);
            yield return new WaitForSeconds(2.5f);
            Instantiate(plantArray[plant], (StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f)), UnityEngine.Quaternion.Euler(0, 0, 0)); //changing name to carrotTile
            seedArray[plant] -= 1; //deleting the numbers from end of string using TrimEnd to remove number and then concatenating the new number of seeds left
            seedText[plant].text = seedText[plant].text.TrimEnd(numbers) + seedArray[plant];
            PlayerController.seedMenu = false; //don't forget like I did to set the bool to false for the poor player
            PlayerController.canDoStuff = true; //I thought the moveAgain method would work, maybe I adjusted the wrong bool...
        }
        else //this else statement SHOULD NOT be necessary, but it's here as a failsafe if they somehow try to double plant
        {
            PlayerController.controller.startTimer(1.5f, "You've already planted there");
        }
    }
    public void harvestMethod(int plant) //need to make a method that can be called from button click, coroutines cannot be called from editor
    {
        StartCoroutine(HarvestPlant(plant));
    }
    public IEnumerator HarvestPlant(int plant)
    {
        yield return new WaitForSeconds(2.5f); //need to figure out how to call some sort of destroy method from the plant controller
        seedArray[plant] += Random.Range(0, 1);
        produceArray[plant] += Random.Range(2, 5); //giving them produce and seeds for harvesting
        seedText[plant].text = seedText[plant].text.TrimEnd(numbers) + seedArray[plant]; //removing number of seeds from both seeds and produce, then adding new number to end of string
        produceText[plant].text = produceText[plant].text.TrimEnd(numbers) + produceArray[plant];
    }
    public UnityEngine.Vector3 StringVector(string breakIt) //currently not in use but a handy method if you need to convert a vector3Int to a vector3 since there is no implicit conversion
    {
        string[] numbers = breakIt.Split(','); //REMEMBER THE SECOND PARAMETER OF SUBSTRING IS THE STRING LENGTH NOT ENDING INDEX
        UnityEngine.Vector3 result = new UnityEngine.Vector3(float.Parse(numbers[0].Substring(1, numbers[0].Length - 1)), float.Parse(numbers[1]), float.Parse(numbers[2].Substring(0, numbers[2].Length - 1)));
        return result;
    }
    public void sellProduce(int plant)
    {
        if (produceArray[plant] > 0)
        {
            produceArray[plant] -= 1;
            switch (plant)
            {
                case 0:
                    money += 5;
                    break;
                case 1:
                    money += 3;
                    break;
                case 2:
                    money += 4;
                    break;
                case 3:
                    money += 6;
                    break;
                case 4:
                    money += 3;
                    break;
                case 5:
                    money += 4;
                    break;
            }
            moneyText.text = moneyText.text.TrimEnd(numbers) + money;
            produceText[plant].text = produceText[plant].text.TrimEnd(numbers) + produceArray[plant];
            if(money >= 10) //10 for testing purposes, adjust as needed
            {
                //implement end game stuff here
                saveInfo();
                SceneManager.LoadSceneAsync("LevelUp");
            }
        }
    }
    public void buyPlant(int plant)
    {
        if (money >= 2)
        {
            seedArray[plant] += 1; //adding seed to inventory and updating text
            seedText[plant].text = seedText[plant].text.TrimEnd(numbers) + seedArray[plant];
            money -= 2;
            moneyText.text = moneyText.text.TrimEnd(numbers) + money;
        }
    }
    public void saveInfo() //saves player information
    {
        saveSystem.savePlayer(PlayerController, this, InteractionController); ;
    }
    public void loadInfo() //unpacks player info and sets various variables equal to what they should be
    {
        saveInformation stats = saveSystem.loadData();
        if (stats != null) //make sure they HAVE a save file or you can break the game if you want
        {
            seedArray = stats.seeds;
            produceArray = stats.produce;
            money = stats.money;
            InteractionController.inTown = stats.town;
            Vector3 playerPosition = new Vector3(stats.position[0], stats.position[1], stats.position[2]);
            foreach (int num in seedArray) //setting all seeds to where they should be
            {
                seedText[num].text = seedText[num].text.TrimEnd(numbers) + seedArray[num]; //removing number of seeds from both seeds and produce, then adding new number to end of string
            }
            foreach (int num in produceArray) //adjusting produce text
            {
                produceText[num].text = produceText[num].text.TrimEnd(numbers) + produceArray[num];
            }
            moneyText.text = moneyText.text.TrimEnd(numbers) + money;
        }
    }
}
