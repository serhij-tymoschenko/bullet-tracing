using System;
using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private Vector3 gameArea;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var bullet = bulletInstance.GetComponent<Rigidbody>();

            Vector2 mousePos = new Vector2(
                Input.mousePosition.x - Screen.width / 2,
                Input.mousePosition.y - Screen.height / 2
            );
            
            StartCoroutine(SpawnBullet(bullet, BulletTrajectory(mousePos), bulletInstance));
        }
    }

    private Vector3 BulletTrajectory(Vector2 mousePos)
    {
        float angle = Mathf.Atan2(mousePos.x, mousePos.y);
        return new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
    }
    
    private IEnumerator SpawnBullet(
        Rigidbody bullet,
        Vector3 trajectory,
        GameObject bulletInstance
    )
    {
        while (Math.Abs(bullet.position.x) < gameArea.x / 2
               && Math.Abs(bullet.position.z) < gameArea.z / 2)
        {
            bullet.MovePosition(bullet.position + trajectory * bulletSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(0.5F);
        Destroy(bulletInstance);

        yield return null;
    }
}