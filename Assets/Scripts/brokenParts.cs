using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenParts : MonoBehaviour
{
    public float movementSpeed = 3f;
    public Vector3 moveDir;
    public float deceleration = 5f;
    // Start is called before the first frame update
    void Start()
    {
        moveDir.x = Random.Range(-movementSpeed, movementSpeed);
        moveDir.y = Random.Range(-movementSpeed, movementSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * Time.deltaTime;
        moveDir = Vector3.Lerp(moveDir, Vector3.zero, deceleration * Time.deltaTime);
    }
}
