using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector2 direction;

    [Header("설정")]
    public float lifeTime = 3f;

    public void Init(Vector2 dir, float spd)
    {
        direction = dir;
        speed = spd;
        Destroy(gameObject, lifeTime);

        // 방향에 따라 총알 회전
        if (direction == Vector2.left)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage();

            Destroy(gameObject);
        }

        // 벽에 닿으면 사라짐
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}