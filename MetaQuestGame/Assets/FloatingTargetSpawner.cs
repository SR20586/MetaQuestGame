using UnityEngine;

public class FloatingTargetSpawner : MonoBehaviour
{
    public GameObject[] targetPrefabs;       // 複数のターゲットプレハブ
    public Vector3 spawnAreaSize = new Vector3(5, 0, 5);  // スポーン範囲のXZサイズ
    public float spawnInterval = 1f;         // 何秒おきにスポーンするか

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTarget();
            timer = 0f;
        }
    }

    void SpawnTarget()
    {
        if (targetPrefabs.Length == 0) return;

        // ランダムな位置を計算
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0f,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        Vector3 spawnPosition = transform.position + randomPos;

        // ランダムなプレハブを選んで生成
        int index = Random.Range(0, targetPrefabs.Length);
        Instantiate(targetPrefabs[index], spawnPosition, Quaternion.identity);
    }

    // ギズモで範囲を可視化
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.25f); // 半透明緑
        Gizmos.DrawCube(transform.position, spawnAreaSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
