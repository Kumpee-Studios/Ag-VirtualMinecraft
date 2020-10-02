using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5; //how fast player moves
    public Transform movePoint; //this is the point that the player will move towards
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; //keeps scene organized, basically making movepoint an orphan to avoid an infinite player movement loop
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (!isMoving && UnityEngine.Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveUp();
                isMoving = true;
                StartCoroutine(moveCooldown());
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDown();
                isMoving = true;
                StartCoroutine(moveCooldown());
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveLeft();
                isMoving = true;
                StartCoroutine(moveCooldown());
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveRight();
                isMoving = true;
                StartCoroutine(moveCooldown());
            }
        }
    }
    private void moveUp() //could be refactored into single switch statement but is not entirely necessary...
    {
        movePoint.position += new UnityEngine.Vector3(0f, 1f, 0f);
    }
    private void moveDown()
    {
        movePoint.position += new UnityEngine.Vector3(0f, -1f, 0f);
    }
    private void moveLeft()
    {
        movePoint.position += new UnityEngine.Vector3(-1f, 0f, 0f);
    }
    private void moveRight()
    {
        movePoint.position += new UnityEngine.Vector3(1f, 0f, 0f);
    }
    private IEnumerator moveCooldown()
    {
        yield return new WaitForSeconds(.1f); //no idea how long moving one square will take, change the float value to reflect movespeed
        if(UnityEngine.Vector3.Distance(transform.position, movePoint.position) > 0)
        {
            movePoint.position = transform.position;
        }
        isMoving = false;
    }
}
