using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;

    [Header("체력")]
    public int maxHp = 3;
    private int currentHp;

    private Vector2 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
        gameObject.tag = "Enemy";
        currentHp = maxHp;
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

    public void TakeDamage()
    {
        currentHp--;

        if (currentHp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}