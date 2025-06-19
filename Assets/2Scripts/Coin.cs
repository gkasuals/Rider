using UnityEngine;
public class Coin : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public AudioSource audioSource;
    public float Coinvalue = 0f;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var car = other.GetComponent<MyCarController>();
            if (car != null)
            {
                car.coinvalue += 1f;
                UIManager.Instance.UpdateCoinText(car.coinvalue);
            }
            particleSystem.Play();
            audioSource.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}
