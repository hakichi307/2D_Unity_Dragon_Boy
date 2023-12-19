using UnityEngine;
using System.Collections;

public class Princess : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private float leftEdge;
    private float rightEdge;
    private SpriteRenderer spriteRenderer;
    private bool canMove = true; // Biến kiểm soát di chuyển
    private bool isWaiting = false; // Biến kiểm soát trạng thái đang đứng tại chỗ
    private bool movingLeft = true; // Declare movingLeft at the class level

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMove && !isWaiting)
            MoveAutomatically();
    }

    private void MoveAutomatically()
    {
        // NPC tự động di chuyển qua trái và phải
        if (transform.position.x > leftEdge && movingLeft)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < rightEdge && !movingLeft)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            movingLeft = !movingLeft;
            FlipCharacterDirection();
            StartCoroutine(WaitAndResume());
        }
    }

    private void FlipCharacterDirection()
    {
        // Lật hình nhân vật khi chạm vào leftEdge hoặc rightEdge
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator WaitAndResume()
    {
        // Đứng tại chỗ 3 giây
        isWaiting = true;
        yield return new WaitForSeconds(3f);
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem nhân vật có chạm vào không
        if (collision.tag == "Player")
        {
            // Dừng di chuyển khi chạm vào nhân vật
            canMove = false;
        }
    }
}
