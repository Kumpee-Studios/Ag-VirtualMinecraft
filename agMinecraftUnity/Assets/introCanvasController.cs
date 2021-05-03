using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class introCanvasController : MonoBehaviour
{
    public followPlayer camera;
    public interactionController interaction;
    public playerController controller;
    public playerInventory inventory;
    public TextMeshProUGUI instructionText;
    public GameObject playerPlaceholder;
    public VideoClip[] clips;
    public VideoPlayer player;
    public GameObject videoScreen;
    [SerializeField] string[] instructions;
    public GameObject arrow;
    public GameObject goToGame;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void male()
    {
        playerPlaceholder.transform.position = Vector3.zero;
        camera.isMale = true;
        saveSystem.savePlayer(camera, controller, inventory, interaction);
        SceneManager.LoadSceneAsync("Backyard");
    }
    public void female()
    {
        playerPlaceholder.transform.position = Vector3.zero;
        camera.isMale = false;
        saveSystem.savePlayer(camera, controller, inventory, interaction);
        SceneManager.LoadSceneAsync("Backyard");
    }
    public void nextButton()
    {
        switch(counter)
        {
            case 5: //deactivate the text and video objects
                instructionText.gameObject.SetActive(false);
                player.gameObject.SetActive(false);
                arrow.SetActive(false);
                videoScreen.SetActive(false);
                goToGame.SetActive(true);
                break;
            default:
                player.clip = clips[counter];
                instructionText.text = instructions[counter];
                break;
        }
        counter++;
    }
}
