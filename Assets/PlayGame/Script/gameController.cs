using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*Game end when player is defeated or the light is blow off */
public class gameController : MonoBehaviour {
     int score = 0;
    public Text scoringText;

    public float spawnTime = 10;
    private float timer = 0;
    bool isStart = false;
    public GameObject player;
    public GameObject mylight;
    public List<GameObject> enemyPrefab;
    public List<Transform> enemySpawnLocation;
    public int  EnemyNumberPerWave =5;
 
    public GameObject startcanvas;
    public GameObject endcanvas;
    List<GameObject> enemyOnField;
   
    void Start () {
        
        enemyOnField = new List<GameObject>();
        player.SetActive(false);
        SetScoreText();

    }

    
    void Update()
    {

        if (isStart)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer -= spawnTime;
                SpawnEnemy();
            }
        }
        
    }

    //when button is pressed
    //initialize player health
    //player position
    //spwan enemy 
    public void StartGame()
    {

       // Debug.Log("Game Start button is pressed");
        startcanvas.SetActive(false); //disable the canvas
        player.SetActive(true);
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion();
        timer = 0;
        isStart = true;
       
        SpawnEnemy();

    }

    public void RestartGame()
    {
        //reset 
        timer = 0;
        score = 0;
        SetScoreText();

        isStart = true;
        endcanvas.SetActive(false); //disable the canvas
        player.SetActive(true);
        player.GetComponent<PlayerHealth>().resetPlayer();
        mylight.GetComponent<LightHealth>().resetLight();  
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion();

        SpawnEnemy();
    }
    //stop the enemy 
    //stop the player //stop the gunshot sound

    //two condition for game over: lamp hp =0; player hp =0
    public void GameOver()
    {
        isStart = false; //stop the timer
        endcanvas.SetActive(true);
        player.SetActive(false);
        //display score

        //clear the enemy on field list
        for (int i = 0; i < enemyOnField.Count; i++)
        {
          if(   enemyOnField[i] != null)
            {
                Destroy(enemyOnField[i]);
            }
        }
        enemyOnField.Clear();

    }
   

    public void ExitGame()
    {
        Application.Quit();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < EnemyNumberPerWave; i++)
        {
            enemyOnField.Add( GameObject.Instantiate(enemyPrefab[i], enemySpawnLocation[i].position, enemySpawnLocation[i].rotation));
        }
      
    }
    public void addScore()
    {
        score++;
        SetScoreText();
    }


    void SetScoreText()
    {
        scoringText.text = "Total Score: " + (score*10).ToString();
        
    }
}

