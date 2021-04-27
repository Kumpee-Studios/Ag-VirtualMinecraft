using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //lets us save it in a file
public class saveInformation //removing monobehavior since this won't be a component
{
    public int money; //stores money earned
    public int[] seeds; //will store array of seeds in inventory
    public int[] produce; //stores produce in inventory
    public float[] position; //stores location in map, might be smarter to just reset them to 0,0,0 every time due to booleans changing ect
    public bool town;
    public bool male;
    public saveInformation(followPlayer camera, playerController controller, playerInventory inventory, interactionController interaction) //handing it the two scripts that will contain information
    {
        male = camera.isMale;
        position = new float[] { controller.gameObject.transform.position.x, controller.gameObject.transform.position.y, controller.gameObject.transform.position.z };
        seeds = inventory.seedArray;
        produce = inventory.produceArray;
        money = inventory.money;
        town = interaction.inTown;
    }
}
