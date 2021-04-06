using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinigame : MonoBehaviour
{
    public float speed = 10;
    private Vector2 targetPosition;
    void Start()
    {

        targetPosition = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            moveUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDown();
        }



    }
    private void moveUp() //could be refactored into single switch statement but is not entirely necessary...
    {
        transform.position += new UnityEngine.Vector3(0, speed * Time.deltaTime, 0);
    }
    private void moveDown()
    {
        transform.position += new UnityEngine.Vector3(0, -speed * Time.deltaTime, 0);
    }
}
