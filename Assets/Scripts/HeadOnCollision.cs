using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOnCollision : MonoBehaviour
{
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            gameManager.gameOver();
        }
    }
}
