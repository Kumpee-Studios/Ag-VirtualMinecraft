using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject[] obstacles;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
 
        if (time > 27.2f)
        {
            time = 0f;

            Vector3 position = transform.position;
            position.y += Random.Range(-10f, 10f) * 1.5f;

            Instantiate(obstacles[Random.Range(0, obstacles.Length)], position, gameObject.transform.rotation);
            //For good looping, take farthest x coordinate background reaches and divide by time.
        }
    }
}
