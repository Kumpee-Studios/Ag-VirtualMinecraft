using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
 
        if (time > 13.6f)
        {
            time = 0f;
            Instantiate(Obstacle);
            //For good looping, take farthest x coordinate and divide by time.
        }
    }
}
