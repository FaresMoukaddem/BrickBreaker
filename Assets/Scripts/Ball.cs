using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    

    public float speed;

    private Rigidbody rb;

    private Vector3 direction;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        speed = GameManager.instance.levels[GameManager.instance.currentLevelIndex].speed;
        direction = new Vector3((speed * 0.2f) * (UnityEngine.Random.Range(0, 2) > 0 ? 1 : -1), -(speed * 0.8f), 0);
        rb.velocity = direction;
        Debug.Log("initial vel: " + direction);
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 hitNormal = collision.contacts[0].normal;

        bool hitSide = false;

        //Debug.Log("before col: " + direction);

        if (Vector3.Dot(hitNormal, Vector3.right) > 0 || Vector3.Dot(hitNormal, Vector3.right) < 0)
        {
            hitSide = true;
            direction = new Vector3(direction.x * -1, direction.y, direction.z);
        }
        else if (Vector3.Dot(hitNormal, Vector3.up) > 0 || Vector3.Dot(hitNormal, Vector3.up) < 0)
        {
            direction = new Vector3(direction.x, direction.y * -1, direction.z);
        }

        if (collision.gameObject.tag == "Player")
        {
            float distanceFromCenter = Vector3.Distance(collision.contacts[0].point, collision.gameObject.transform.position);
            distanceFromCenter = float.Parse(String.Format("{0:0.0}", distanceFromCenter));

            if (!hitSide && distanceFromCenter > 0.0f)
            {
                bool isMovingHorizontallyPositive = direction.x >= 0;

                direction = new Vector3((distanceFromCenter * speed) * (isMovingHorizontallyPositive ? 1 : -1), speed - (distanceFromCenter * speed), direction.z);
            }
        }

        //Debug.Log("after col: " + direction);

        rb.velocity = direction;

        if (collision.gameObject.tag == "Brick")
        {
            Destroy(collision.gameObject);
            GameManager.instance.AddScore(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lose")
        {
            GameManager.instance.currentLives--;

            UIHandler.instance.SetLivesText(GameManager.instance.currentLives);

            BallPooler.instance.PutBallBackIntoPool(gameObject, GameManager.instance.currentLives > 0);

            if(GameManager.instance.currentLives <= 0)
            {
                GameManager.instance.PromptRestartLevel();
            }
        }
    }

}
