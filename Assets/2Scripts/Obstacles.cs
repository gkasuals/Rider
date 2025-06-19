

using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // 이동 관련 변수
    public float moveAmplitude = 2f;   // 위아래 이동 범위
    public float moveSpeed = 2f;       // 이동 속도

    // 회전 관련 변수
    public float rotateSpeed = 360f;   // 초당 회전 각도(도)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // 시계방향으로 빠르게 회전 (톱니바퀴 연출)
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
