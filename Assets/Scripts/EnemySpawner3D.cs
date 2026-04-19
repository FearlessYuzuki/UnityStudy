using UnityEngine;

public class EnemySpawner3D : MonoBehaviour
{
    [Header("生成配置")]
    public GameObject enemyPrefab; 
    public float spawnRate = 2f;   
    
    [Header("生成位置范围")]
    public float spawnZ = 12f;     
    public float minX = -10f;     
    public float maxX = 10f;    

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