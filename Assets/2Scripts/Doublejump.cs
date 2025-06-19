using UnityEngine;

public class Doublejump : MonoBehaviour
{
    [SerializeField] private Sprite doubleJumpSprite; // 더블점프 상태 스프라이트

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MyCarController car = other.GetComponent<MyCarController>();
        if (car != null)
        {
            Destroy(gameObject); // 아이템 소모
        }
    }
}
