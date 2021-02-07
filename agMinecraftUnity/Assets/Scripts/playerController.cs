using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerController : MonoBehaviour
{
    [SerializeField] int speed = 8;
    public GridLayout grid;
    public Tilemap gridTilemap;
    public TileBase hoedGround;
    public float moveSpeed = 5; //how fast player moves
    public GameObject hoePoint; //grabbing reference to 'hoe' spot player can use to till ground
    public timerBarController controller;
    public bool canHoe = true; //this will be accessed from the interactionController to adjust what action is taken when space is pressed, will require mulitple bools
    private bool left = false;
    private bool down = true;
    public bool canDoStuff = true; //true for the interaction controller
    public bool seedMenu = false; //need to make sure that the seed menu is open so when they click button it will plant crop
    private Transform transform2; //need this to track the transform of the tile to create the carrot on
    [SerializeField] interactionController interaction; //keeps it private while still showing in inspector
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canDoStuff)
        {
                if (Input.GetKey(KeyCode.W))
                {
                    if (down) //work in progress, will probably just wind up using sprite anims...not sure. Will need spritesheet first
                    {
                        down = !down;
                        //this.gameObject.transform.rotation = UnityEngine.Quaternion.Euler(90, 0, 0);
                        hoePoint.gameObject.transform.position = this.gameObject.transform.position + new UnityEngine.Vector3(0, 1.5f, 0);
                    }
                    moveUp();
                }
                if (Input.GetKey(KeyCode.S))
                {
                    if (!down) //see work in progress comment above on (currently) line 32
                    {
                        down = !down;
                        //this.gameObject.transform.rotation = UnityEngine.Quaternion.Euler(-90, 0, 0);
                        hoePoint.gameObject.transform.position = this.gameObject.transform.position + new UnityEngine.Vector3(0, -1.5f, 0); //adjusts where they can hoe the ground
                    }
                    moveDown();
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (!left)
                    {
                        left = !left; //the rotate brings the hoe point with it
                        this.gameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, 180, 0);
                        hoePoint.gameObject.transform.position = this.gameObject.transform.position + new UnityEngine.Vector3(-1.5f, 0, 0); //since I'm rotating the sprite this just has to be 0

                    }
                    moveLeft();
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (left)
                    {
                        left = !left;
                        this.gameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);
                        hoePoint.gameObject.transform.position = this.gameObject.transform.position + new UnityEngine.Vector3(1.5f, 0, 0); //since I'm rotating the sprite this just has to be 0
                    }
                    moveRight();
                }
                if (Input.GetKey(KeyCode.Space)) 
                { 
                    interaction.handleInteraction();
                }
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
    private void moveLeft()
    {
        transform.position += new UnityEngine.Vector3(-speed * Time.deltaTime, 0, 0);
    }
    private void moveRight()
    {
        transform.position += new UnityEngine.Vector3(speed * Time.deltaTime, 0, 0);
    }
    public void hoeGround()
    {
        //implement tile changing code here
        canDoStuff = false;
        StartCoroutine(moveAgain(3f));
        controller.startTimer(3f, "Breaking ground");
        Vector3Int hoeTile = grid.WorldToCell(hoePoint.transform.position); //figuring out the x,y,z coordinate the of tile to hoe
        Debug.Log(hoeTile); //printing it for sanity's sake
        gridTilemap.SetTile(hoeTile, hoedGround); //so, SetTile must have capital S and T, spent a while figuring that out
        //outside of that you need a reference to the tilemap not just the grid even though grid inherits FROM tilemap
        //after that just feed the SetTile the tile in question you want to instantiate (using a seperate layer with colliders)
        //and where to instantiate it. I wrote that backwards, location, tile to create. 
    }

    public IEnumerator moveAgain(float delay)
    {
        yield return new WaitForSeconds(delay);
        canDoStuff = true;
    }
    public interactionController getInteractionController()
    {
        return interaction;
    }
}
