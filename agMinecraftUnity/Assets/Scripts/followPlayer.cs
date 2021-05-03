using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public bool isMale; //public for access from saveinformation
    public GameObject malePlayer;
    public GameObject femalePlayer;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if(isMale) //should check gender and activate/deactivate based on everything.
        {
            femalePlayer.SetActive(false); //deactivating female player gameobject since its tagged player
            GameObject[] females = GameObject.FindGameObjectsWithTag("female");
            foreach(GameObject female in females)
            {
                female.SetActive(false);
            }
        } else
        {
            malePlayer.SetActive(false); //deactivating male player gameobject since its tagged player
            GameObject[] males = GameObject.FindGameObjectsWithTag("male");
            foreach(GameObject male in males)
            {
                male.SetActive(false);
            }
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }
}
