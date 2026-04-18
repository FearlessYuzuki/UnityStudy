using UnityEngine;

public class EnemySpawner3D : MonoBehaviour
{
    [Header("生成配置")]
    public GameObject enemyPrefab; // 拖入你的敌人预制体
    public float spawnRate = 2f;   // 每 2 秒生成一个
    
    [Header("生成位置范围")]
    public float spawnZ = 12f;     // 在屏幕顶端外生成
    public float minX = -10f;      // 屏幕最左侧
    public float maxX = 10f;       // 屏幕最右侧

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // 随机一个 X 坐标
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, spawnZ);
        
        // 生成敌人
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}