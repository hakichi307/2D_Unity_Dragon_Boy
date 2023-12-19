using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide_ways_updown : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float upEdge;
    private float downEdge;

    private void Awake()
    {
        upEdge = transform.position.y - movementDistance;
        downEdge = transform.position.y + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.y > upEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.y < downEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
