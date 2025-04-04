using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] breakables;
    private int piecesToDrop;
    public int maxPieces = 3;
    public int breakableHealth =3;


    public bool shouldDropItem = false;
    public GameObject[] itemsToDrop;
    public float dropChangePercentage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerDash>().dashCounter > 0)
            {
                Destroy(this.gameObject);
                piecesToDrop = Random.Range(1, maxPieces);

                for(int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, breakables.Length);
                    Instantiate(breakables[randomPiece], transform.position,transform.rotation);
                    shouldDropItem = true;
                }
            }
            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if(dropChance < dropChangePercentage)
                {
                    int randomItem = Random.Range(0,itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);

                }
            }

        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            breakableHealth--;
            if (breakableHealth < 0)
            {
                Destroy(this.gameObject);
                piecesToDrop = Random.Range(1, maxPieces);

                for (int i = 0; i < piecesToDrop; i++)
                {
                    int randomPiece = Random.Range(0, breakables.Length);
                    Instantiate(breakables[randomPiece], transform.position, transform.rotation);
                    shouldDropItem = true;
                }
            }
            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if (dropChance < dropChangePercentage)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);

                }
            }
        }
        
    }
}
