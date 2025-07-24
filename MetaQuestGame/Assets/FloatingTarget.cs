using UnityEngine;

public class FloatingTarget : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifetime = 5f;
    public AudioSource audioSource;
    public AudioClip destroySFX;

    private bool isDestroyed = false;

    void Start()
    {
        // 最初にオブジェクトを有効化
        gameObject.SetActive(true);

        // 指定秒数後に自動で削除
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDestroyed) return;

        if (collision.gameObject.CompareTag("Projectile"))
        {
            isDestroyed = true;

            // 効果音用の一時オブジェクトを生成
            if (destroySFX != null)
            {
                GameObject soundObject = new GameObject("DestroySFX");
                AudioSource newSource = soundObject.AddComponent<AudioSource>();
                newSource.clip = destroySFX;
                newSource.Play();
                Destroy(soundObject, destroySFX.length);
            }

            Destroy(gameObject);
        }
    }
}
