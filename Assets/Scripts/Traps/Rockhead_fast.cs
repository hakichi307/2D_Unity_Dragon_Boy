using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockhead_fast : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float returnSpeed; // Tốc độ khi trở về vị trí ban đầu
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[2];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private bool returning;
    private Vector3 initialPosition;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void OnEnable()
    {
        initialPosition = transform.position;
        Stop();
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else if (returning)
        {
            MoveToInitialPosition();
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
        returning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
        StartCoroutine(ReturnToInitialPositionAfterDelay(3f));
    }

    private void MoveToInitialPosition()
    {
        // Tăng tốc độ khi trở về vị trí ban đầu
        float currentReturnSpeed = returnSpeed;

        // Di chuyển trở về vị trí ban đầu
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, currentReturnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            returning = false;
            initialPosition = transform.position;
        }
    }

    private IEnumerator ReturnToInitialPositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        returning = true;
    }
}
