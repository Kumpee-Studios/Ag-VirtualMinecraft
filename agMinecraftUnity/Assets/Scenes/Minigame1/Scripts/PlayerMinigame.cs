using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMinigame : MonoBehaviour
{
    public float speed = 10;
    public GameObject gameOverCanvas;
    public backgroundScrollScript scroller;
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
        transform.position += new UnityEngine.Vector3(0, -speed * Time.deltaTime * 0.25f , 0);



    }
    private void moveUp() //could be refactored into single switch statement but is not entirely necessary...
    {
        transform.position += new UnityEngine.Vector3(0, speed * Time.deltaTime, 0);
    }
    private void moveDown()
    {
        transform.position += new UnityEngine.Vector3(0, -speed * Time.deltaTime, 0);
    }
    public void gameOverCondition()
    {
        gameOverCanvas.gameObject.SetActive(true);
        scroller.gameOverCondition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0;
            gameOverCondition();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Replay()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
