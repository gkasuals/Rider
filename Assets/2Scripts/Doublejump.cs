using UnityEngine;

public class Doublejump : MonoBehaviour
{
    [SerializeField] private Sprite doubleJumpSprite; // �������� ���� ��������Ʈ

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
            Destroy(gameObject); // ������ �Ҹ�
        }
    }
}
