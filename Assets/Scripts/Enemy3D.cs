using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    [Header("敌人属性")]
    public float speed = 5f;

    private void Update()
    {
        // 2.5D 俯视角下，Z 轴的负方向（Vector3.back）就是屏幕的正下方
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // 如果飞出屏幕底端，自动销毁，防止内存泄漏
        // (Z < -15 只是个大概值，你可以根据你的摄像机高度调整)
        if (transform.position.z < -15f)
        {
            Destroy(gameObject);
        }
    }
}