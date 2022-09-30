using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] int pointsForCoinPickup = 100;

    bool wascollected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wascollected)
        {
            wascollected = true;
            FindObjectOfType<gameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            //gameObject.SetActive(false);
            Destroy(gameObject);
           
        }
    }
    
}
