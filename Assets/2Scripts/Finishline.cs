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
                // ���� ó��: MyCarController�� ������ ���� ������� ��� ����
                GameManager.Instance.GameStop();
            }
        }
    }
}
