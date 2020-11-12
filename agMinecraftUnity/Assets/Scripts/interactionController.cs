using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionController : MonoBehaviour
{
    public playerController playercontroller;
    public playerInventory PlayerInventory;
    public GameObject interactButton;
    private bool dirt = false;
    private dirtController adjustBool;
    // Start is called before the first frame update
    void Start()
    {
        Button interactbuttonPart = interactButton.GetComponent<Button>();
        interactbuttonPart.onClick.AddListener(handleInteraction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.gameObject.tag == "Walls")
        {
            playercontroller.canHoe = false;
        }
     else if(collision.gameObject.tag == "Dirt" && playercontroller.canDoStuff == true && collision.gameObject.GetComponent<dirtController>().canInteract)
        {
            adjustBool = collision.gameObject.GetComponent<dirtController>(); //storing reference to object here so that I can adjust boolean later in code
            interactButton.SetActive(true);
            dirt = true;
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playercontroller.canHoe = true; //so this isn't entirely foolproof, will need to adjust multiple bools possibly
        interactButton.SetActive(false);//not sure since I might be able to figure out what this collides with from player controller using tags
        dirt = false;
    }
    public void handleInteraction()
    {
        if (dirt)
        {
            PlayerInventory.StartCoroutine("plantCarrot");
            dirt = false;//should put a boolean in the dirt controller script to check if it has been planted on already
            adjustBool.canInteract = false;
        } else if(playercontroller.canHoe)
        {
            playercontroller.hoeGround();
        }
    }
}
