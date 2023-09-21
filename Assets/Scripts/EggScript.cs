using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    [SerializeField] GameObject eggPrefab;
    GameObject snake;
    SnakeManager snakeManager;
    [HideInInspector]public Vector3 spawnLocation;
    List<Vector3> snakeBodyPosition = new List<Vector3>();

    //play boundary
    float minX = -28.07f;
    float maxX = 18.1f;
    float minZ = -23.91f;
    float maxZ = 21.3f;

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
        //add location of the head to total positions
        snakeBodyPosition.Add(snakeManager.Head.transform.position);
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
