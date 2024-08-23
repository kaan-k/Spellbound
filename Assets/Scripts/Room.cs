using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeUponEntery, openUponClear;
    public GameObject[] doors;

    private bool roomActive;

    public List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count > 0 && roomActive && openUponClear)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if(enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);

                    closeUponEntery = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
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
}
