using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class cornGraze : MonoBehaviour
{
    //public backgroundScrollScript scoreTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private float grazingCorn = 25f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //scoreTime.scoreTime += 25;
            
        }
    }
}
