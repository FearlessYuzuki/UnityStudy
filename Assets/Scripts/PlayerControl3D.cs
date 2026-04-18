using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 10f;
    
    [Header("射击属性")]
    public Transform firePoint; // 子弹发射点（枪口位置）
    public float fireRate = 0.15f; // 射击间隔，数字越小射速越快
    private float nextFireTime = 0f;
    
    private float xMin, xMax, zMin, zMax;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        CalculateBoundaries();//开始计算边界
    }

    private void CalculateBoundaries()
    {
        //计算高度(calculate hight to players
        float camdistanceToPlayer = mainCam.transform.position.y - transform.position.y;
        
        //坐标转换(Location transition)
        Vector3 bottomLeft = mainCam.ScreenToWorldPoint(new Vector3(0, 0, camdistanceToPlayer));
        Vector3 topRight = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camdistanceToPlayer));
        
        xMin = bottomLeft.x ;
        xMax = topRight.x ;
        zMin = bottomLeft.z ; 
        zMax = topRight.z ;
    }

    private void Update()
    {
        //CalculateBoundaries();//持续计算 之前切分辨率会卡出去 为了自己调试方便
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        //bound检查
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(transform.position.z, zMin, zMax);
        
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
        
        // --- 射击逻辑 ---
        // GetKey 表示按住不放就能一直连发
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void Shoot()
    {
        // 核心亮点：不 Instantiate，而是向池子申请子弹
        GameObject bullet = BulletPool3D.Instance.GetBullet();
        
        // 如果你设置了具体的枪口位置，就用枪口位置；如果没有，就从玩家中心发射
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        
        bullet.transform.position = spawnPos;
        bullet.transform.rotation = transform.rotation;
    }
}
