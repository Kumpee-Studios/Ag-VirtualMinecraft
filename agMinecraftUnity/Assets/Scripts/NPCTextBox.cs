using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NPCTextBox : MonoBehaviour
{
    public GameObject textbox;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if player presses 'E' key this frame
        if (Input.GetKeyDown(KeyCode.E))
        {
            // if textbox reference exists
            if (textbox != null)
            {
                // toggle textbox (visible/hidden)
                bool wasActive = textbox.gameObject.activeSelf;
                textbox.gameObject.SetActive(!wasActive);
            }
        }
    }
}
