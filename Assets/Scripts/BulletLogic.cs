using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [Header("子弹属性")]
    public float speed = 25f;
    public float lifeTime = 2f; // 如果子弹一直没打中，2秒后自动回收，防止内存泄漏

    private float timer;
    
    private void OnEnable()
    {
        timer = 0f; 
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 存活时间检测
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            BulletPool3D.Instance.ReturnBullet(this.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)//触发部分 粒子特效 以及分数
    {
        if (other.CompareTag("Enemy"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(100);
            }
           
            Destroy(other.gameObject);
            BulletPool3D.Instance.ReturnBullet(this.gameObject);
        }
        
    }
}