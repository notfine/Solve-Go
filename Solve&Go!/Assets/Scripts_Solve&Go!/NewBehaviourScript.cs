using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    playerMovement player;
    float xSpeed;
    [SerializeField] float bulletSpeed = 10f;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemies")
        {
            Destroy(other.gameObject);
        }   
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
