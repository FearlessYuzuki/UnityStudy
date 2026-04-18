using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [Header("子弹属性")]
    public float speed = 25f;
    public float lifeTime = 2f; // 如果子弹一直没打中，2秒后自动回收，防止内存泄漏

    private float timer;

    // 注意：对象池里的子弹不能用 Start() 初始化时间，必须用 OnEnable()！
    // 因为子弹没有被销毁，只是被反复激活和隐藏
    private void OnEnable()
    {
        timer = 0f; 
    }

    private void Update()
    {
        // 2.5D 俯视角下，Z 轴 (Vector3.forward) 就是我们的“前方”
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 存活时间检测
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            // 时间到了，乖乖回池子里，绝对不能用 Destroy()！
            BulletPool3D.Instance.ReturnBullet(this.gameObject);
        }
    }

    // 预留的碰撞接口：打中敌人后回收自己
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 这里以后可以写触发粒子爆炸特效的代码
            Destroy(other.gameObject);
            BulletPool3D.Instance.ReturnBullet(this.gameObject);
        }
    }
}