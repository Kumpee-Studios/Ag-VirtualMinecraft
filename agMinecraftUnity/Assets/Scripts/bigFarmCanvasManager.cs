﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigFarmCanvasManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject seedsCanvas;
    public GameObject produceCanvas;
    public GameObject fertilizerCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseCanvas.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    public void showSeeds()
    {
        if (seedsCanvas.activeSelf == false)
        {
            seedsCanvas.SetActive(true); //making sure there's only one overlay on screen on a time to avoid clutter
            produceCanvas.SetActive(false);
            fertilizerCanvas.SetActive(false);
        }
        else
        {
            hideSeeds();
        }
    }
    public void showProduce()
    {
        if (produceCanvas.activeSelf == false)
        {
            produceCanvas.SetActive(true);
            seedsCanvas.SetActive(false);
            fertilizerCanvas.SetActive(false);
        }
        else
        {
            hideProduce();
        }
    }
    public void showFertilizer()
    {
        if (fertilizerCanvas.activeSelf == false)
        {
            fertilizerCanvas.SetActive(true);
            produceCanvas.SetActive(false);
            seedsCanvas.SetActive(false);
        }
        else
        {
            hideFertilizer();
        }
    }
    public void hideSeeds() //splitting these methods up so that I can use them here and call them whenever an item is selected from one of these menus
    {
        seedsCanvas.SetActive(false);
    }
    public void hideProduce()
    {
        produceCanvas.SetActive(false);
    }
    public void hideFertilizer()
    {
        fertilizerCanvas.SetActive(false);
    }
}
