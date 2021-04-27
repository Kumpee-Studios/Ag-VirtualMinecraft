using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class produceBuyer : MonoBehaviour
{
    public SpriteRenderer producePoint; //creating a sprite object that will display what produce the npc buys
    public Sprite[] vegetables;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-4f, 0, 0);
        StartCoroutine(walkToStand());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator walkToStand() //walking to stand
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(returnFromStand());
    }
    private IEnumerator returnFromStand() //pauses at stand while buying stuff
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0); //forgot to stop them at first
        int bought = Random.Range(1, 3);
        int selection = Random.Range(0, 5);
        int price = Random.Range(2, 5); //dymanically generate what they buy, for how much, and grab reference to player's inventory
        producePoint.sprite = vegetables[selection]; //should show what vegetable they bought above head
        playerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInventory>();
        if(inventory.produceArray[selection] > bought)
        {
            inventory.produceArray[selection] -= bought; //code identical to playerInventory Script
            inventory.money += price * bought;
            inventory.moneyText.text = inventory.moneyText.text.TrimEnd(inventory.numbers) + inventory.money; //updates money text
            inventory.produceText[selection].text = inventory.produceText[selection].text.TrimEnd(inventory.numbers) + inventory.produceArray[selection];
            inventory.canEnd(); //checking if player has sold enough produce to end the game
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(removeFromScene());
    }
    private IEnumerator removeFromScene() //return from stand and explodes
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(4f, 0, 0);
        producePoint.sprite = null;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
