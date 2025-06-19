using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem crashParticle; // �ν����Ϳ��� ��ƼŬ ����
    [SerializeField] private AudioSource crashAudio;       // �ν����Ϳ��� AudioSource ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
                crashParticle.Play();
                crashAudio.Play();
            StartCoroutine(DelayedGameStop());
        }
    }

    private System.Collections.IEnumerator DelayedGameStop()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GameStop();
    }
}
