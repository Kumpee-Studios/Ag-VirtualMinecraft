using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionController : MonoBehaviour
{
    public playerController playercontroller;
    public GameObject interactButton;
    // Start is called before the first frame update
    void Start()
    {
        
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
     else if(collision.gameObject.tag == "Dirt" && playercontroller.canDoStuff == true)
        {
            interactButton.SetActive(true);
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playercontroller.canHoe = true; //so this isn't entirely foolproof, will need to adjust multiple bools possibly
        interactButton.SetActive(false);//not sure since I might be able to figure out what this collides with from player controller using tags
    }
}
