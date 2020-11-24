using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("No one has been able to deflect the 20-meter radius of Emerald Splash!");
    }
}
