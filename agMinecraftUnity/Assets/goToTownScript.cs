using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToTownScript : MonoBehaviour
{
    [SerializeField] private canvasController canvascontroller; //really don't like doing this, should consider refactoring later down the road
    [SerializeField] private GameObject returnPoint;
    private playerController PlayerController;
    private bool goingToCity = false;
    [SerializeField] private GameObject pixelTruck;
    [SerializeField] private GameObject pixelCity;
    private void Start()
    {
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerController>().getInteractionController().flipTown();
            collision.gameObject.transform.position = new Vector3(60f, -15f, -10f);
            PlayerController.canDoStuff = false;
            PlayerController.canHoe = false;
            moveCar();
        }
    }
    public void moveCar() //making public for button click
    {
        goingToCity = !goingToCity;
        if(goingToCity) //this will trigger first time it runs since false will flip to true, then it will evaluate boolean
        {
            pixelTruck.transform.Rotate(new Vector3(0, 0, 0)); //make sure car is facing 'forward' 
            Vector3 moveSpeed = new Vector3(18f, 0f, 0f);
            pixelTruck.GetComponent<Rigidbody2D>().velocity = moveSpeed; //moving car and player
            PlayerController.gameObject.GetComponent<Rigidbody2D>().velocity = moveSpeed;
            canvascontroller.goToCity();
        } else
        {
            pixelTruck.transform.Rotate(new Vector3(0, 180, 0)); //faces car 'backwards' 
            PlayerController.gameObject.transform.position = new Vector3(pixelTruck.transform.position.x, pixelTruck.transform.position.y, -10f);
            Vector3 moveSpeed = new Vector3(-18f, 0f, 0f);
            pixelTruck.GetComponent<Rigidbody2D>().velocity = moveSpeed; //moving car and player
            PlayerController.gameObject.GetComponent<Rigidbody2D>().velocity = moveSpeed;
            canvascontroller.returnFromCity();
        }
        StartCoroutine(stopCar());
    }
    private IEnumerator stopCar() //stops car and player
    {
        yield return new WaitForSeconds(4f);
        pixelTruck.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //setting both car and player to stop, otherwise they go forever
        PlayerController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (goingToCity) //jumps the camera vertically up so they see the city instead of the road
        {
            PlayerController.gameObject.transform.position = new Vector3(pixelCity.transform.position.x, pixelCity.transform.position.y, -10f);
        } else //will warp them back to the farm once they return
        {
            PlayerController.gameObject.transform.position = returnPoint.transform.position;
            PlayerController.canDoStuff = true;
            PlayerController.canHoe = true;        
        }
    }
}
