using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy3D : MonoBehaviour
{
    [Header("敌人属性")]
    public float speed = 5f;

    private void Start()
    {
        InvokeRepeating("Randomize", 2f, 3f);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // 如果飞出屏幕底端，自动销毁，防止内存泄漏
        if (transform.position.z < -15f)
        {
            Destroy(gameObject);
        }
    }

    void Randomize()
    {
        speed = Random.Range(5f, 10f);
    }
}