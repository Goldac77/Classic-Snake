using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    public SnakeManager snakeManager;
    public GameManager gameManager;
    float TurnDirection;
    AudioSource eatSound;

    // Start is called before the first frame update
    void Start()
    {
        eatSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //always move forward
        transform.position += transform.forward * snakeManager.moveSpeed * Time.deltaTime;

        //steer head
        TurnDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * TurnDirection * snakeManager.turnSpeed * Time.deltaTime);

        //adjust rotation when the head is falling to the side
        if(transform.rotation.eulerAngles.z != 0)
        {
            transform.Rotate(Vector3.zero * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "egg")
        {
            snakeManager.Grow();
            eatSound.Play();
        } else if(collision.gameObject.tag == "obstacle_side")
        {
            Destroy(gameManager.spawnedObstacle);
            snakeManager.Shrink();
        }
    }
}
