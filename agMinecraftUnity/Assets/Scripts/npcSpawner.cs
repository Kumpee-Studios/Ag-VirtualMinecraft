using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcSpawner : MonoBehaviour
{
    public GameObject[] npcs;
    public int people = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnNPCS()
    {
        people = 0; //resetting variable for reuse
        people = Random.Range(2, 10); //between 2 and 10 people will come to market
        StartCoroutine(spawnPeople());
        /*for (int i = 0; i < people; i++)
        {
            Instantiate(npcs[Random.Range(0, npcs.Length)]);
        }*/
    }
    private IEnumerator spawnPeople()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 3.0f));
        if (people > 0) //recursively calling spawn people at random-ish intervals
        {
            Instantiate(npcs[Random.Range(0, npcs.Length)], transform);
            people--;
            StartCoroutine(spawnPeople());
        }
    }
}
