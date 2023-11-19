using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public GameObject SnakeBodyPrefab;
    public GameObject Head;
    public Vector3 middleBodyPos;
    [SerializeField] float BodyGap;
    public float turnSpeed;
    public float moveSpeed;
    [SerializeField] GameHUD gameHud;
    int score;
    public List<GameObject> SnakeBody = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //middle body position
        if(SnakeBody.Count > 5)
        {
            middleBodyPos = SnakeBody[SnakeBody.Count / 2].transform.position;
        }

        for (int i = 0; i < SnakeBody.Count; i++)
        {
            Vector3 targetPosition;
            Quaternion targetRotation;

            if (i == 0)
            {
                // Follow the head
                targetPosition = Head.transform.position - (Head.transform.forward * 0.6f);
                targetRotation = Head.transform.rotation;
            }
            else
            {
                // Follow the previous body segment
                targetPosition = SnakeBody[i - 1].transform.position - (SnakeBody[i-1].transform.forward * BodyGap);
                targetRotation = SnakeBody[i - 1].transform.rotation;
            }

            Vector3 moveDirection = targetPosition - SnakeBody[i].transform.position;

            SnakeBody[i].transform.position += moveDirection * moveSpeed * Time.deltaTime;
            SnakeBody[i].transform.LookAt(targetPosition);
            SnakeBody[i].transform.rotation = Quaternion.Slerp(SnakeBody[i].transform.rotation, targetRotation, Time.deltaTime);

        }


        //add body
        if (Input.GetKeyDown(KeyCode.R))
        {
            Grow();
        }
    }
    
    public void Grow()
    {
        //last body segment position and spawn positions
        if(SnakeBody.Count > 0)
        {
            Vector3 LastPosition = SnakeBody[SnakeBody.Count - 1].transform.position;
            Vector3 SpawnPosition = new Vector3(LastPosition.x, LastPosition.y, LastPosition.z - (BodyGap + 1f));
            GameObject body = Instantiate(SnakeBodyPrefab, SpawnPosition, Quaternion.identity, transform);
            SnakeBody.Add(body);
        } else
        {
            //spawn first body segment
            Vector3 HeadPosition = Head.transform.position;
            Vector3 SpawnPosition = new Vector3(HeadPosition.x, HeadPosition.y, HeadPosition.z - (0.6f + 1f));
            GameObject body = Instantiate(SnakeBodyPrefab, SpawnPosition, Quaternion.identity, transform);
            SnakeBody.Add(body);
        }

        if(turnSpeed <= 105)
        {
            moveSpeed += 1;
            turnSpeed += 10;
        }

        score++;
        PlayerPrefs.SetInt("score", score);
        gameHud.showScore(score);
    }

    public void Shrink()
    {
        GameObject lastBody = SnakeBody[SnakeBody.Count - 1];
        SnakeBody.RemoveAt(SnakeBody.Count - 1);
        Destroy(lastBody);

        if (turnSpeed >= 15)
        {
            moveSpeed -= 2;
            turnSpeed -= 20;
        }

        score -= 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(middleBodyPos, SnakeBody.Count);
    }
}
