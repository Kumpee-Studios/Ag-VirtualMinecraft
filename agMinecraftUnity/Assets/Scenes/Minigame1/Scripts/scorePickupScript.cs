using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class scorePickupScript : MonoBehaviour
{
    public backgroundScrollScript scorePower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scorePower.scoreTime += 25;
            Destroy(this.gameObject);
        }
    }
}
