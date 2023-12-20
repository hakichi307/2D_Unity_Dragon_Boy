using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pine_tree : MonoBehaviour
{
    private AudioSource christmasSound;



    private void Start()
    {
        christmasSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            christmasSound.Play();
        }
    }

   
}
