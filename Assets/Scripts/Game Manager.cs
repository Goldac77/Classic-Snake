using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] GameObject snake;
    SnakeManager snakeManager;
    Vector3 middleBodyPos;
    GameObject egg;
    EggScript eggScript;
    Vector3 eggPosition;

    //obstacles play boundary
    float minX = -20.27f;
    float maxX = 10.81f;
    float minZ = -18.9f;
    float maxZ = 12.75f;

    //obstacle data
    Vector3 randomPosition;
    int randomIndex = 0;
    GameObject spawnedObstacle;
    // Start is called before the first frame update
    void Start()
    {
        snakeManager = snake.GetComponent<SnakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int snakeLength = snakeManager.SnakeBody.Count;
        middleBodyPos = snakeManager.middleBodyPos;
        if(snakeLength > 5)
        {
            //instantiate obstacles
            getRandomPosition();
            if(Vector3.Distance(randomPosition, middleBodyPos) < snakeLength || randomPosition == eggPosition)
            {
                getRandomPosition();
            } else
            {
                if (!spawnedObstacle)
                {
                    instantiateObstacle();
                } else
                {
                    //TODO: destroy obstacles after 30s
                    Destroy(spawnedObstacle, 30f);
                }
            }
        }
    }

    private void getRandomPosition()
    {
        randomPosition = new Vector3(Random.Range(minX, maxX), 0.773f, Random.Range(minZ, maxZ));
    }

    void getRandomIndex()
    {
        randomIndex = Random.Range(0, obstacles.Count);
    }

    void instantiateObstacle()
    {
        egg = GameObject.FindWithTag("egg");
        eggScript = egg.GetComponent<EggScript>();
        eggPosition = eggScript.spawnLocation;

        getRandomIndex();
        spawnedObstacle = Instantiate(obstacles[randomIndex], randomPosition, Quaternion.identity);
    }

    public void gameOver()
    {
        //move to gameover screen
        Debug.Log("Game Over!!");
    }
}
