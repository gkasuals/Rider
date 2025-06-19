using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem crashParticle; // 인스펙터에서 파티클 연결
    [SerializeField] private AudioSource crashAudio;       // 인스펙터에서 AudioSource 연결

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
