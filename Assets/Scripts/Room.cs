using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeUponEntery, openUponClear;
    public GameObject[] doors;

    private bool roomActive;
    private bool spawnedOnce;

    public Transform enemySpawner;
    public GameObject enemy;
    public int enemiesToSpawn;


    public List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        spawnedOnce = false;
        roomActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        RoomLoop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EnemySpawner();
            cameraController.instance.ChangeTarget(transform);

            if (closeUponEntery)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);

                }
            }
            
        }
        roomActive = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            roomActive = false;
        }
    }

    private void EnemySpawner()
    {

        for (int i = 0;i < enemiesToSpawn; i++)
        {
            if (!spawnedOnce)
            {
                enemySpawner.localPosition = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0f);


                Vector3 spawnPosition = enemySpawner.position;

                Debug.Log($"Spawning enemy at: {spawnPosition}");


                GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);


                enemies.Add(newEnemy);

                //enemySpawner.localPosition = new Vector3(0f, 0f, 0f);

                //enemySpawner.localPosition = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0f);
                //GameObject newEnemy = Instantiate(enemy, enemySpawner.localPosition, enemySpawner.localRotation, transform);
                //enemies.Add(newEnemy);

                //Instantiate(enemy, enemySpawner.localPosition, enemySpawner.localRotation);
            }

        }
        spawnedOnce = true;
        

    }
    private void RoomLoop()
    {
        if (enemies.Count > 0 && roomActive && openUponClear)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0 && spawnedOnce)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);

                    closeUponEntery = false;
                }
            }
        }
    }
}
