using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour
{

    public static BallPooler instance;

    public int numberOfBalls;

    public GameObject ballPrefab;

    public Vector3 ballStartingPosition;

    public Transform ballsParent;

    Queue<GameObject> balls = new Queue<GameObject>();

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject g;

        for(int i = 0; i < numberOfBalls; i++)
        {
            g = Instantiate(ballPrefab, ballStartingPosition, Quaternion.identity, ballsParent);
            g.gameObject.SetActive(false);
            balls.Enqueue(g);
        }
    }

    public void PutBallBackIntoPool(GameObject ball, bool withNewBall)
    {
        ball.SetActive(false);
        balls.Enqueue(ball);

        if (withNewBall)
            SpawnNewBallFromPool();
    }

    public void SpawnNewBallFromPool()
    {
        GameObject ball = balls.Dequeue();
        ball.transform.position = ballStartingPosition;
        ball.SetActive(true);
    }
}
