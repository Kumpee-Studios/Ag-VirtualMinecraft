using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class interactionController : MonoBehaviour
{
    public playerController playercontroller;
    public playerInventory PlayerInventory;
    public GameObject interactButton;
    public Text interactText;
    public timerBarController controller;
    public canvasController CanvasController;
    private bool dirt = false; //using these bools to control which interaction is used
    private bool inTown = false; //tracking whether user is somewhere they can hoe or not
    private bool canHarvest = false;
    private bool deadPlant = false;
    private bool seedSeller = false;
    private bool produceBuyer = false;
    public GameObject[] collisions; //shouldn't need 2, but just in case...
    // Start is called before the first frame update
    void Start()
    {
        Button interactbuttonPart = interactButton.GetComponent<Button>();
        interactbuttonPart.onClick.AddListener(handleInteraction);
        collisions = new GameObject[2]; //okay please leave this here, without it you'll get deliciously nasty index out of bounds exceptions
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     Debug.Log(collision.gameObject.tag); //leaving this here JUST in case
     if(collision.gameObject.tag == "Walls")
        {
            playercontroller.canHoe = false;
        }
     else if(collision.gameObject.tag == "Plant")
        {
            collisions[0] = collision.gameObject;
            AnimatorClipInfo[] info = collision.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            if(info[0].clip.name.Contains("harvest"))
            {
                canHarvest = true;
                interactButton.SetActive(true); //Yes I know having this in two places seems inefficient, I tried it the other way
                interactText.text = "Harvest";
            }else if(info[0].clip.name.Contains("dead"))
            {
                deadPlant = true;
                interactButton.SetActive(true); //if you have it not twice, it shows the button but you can't interact with it
                interactText.text = "Remove";
            }
            playercontroller.canHoe = false;
        }
     else if(collision.gameObject.tag == "Dirt" && playercontroller.canDoStuff == true)
        {
            StartCoroutine(checkPlant());
        }   
     else if(collision.gameObject.tag == "SeedSeller")
        {
            seedSeller = true;
            interactButton.SetActive(true); //if you have it not twice, it shows the button but you can't interact with it
            interactText.text = "Buy Seeds";
        }
    else if(collision.gameObject.tag == "ProduceBuyer")
        {
            produceBuyer = true;
            interactButton.SetActive(true); //if you have it not twice, it shows the button but you can't interact with it
            interactText.text = "Sell Produce";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions[0] = null;
        playercontroller.canHoe = true; //so this isn't entirely foolproof, will need to adjust multiple bools possibly
        canHarvest = false;
        deadPlant = false;
        interactButton.SetActive(false);//not sure since I might be able to figure out what this collides with from player controller using tags
        dirt = false;
        seedSeller = false;
        produceBuyer = false;
        StopCoroutine("checkPlant"); //in case they mouse off in 5 milliseconds or less, make sure it doesn't display interact
    }
    public void handleInteraction()
    {
        if(canHarvest)
        {//yes this method call looks like a mess, it grabs the integer that the plantmanager script is storing and passes it to the harvest plant method of PlayerInventory
            StartCoroutine(PlayerInventory.HarvestPlant(collisions[0].gameObject.GetComponent<plantManager>().plantNumber));
            playercontroller.canDoStuff = false;
            StartCoroutine(playercontroller.moveAgain(3f));
            interactButton.SetActive(false);//not sure since I might be able to figure out what this collides with from player controller using tags
            controller.startTimer(3f, "Harvesting Plant"); //displaying bar so user knows they're doing something
            StartCoroutine(removePlant()); //removing plant after harvest to prevent spam
        }
        else if(deadPlant)
        {
            interactButton.SetActive(false);//not sure since I might be able to figure out what this collides with from player controller using tags
            playercontroller.canDoStuff = false;
            StartCoroutine(playercontroller.moveAgain(3f));
            controller.startTimer(3f, "Removing Plant");
            StartCoroutine(removePlant());
        }
        else if (dirt && playercontroller.canDoStuff && !playercontroller.seedMenu) //had to adjust order of if else statements
        {
            interactButton.SetActive(false);
            dirt = false;
            playercontroller.seedMenu = true;
            CanvasController.showSeeds();
            CanvasController.interactableSeeds();
            playercontroller.canHoe = false;
            playercontroller.canDoStuff = false;
        } else if(playercontroller.canHoe && !inTown)
        {
            playercontroller.hoeGround();
        } else if(seedSeller)
        {
            //do seed stuff here
            CanvasController.showSeedSeller();
        } else if (produceBuyer)
        {
            CanvasController.showProduce();
            CanvasController.interactableProduce();
            interactButton.SetActive(false);
            //playercontroller.canDoStuff = false;
        }
    }
    public IEnumerator checkPlant()
    {
        yield return new WaitForSeconds(.005f); //should wait 5/1000th's of a second, should be practically imperceptible to a player
        if (collisions[0] == null)
        {
            interactButton.SetActive(true); //gives game chance to update whether the dirt tile has a plant on top of it
            interactText.text = "Plant seed";
            dirt = true; //if it doesn't then they can plant seeds
        }
    }
    public IEnumerator removePlant()
    { //need this for once they finish harvesting and when they remove dead plant
        yield return new WaitForSeconds(3f);
        Destroy(collisions[0].gameObject); //destroy the plant if its dead
    }
    public void flipTown() //using this in an attempt to prevent hoe-ing while in town or in the shed
    {
        inTown = !inTown;
    }
}
