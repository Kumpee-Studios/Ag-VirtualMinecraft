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
    public followPlayer follow;
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
    public char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //just so you don't have to create an array every time you update produce ect.

    // Start is called before the first frame update
    void Start()
    {
        loadInfo(); //Loading Player Information
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
            Debug.Log("Should've edited text by now.");
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
    public void sellProduce(int plant) //I like to think this is self documenting selling code, just in case
    {   //in case something explodes just un comment these
        //Debug.Log("This is the plant variable: " + plant);
        //Debug.Log("Produce array of plant: " + produceArray[plant]);
        if (produceArray[plant] > 0) //checks that they have produce to sell
        {
            produceArray[plant] -= 1; //reduces produce by 1
            switch (plant) //adds money depending on what plant they sold
            {
                case 0:
                    money += 2;
                    break;
                case 1:
                    money += 2;
                    break;
                case 2:
                    money += 2;
                    break;
                case 3:
                    money += 2;
                    break;
                case 4:
                    money += 2;
                    break;
                case 5:
                    money += 2;
                    break;
            }
            moneyText.text = moneyText.text.TrimEnd(numbers) + money; //updates money text
            produceText[plant].text = produceText[plant].text.TrimEnd(numbers) + produceArray[plant];
            canEnd();
        }
    }
    public void buyPlant(int plant)
    {
        if (money >= 2)
        {
            seedArray[plant] += 1; //adding seed to inventory and updating text
            seedText[plant].text = seedText[plant].text.TrimEnd(numbers) + seedArray[plant];
            money -= 2; //removing money
            moneyText.text = moneyText.text.TrimEnd(numbers) + money; //updating money text
        }
    }
    public void saveInfo() //saves player information
    {
        saveSystem.savePlayer(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<followPlayer>(), PlayerController, this, InteractionController); ;
    }
    public void loadInfo() //unpacks player info and sets various variables equal to what they should be
    {
        saveInformation stats = saveSystem.loadData();
        if (stats != null) //make sure they HAVE a save file or you can break the game if you want
        {
            if (follow != null)
            {
                follow.isMale = stats.male;
            }
            seedArray = stats.seeds;
            produceArray = stats.produce;
            money = stats.money;
            InteractionController.inTown = stats.town;
            Vector3 playerPosition = new Vector3(stats.position[0], stats.position[1], stats.position[2]);
            GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = playerPosition;
            int index = 0;
            foreach (int num in seedArray) //setting all seeds to where they should be
            {
                if (seedText[index] != null)
                {
                    seedText[index].text = seedText[index].text.TrimEnd(numbers) + seedArray[index]; //removing number of seeds from both seeds and produce, then adding new number to end of string
                    index++;
                }
            }
            index = 0;
            foreach (int num in produceArray) //adjusting produce text
            {
                if (produceText[index] != null)
                {
                    produceText[index].text = produceText[index].text.TrimEnd(numbers) + produceArray[index];
                    index++;
                }
            }
            if (moneyText != null)
            {
                moneyText.text = moneyText.text.TrimEnd(numbers) + money;
            }
        }
        else //I had this, and then I removed it for no reason, don't get rid of this. If the user is a new player it gives them 3 seeds a piece
        {
            for (int i = 0; i < seedArray.Length; i++)
            {
                seedArray[i] = 3;
            }
            foreach (int num in seedArray) //setting all seeds to where they should be
            {
                int index = 0;
                if (seedText[index] != null)
                {
                    seedText[index].text = seedText[index].text.TrimEnd(numbers) + seedArray[index]; //removing number of seeds from both seeds and produce, then adding new number to end of string
                    index++;
                }
            }
        }
    }
    public void canEnd()
    {
        if (money >= 10) //10 for testing purposes, adjust as needed
        { //if they've reached a milestone it will take them to the level up screen
            CanvasController.canEnd = true;
        }
    }
}
