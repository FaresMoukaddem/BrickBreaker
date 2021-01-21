using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCreator : MonoBehaviour
{
    public static BrickCreator instance;

    public int rows, columns = 1;

    public GameObject brick;

    public float workspaceWidth;

    public float startingYPos;

    public float step;

    private float brickHeight, brickWidth, pointerStepX, pointerStepY;

    private Transform brickParent, pointer;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    void Start()
    {
        brickParent = transform.GetChild(0);
        pointer = transform.GetChild(1);
    }

    public void Configure(LevelScriptableObject level)
    {
        rows = level.rows;
        columns = level.columns;
    }

    // Start is called before the first frame update
    public void Generate()
    {
        foreach(Transform child in brickParent)
        {
            Destroy(child.gameObject);
        }

        pointer.position = new Vector3(0, startingYPos, 0);

        brickHeight = brick.transform.localScale.y;

        pointerStepX = workspaceWidth / columns;

        brickWidth = pointerStepX - step;

        pointerStepY = brickHeight + step;

        if (brickWidth > 0 && rows > 0 && columns > 0 && workspaceWidth > 0)
        {
            CreateBrickWall();
        }
        else
        {
            Debug.LogError("Invalid input in BrickCreator");
        }
    }

    void CreateBrickWall()
    {
        pointer.Translate(transform.right * -1 * workspaceWidth / 2);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject g = Instantiate(brick, pointer.position, Quaternion.identity, brickParent);

                g.transform.localScale = new Vector3(brickWidth, brickHeight, 1);

                pointer.Translate(transform.right * pointerStepX);
            }

            pointer.Translate(transform.up * pointerStepY);
            pointer.position = new Vector3(0 - workspaceWidth/2, pointer.position.y, pointer.position.z);
        }

        brickParent.Translate(brickParent.right * step / 2);

    }
}