using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpaawnNScroll : MonoBehaviour
{
    /*
    public GameObject obstacleDown;
    public GameObject obstacleUp;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(obstacleUp, new Vector3(250, 75, 0), Quaternion.identity);
        Instantiate(obstacleDown, new Vector3(150, -75, 0), Quaternion.identity);
    }
    */

    // Update is called once per frame
    private float time = 0f;
    void Update()
    {
        this.transform.Translate(new Vector3(-10f, 0, 0) * Time.deltaTime, Space.World);
        time += Time.deltaTime;
        if (time > 54.4f)
        {
            time = 0f;
            Destroy(this.gameObject);

            /*
            Instantiate(obstacleUp, new Vector3(250, 75, 0), Quaternion.identity);
            Instantiate(obstacleDown, new Vector3(150, -75, 0), Quaternion.identity); */
        }
    }
}
