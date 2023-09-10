using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public GameObject SnakeBodyPrefab;
    public GameObject Head;
    float TurnDirection;
    [SerializeField] float BodyGap;
    [SerializeField] float TurnSpeed;
    [SerializeField] float moveSpeed;
    public List<GameObject> SnakeBody = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //always move forward
        Head.transform.position += Head.transform.forward * moveSpeed * Time.deltaTime;

        //steer head
        TurnDirection = Input.GetAxis("Horizontal");
        Head.transform.Rotate(Vector3.up * TurnDirection * TurnSpeed * Time.deltaTime);

        for (int i = 0; i < SnakeBody.Count; i++)
        {
            Vector3 targetPosition;
            Quaternion targetRotation;

            if (i == 0)
            {
                // Follow the head
                targetPosition = Head.transform.position - (Head.transform.forward * BodyGap);
                targetRotation = Head.transform.rotation;
            }
            else
            {
                // Follow the previous body segment
                targetPosition = SnakeBody[i - 1].transform.position - (SnakeBody[i].transform.forward * BodyGap);
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
        //last body segment position
        if(SnakeBody.Count > 0)
        {
            Vector3 LastPosition = SnakeBody[SnakeBody.Count - 1].transform.position;
            Vector3 SpawnPosition = new Vector3(LastPosition.x, LastPosition.y, LastPosition.z - BodyGap);
            GameObject body = Instantiate(SnakeBodyPrefab, SpawnPosition - Vector3.forward, SnakeBody[SnakeBody.Count-1].transform.localRotation, transform);
            SnakeBody.Add(body);
        } else
        {
            Vector3 HeadPosition = Head.transform.position;
            Vector3 SpawnPosition = new Vector3(HeadPosition.x, HeadPosition.y, HeadPosition.z - BodyGap);
            GameObject body = Instantiate(SnakeBodyPrefab, SpawnPosition - Vector3.forward, Quaternion.identity, transform);
            SnakeBody.Add(body);
        }   
    }
}
