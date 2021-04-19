using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpaawnNScroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float time = 0f;
    void Update()
    {
        this.transform.Translate(new Vector3(-10f, 0, 0) * Time.deltaTime, Space.World);
        time += Time.deltaTime;
        if (time > 13.6f)
        {
            time = 0f;
            Destroy(this.gameObject);
           
        }
    }
}
