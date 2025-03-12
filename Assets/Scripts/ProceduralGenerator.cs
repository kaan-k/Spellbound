using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor;
    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left };
    public Direction selectedDirection;


    public float xOffset = 18f, yOffset = 10f;

    public LayerMask defineRoom;

    private GameObject endRoom;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position,generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGeneratorPoint();

        for(int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            if(i+1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color=endColor;
                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGeneratorPoint();

            while(Physics2D.OverlapCircle(generatorPoint.position,0.2f, defineRoom))
            {
                MoveGeneratorPoint();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveGeneratorPoint()
    {
        switch(selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;

        }
    }
}
