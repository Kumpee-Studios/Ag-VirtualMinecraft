using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cornSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject corn;
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

            Vector3 position = transform.position;
            position.y += Random.Range(0, 0);

            Instantiate(corn, position, gameObject.transform.rotation);
            //For good looping, take farthest x coordinate background reaches and divide by time.
        }
    }
    }
