using UnityEngine;

public class Sushi : MonoBehaviour
{
    void Start()
    {
        gameObject.tag = "Sushi";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 먼저 비활성화 후 GameManager 호출
            gameObject.SetActive(false);
            GameManager.Instance.SushiCollected();
            Destroy(gameObject);
        }
    }
}