using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInventory : MonoBehaviour
{
    public GameObject seedCanvas;
    public playerController PlayerController;
    //need 6 ints to hold number of each seed, array might be better
    public int[] seedArray = new int[6]; //values of number of seeds they have and text to display them in inventory
    public TextMeshProUGUI[] seedText = new TextMeshProUGUI[6];
    //5 more needed
    public int[] produceArray = new int[6]; //values of how much harvested produce they have and the text that needs to be updated
    public TextMeshProUGUI[] produceText = new TextMeshProUGUI[6];
    [Tooltip("Alphabetical order, bean, carrot, corn, soybean, squash, sugarbeet. Please don't change unless you like making a hard time for yourself.")]
    public GameObject[] plantArray = new GameObject[6]; //in order of prefabs alphabetically: bean, carrot, corn, soybean, squash, sugarbeet
    public List<string> stringCoords = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        //remove this later, I need to be able to plant seeds right now
        for(int number = 0; number < seedArray.Length; number++)
        {
            seedArray[number] = 5; //just trying to set all values of seeds to 5 to make sure I test each one
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator plantCarrot() //this might move to some sort of plant controller/inventory controller script, we'll see though
    {//called from interaction controller
        PlayerController.canDoStuff = false;
        StartCoroutine(PlayerController.moveAgain(2.5f));
        PlayerController.controller.startTimer(2.5f, "Planting carrot");
        yield return new WaitForSeconds(2.5f);
        Vector3Int hoeTile = PlayerController.grid.WorldToCell(PlayerController.hoePoint.transform.position); //figuring out the x,y,z coordinate the of tile to hoe
        Instantiate(plantArray[2], (StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f)), UnityEngine.Quaternion.Euler(0, 0, 0)); //changing name to carrotTile
    }
    public void plantMethod(int plant) //same as harvestMethod
    {
        StartCoroutine(plantPlant(plant));
    }
    public IEnumerator plantPlant(int plant)
    { //creating the generic version of plantCarrot that will simply take in an integer value and instatiate the desired prefab
        Vector3Int hoeTile = PlayerController.grid.WorldToCell(PlayerController.hoePoint.transform.position); //figuring out the x,y,z coordinate the of tile to hoe
        //moving tile up here to check if it has already been filled with seed
        if (seedArray[plant] > 0 && PlayerController.seedMenu && !stringCoords.Contains(StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f).ToString()))
        {
            seedCanvas.SetActive(false); //turning off canvas once they click a button
            PlayerController.canDoStuff = false; //should have already been set but just to make sure
            StartCoroutine(PlayerController.moveAgain(2.5f));
            PlayerController.controller.startTimer(2.5f, "Planting seed");
            yield return new WaitForSeconds(2.5f);
            Instantiate(plantArray[plant], (StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f)), UnityEngine.Quaternion.Euler(0, 0, 0)); //changing name to carrotTile
            seedArray[plant] -= 1; //deleting the numbers from end of string using TrimEnd to remove number and then concatenating the new number of seeds left
            seedText[plant].text = seedText[plant].text.TrimEnd(new[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }) + seedArray[plant];
            PlayerController.seedMenu = false; //don't forget like I did to set the bool to false for the poor player
            stringCoords.Add(StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f).ToString());
        }
        else
        {
            PlayerController.controller.startTimer(1.5f, "You've already planted there");
        }
        //need to let user know they've planted a seed there already
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
    }
    public UnityEngine.Vector3 StringVector(string breakIt) //currently not in use but a handy method if you need to convert a vector3Int to a vector3 since there is no implicit conversion
    {
        string[] numbers = breakIt.Split(','); //REMEMBER THE SECOND PARAMETER OF SUBSTRING IS THE STRING LENGTH NOT ENDING INDEX
        UnityEngine.Vector3 result = new UnityEngine.Vector3(float.Parse(numbers[0].Substring(1, numbers[0].Length - 1)), float.Parse(numbers[1]), float.Parse(numbers[2].Substring(0, numbers[2].Length - 1)));
        return result;
    }
}
