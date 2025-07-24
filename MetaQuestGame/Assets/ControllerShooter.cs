using UnityEngine;

public class ControllerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float destroyAfterSeconds = 5f;

    public Transform leftControllerTransform;
    public Transform rightControllerTransform;
    public AudioClip shootSFX;
    public AudioSource audioSource;

    void Update()
    {
        // 左トリガー
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            ShootFromController(leftControllerTransform);
        }

        // 右トリガー
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            ShootFromController(rightControllerTransform);
        }
    }

    void ShootFromController(Transform controller)
    {
        if (controller == null || projectilePrefab == null) return;

        // 弾を生成
        GameObject projectile = Instantiate(projectilePrefab, controller.position + controller.forward * 0.1f, controller.rotation);

        // Rigidbodyで前方に飛ばす
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = controller.forward * shootForce;
            if (audioSource != null && shootSFX != null)
            {
                audioSource.PlayOneShot(shootSFX);
            }
        }

        // 一定時間後に削除
        Destroy(projectile, destroyAfterSeconds);
    }
    
}
