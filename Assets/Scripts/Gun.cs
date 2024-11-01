using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform endTrans;
    public Transform container;

    private void FixedUpdate()
    {
        if (m_BulletTimer < 0 || Time.fixedTime - m_BulletTimer > m_BulletTime)
        {
            for (int i = 0; i < 10; i++)
            {
                GenerateBullet();
            }

            m_BulletTimer = Time.fixedTime;
        }
    }

    private void GenerateBullet()
    {
        GameObject bulletGO = GameObject.Instantiate(bulletPrefab, container, false);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        float xOffset = Random.Range(-10f, 10f);
        float yOffset = Random.Range(-9f, 9f);// * (Random.Range(0, 100) > 50 ? 1 : -1);

        Vector3 midPos = transform.position + Vector3.right * xOffset + Vector3.up * yOffset;
        bulletGO.SetActive(true);
        bullet.Move(transform.position, midPos, endTrans.position);
    }

    private float m_BulletTime = 0.1f;
    private float m_BulletTimer = -1;
}
