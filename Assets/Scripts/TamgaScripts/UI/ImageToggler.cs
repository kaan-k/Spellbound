using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggler : MonoBehaviour
{
    public GameObject player;
    public Image tamgaImage;
    public GameObject imageToShow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        imageToShow = player.GetComponent<PlayerShooting>().firingObject;
        SpriteRenderer sr = imageToShow.GetComponent<SpriteRenderer>();
        if(sr == null)
        {
            sr = imageToShow.GetComponentInChildren<SpriteRenderer>();
        }

        tamgaImage.sprite = sr.sprite;
    }
}
