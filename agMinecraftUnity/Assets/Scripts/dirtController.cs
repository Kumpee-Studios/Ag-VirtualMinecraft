using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirtController : MonoBehaviour
{
    public bool canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ran over dirt");
    }
}
