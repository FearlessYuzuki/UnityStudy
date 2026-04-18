using System.Collections.Generic;
using UnityEngine;

public class BulletPool3D : MonoBehaviour
{
    public static BulletPool3D Instance; // 单例模式，让全宇宙都能轻易呼叫它

    [Header("池子配置")]
    public GameObject bulletPrefab; // 你的 3D 子弹预制体
    public int poolSize = 100;      // 弹幕游戏，初始池子可以设大一点

    // 核心数据结构：队列（先进先出，最适合做对象池）
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        // 经典的单例初始化
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 游戏还没开始（Awake阶段），我们就在后台偷偷造好100发子弹并藏起来
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false); // 隐藏待命
            // 为了保持 Hierarchy 面板干净，让子弹全做 GameManager 的子物体
            obj.transform.SetParent(this.transform); 
            pool.Enqueue(obj);
        }
    }

    // 射击时调用：借用子弹
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
            // 如果同屏子弹超过100发，池子被掏空了，我们就临时加班造一颗
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