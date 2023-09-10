using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    [SerializeField] GameObject eggPrefab;
    GameObject snake;
    SnakeManager snakeManager;
    Vector3 spawnLocation;
    List<Vector3> snakeBodyPosition = new List<Vector3>();

    //play boundary
    float minX = -37.99762f;
    float maxX = 30.33238f;
    float minZ = -23.38314f;
    float maxZ = 61.07686f;

    Vector3 randomPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        snake = GameObject.FindWithTag("snake");
        snakeManager = snake.GetComponent<SnakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the position of all snake body segments
        foreach(var body in snakeManager.SnakeBody)
        {
            snakeBodyPosition.Add(body.transform.position);
        }
    }

    private void getRandomPosition()
    {
        randomPosition = new Vector3(Random.Range(minX, maxX), 0.32f, Random.Range(minZ, maxZ));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "head")
        {
            snakeManager.Grow();
            getRandomPosition();

            if (snakeBodyPosition.Contains(randomPosition))
            {
                getRandomPosition();
            } else
            {
                spawnLocation = randomPosition;
                Debug.Log(spawnLocation);
            }

            Instantiate(eggPrefab, spawnLocation, Quaternion.identity);

            Destroy(gameObject);
        }


    }
}
