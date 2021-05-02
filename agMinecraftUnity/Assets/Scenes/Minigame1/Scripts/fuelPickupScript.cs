using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class fuelPickupScript : MonoBehaviour
{
    public backgroundScrollScript fuelCounter;

    // Start is called before the first frame update
    void Start()
    {
        fuelCounter = GameObject.FindGameObjectWithTag("Dirt").GetComponent<backgroundScrollScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fuelCounter.fuelCount += 50;
            Destroy(this.gameObject);
        }
    }
}
