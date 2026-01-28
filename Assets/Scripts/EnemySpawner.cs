using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("スポーン設定 (Spawn Settings)")]
    [Tooltip("出現させる敵のプレハブリスト")]
    [SerializeField] private List<GameObject> enemyPrefabs;

    [Tooltip("スポーン範囲 (幅, 高さ, 奥行き)")]
    [SerializeField] private Vector3 spawnArea = new Vector3(10f, 0f, 10f);

    [Tooltip("出現間隔（秒）")]
    [SerializeField] private float spawnInterval = 2f;

    void Start()
    {
        // 定期的にお敵を生成するコルーチンを開始
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 設定した間隔待機
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // プレハブが登録されていない場合は処理しない
        if (enemyPrefabs == null || enemyPrefabs.Count == 0) return;

        // 1. リストからランダムに敵を一つ選ぶ
        int index = Random.Range(0, enemyPrefabs.Count);
        GameObject selectedPrefab = enemyPrefabs[index];

        // 2. スポーン範囲内でランダムな位置を計算
        // (transform.positionを中心として、spawnAreaの範囲内にばらつかせる)
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        Vector3 spawnPos = transform.position + randomPos;

        // 3. 敵を生成
        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }

    // エディター上でスポーン範囲を可視化する（選択時のみ表示）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); // 赤色
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}