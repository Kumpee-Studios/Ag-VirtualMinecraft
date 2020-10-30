using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerController : MonoBehaviour
{
    public GridLayout grid;
    public Tilemap gridTilemap;
    public TileBase hoedGround;
    public float moveSpeed = 5; //how fast player moves
    public Transform movePoint; //this is the point that the player will move towards
    public GameObject hoePoint; //grabbing reference to 'hoe' spot player can use to till ground
    public timerBarController controller;
    public bool canHoe = true; //this will be accessed from the interactionController to adjust what action is taken when space is pressed, will require mulitple bools
    private bool isMoving = false;
    private bool left = false;
    private bool down = true;
    public bool canDoStuff = true; //true for the interaction controller
    public GameObject carrotFab; //might be easier to make an array later, food for thought
    private Transform transform2; //need this to track the transform of the tile to create the carrot on
    [SerializeField] interactionController interaction; //keeps it private while still showing in inspector
    [SerializeField] GameObject seedpoint;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; //keeps scene organized, basically making movepoint an orphan to avoid an infinite player movement loop
    }

    // Update is called once per frame
    void Update()
    {
        if (canDoStuff)
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            if (!isMoving && UnityEngine.Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
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
                    isMoving = true;
                    StartCoroutine(moveCooldown());
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
                    isMoving = true;
                    StartCoroutine(moveCooldown());
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
                    isMoving = true;
                    StartCoroutine(moveCooldown());
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
                    isMoving = true;
                    StartCoroutine(moveCooldown());
                }
                if (Input.GetKey(KeyCode.Space)) //need a way ot check if they're trying to hoe the fence
                { //this will be generic controller so other if statements will go inside instead of one multi-condition if
                    /*if (canHoe)
                    {
                        hoeGround();
                    }*/
                    interaction.handleInteraction();
                }
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
    public IEnumerator plantCarrot() //this might move to some sort of plant controller/inventory controller script, we'll see though
    {//called from interaction controller
        seedpoint.transform.position = hoePoint.transform.position;
        canDoStuff = false;
        StartCoroutine(moveAgain(2.5f));
        controller.startTimer(2.5f, "Planting carrot");
        yield return new WaitForSeconds(2.5f);
        Vector3Int hoeTile = grid.WorldToCell(hoePoint.transform.position); //figuring out the x,y,z coordinate the of tile to hoe
        seedpoint.transform.position = StringVector(hoeTile.ToString()); //don't need this, remove next coding session
        Instantiate(carrotFab, (StringVector(hoeTile.ToString()) + new UnityEngine.Vector3(0.5f, 0.5f, 0f)), UnityEngine.Quaternion.Euler(0, 0, 0)); //changing name to carrotTile
    }

    public IEnumerator moveAgain(float delay)
    {
        yield return new WaitForSeconds(delay);
        canDoStuff = true;
    }
    public UnityEngine.Vector3 StringVector(string breakIt) //currently not in use but a handy method if you need to convert a vector3Int to a vector3 since there is no implicit conversion
    {
        string[] numbers = breakIt.Split(','); //REMEMBER THE SECOND PARAMETER OF SUBSTRING IS THE STRING LENGTH NOT ENDING INDEX
        UnityEngine.Vector3 result = new UnityEngine.Vector3(float.Parse(numbers[0].Substring(1, numbers[0].Length - 1)), float.Parse(numbers[1]), float.Parse(numbers[2].Substring(0, numbers[2].Length - 1)));
        return result;
    }
}
