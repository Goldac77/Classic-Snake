using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] GameObject snake;
    SnakeManager snakeManager;
    Vector3 middleBodyPos;

    //play boundary
    float minX = -28.07f;
    float maxX = 18.1f;
    float minZ = -23.91f;
    float maxZ = 21.3f;

    Vector3 randomPosition;
    int randomIndex = 0;
    bool obstacleInstantiated = false;
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
            if(Vector3.Distance(randomPosition, middleBodyPos) < snakeLength)
            {
                getRandomPosition();
            } else
            {
                if (!obstacleInstantiated)
                {
                    instantiateObstacle();
                } else
                {
                    //TODO: destroy obstacles after 30s
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
        getRandomIndex();
        Instantiate(obstacles[randomIndex], randomPosition, Quaternion.identity);
        obstacleInstantiated = true;
    }
}
