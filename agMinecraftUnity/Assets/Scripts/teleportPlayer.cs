using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportPlayer : MonoBehaviour
{
    public GameObject moveTo;
    //[SerializeField] GameObject moveTo2; //why did I have this here anyways?
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
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerController>().getInteractionController().flipTown();
            collision.gameObject.transform.position = moveTo.gameObject.transform.position;
        }
    }
}
