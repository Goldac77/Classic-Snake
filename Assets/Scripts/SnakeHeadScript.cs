using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadScript : MonoBehaviour
{
    public SnakeManager snakeManager;
    AudioSource eatSound;
    // Start is called before the first frame update
    void Start()
    {
        eatSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "egg")
        {
            eatSound.Play();
            if(snakeManager.turnSpeed <= 105)
            {
                snakeManager.moveSpeed += 1;
                snakeManager.turnSpeed += 10;
            } 
        }
    }
}
