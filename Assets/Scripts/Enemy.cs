using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;

    private Vector2 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
        gameObject.tag = "Enemy";
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            direction *= -1;
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    // Is Trigger 대신 OnCollisionEnter2D 사용
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}