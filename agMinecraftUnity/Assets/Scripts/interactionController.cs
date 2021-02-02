using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class interactionController : MonoBehaviour
{
    public playerController playercontroller;
    public playerInventory PlayerInventory;
    public GameObject interactButton;
    public GameObject seedCanvas;
    private bool dirt = false;
    public dirtController adjustBool;
    public GameObject[] collisions; //shouldn't need 2, but just in case...
    public LayerMask plantMask;
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
        Debug.Log(collision.gameObject.tag);
     if(collision.gameObject.tag == "Walls")
        {
            playercontroller.canHoe = false;
        }
     else if(collision.gameObject.tag == "Plant")
        {
            collisions[0] = collision.gameObject;
            adjustBool.canInteract = false;
            AnimatorClipInfo[] info = collision.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            //Debug.Log(collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0));
            if(info[0].clip.name.Contains("harvest"))
            {
                Debug.Log("Got em");
                //implement actual harvesting code here, probably need to grab name of plant from animator, then add seeds of that plant type to the inventory
            }
            else 
            {
                playercontroller.canHoe = false;
            }
        }
     else if(collision.gameObject.tag == "Dirt" && playercontroller.canDoStuff == true && !Physics2D.IsTouchingLayers(collision.GetComponent<TilemapCollider2D>(), plantMask))
        {
            Debug.Log(collision.GetComponent<TilemapCollider2D>());
            Debug.Log(Physics2D.IsTouchingLayers(collision.GetComponent<TilemapCollider2D>(), plantMask));
//            collisions[0] = collision.gameObject;
            //adjustBool = collision.gameObject.GetComponent<dirtController>(); //storing reference to object here so that I can adjust boolean later in code
            interactButton.SetActive(true);
            dirt = true;
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (adjustBool != null) //check that the dirtController has been stored as a variable
        {
            adjustBool.canInteract = true;
        }
        collisions[0] = null;
        playercontroller.canHoe = true; //so this isn't entirely foolproof, will need to adjust multiple bools possibly
        interactButton.SetActive(false);//not sure since I might be able to figure out what this collides with from player controller using tags
        dirt = false;
    }
    public void handleInteraction()
    {
        if (dirt && playercontroller.canDoStuff && !playercontroller.seedMenu)
        {
            //PlayerInventory.StartCoroutine("plantCarrot"); handled from buttons in seedCanvas
            dirt = false;//should put a boolean in the dirt controller script to check if it has been planted on already
            //adjustBool.canInteract = false;
            playercontroller.canDoStuff = false;
            playercontroller.seedMenu = true;
            seedCanvas.SetActive(true);
        } else if(playercontroller.canHoe)
        {
            playercontroller.hoeGround();
        }
    }
}
