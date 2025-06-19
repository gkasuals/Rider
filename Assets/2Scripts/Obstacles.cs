

using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // �̵� ���� ����
    public float moveAmplitude = 2f;   // ���Ʒ� �̵� ����
    public float moveSpeed = 2f;       // �̵� �ӵ�

    // ȸ�� ���� ����
    public float rotateSpeed = 360f;   // �ʴ� ȸ�� ����(��)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // �ð�������� ������ ȸ�� (��Ϲ��� ����)
        transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameStop();
        }
    }
}
