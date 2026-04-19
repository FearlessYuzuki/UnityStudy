using System.Collections.Generic;
using UnityEngine;

public class BulletPool3D : MonoBehaviour
{
    public static BulletPool3D Instance; 

    [Header("池子配置")]
    public GameObject bulletPrefab; 
    public int poolSize = 100;   

    // 核心数据结构：队列（先进先出，最适合做对象池）
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false); // 隐藏待命
            // 为了保持 Hierarchy 面板干净，让子弹全做 GameManager 的子物体
            obj.transform.SetParent(this.transform); 
            pool.Enqueue(obj);
        }
    }

    public GameObject GetBullet()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true); // 激活
            return obj;
        }
        else
        {
    
            GameObject obj = Instantiate(bulletPrefab);
            obj.transform.SetParent(this.transform);
            return obj;
        }
    }

    // 子弹飞出屏幕或打中敌人时调用：归还子弹
    public void ReturnBullet(GameObject obj)
    {
        obj.SetActive(false); // 再次隐藏
        pool.Enqueue(obj);    // 放回队列尾部
    }
}