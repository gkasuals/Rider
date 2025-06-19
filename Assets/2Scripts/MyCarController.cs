using UnityEngine;

public class MyCarController : MonoBehaviour
{
    private SurfaceEffector2D surfaceEffector2D;
    private Rigidbody2D rb;
    private bool onGround = false;
    public float torqueForce = 10f;

    public float jumpForce = 7f;
    public float speed = 10f;
    public float boostSpeed = 20f;
    public float slowSpeed = 5f;
    public float coinvalue = 0f;

    private bool isBoosting = false;
    private bool isEnding = false;

    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private AudioSource carAudio; // 인스펙터에서 AudioSource 연결

    public float minVolume = 0.2f;
    public float maxVolume = 1.0f;
    public float maxSpeedForVolume = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
            trailRenderer.emitting = false;
        }
        if (carAudio != null)
        {
            carAudio.loop = true;
            carAudio.playOnAwake = false;
            carAudio.volume = minVolume;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<SurfaceEffector2D>(out var effector))
        {
            onGround = true;
            surfaceEffector2D = effector;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<SurfaceEffector2D>(out var effector))
        {
            onGround = false;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(torqueForce);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(-torqueForce);
        }
    }
    private void Update()
    {
        if (surfaceEffector2D == null) return;
        if (isEnding) return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            surfaceEffector2D.speed = slowSpeed;
        }
        if (Input.GetKeyUp(KeyCode.D) && !Input.GetKeyUp(KeyCode.A))
        {
            surfaceEffector2D.speed = speed;
        }
        else if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            Jump();
        }

        if (coinvalue >= 10f && Input.GetKeyDown(KeyCode.Space) && !isBoosting)
        {
            StartCoroutine(SpeedBoostCoroutine());
            Debug.Log("Speed Boost Activated!");
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space) && !isBoosting)
        {
            surfaceEffector2D.speed = speed;
        }

        float surfaceSpeed = surfaceEffector2D != null ? surfaceEffector2D.speed : 0f;
        float carSpeed = rb != null ? rb.linearVelocity.magnitude : 0f;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateSurfaceSpeedText($"Surface: {surfaceSpeed:F2}");
            UIManager.Instance.UpdateCarSpeedText($"Car: {carSpeed:F2}");
        }

        if (carAudio != null)
        {
            if (onGround)
            {
                if (!carAudio.isPlaying)
                    carAudio.Play();
                float t = Mathf.Clamp01(carSpeed / maxSpeedForVolume);
                carAudio.volume = Mathf.Lerp(minVolume, maxVolume, t);
            }
            else
            {
                if (carAudio.isPlaying)
                    carAudio.Pause();
            }
        }
    }

    private System.Collections.IEnumerator SpeedBoostCoroutine()
    {
        isBoosting = true;
        if (trailRenderer != null)
            trailRenderer.emitting = true;

        coinvalue -= 10f;
        UIManager.Instance.UpdateCoinText(coinvalue);
        float originalSpeed = surfaceEffector2D.speed;
        surfaceEffector2D.speed = originalSpeed + 50f;
        yield return new WaitForSeconds(1f);
        surfaceEffector2D.speed = originalSpeed;

        if (trailRenderer != null)
            trailRenderer.emitting = false;

        isBoosting = false;
    }

    private void Jump()
    {
        onGround = false;
        if (rb == null) return;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void AddCoin(float value)
    {
        coinvalue += value;
    }

    public System.Collections.IEnumerator SmoothStopAndNotify(System.Action onStopped)
    {
        if (surfaceEffector2D == null)
            yield break;

        isEnding = true;

        float duration = 0.1f;
        float startSpeed = surfaceEffector2D.speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            surfaceEffector2D.speed = Mathf.Lerp(startSpeed, 0f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        surfaceEffector2D.speed = 0f;

        yield return new WaitForSeconds(1f);

        onStopped?.Invoke();
    }
}
