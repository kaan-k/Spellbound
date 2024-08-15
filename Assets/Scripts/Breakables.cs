using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] breakables;
    private int piecesToDrop;
    public int maxPieces = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(PlayerController.instance.dashCounter > 0)
            {
                Destroy(this.gameObject);
                piecesToDrop = Random.Range(1, maxPieces);

                for(int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(1, breakables.Length);
                    Instantiate(breakables[randomPiece], transform.position,transform.rotation);
                }


            }

        }
        
    }
}
