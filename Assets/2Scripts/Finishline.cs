using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play();
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            MyCarController car = other.GetComponent<MyCarController>();
            if (car != null)
            {
                StartCoroutine(car.SmoothStopAndNotify(() => GameManager.Instance.GameStop()));
            }
            else
            {
                // 예외 처리: MyCarController가 없으면 기존 방식으로 즉시 정지
                GameManager.Instance.GameStop();
            }
        }
    }
}
