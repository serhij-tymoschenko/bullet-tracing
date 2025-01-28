using System;
using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float fireRate;
    [SerializeField] private Vector3 gameArea;
    private bool canFire = true;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
    
    private void Fire()
    {
        if (canFire)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bullet = bulletInstance.GetComponent<Rigidbody>();

            Vector2 mousePos = new Vector2(
                Input.mousePosition.x - Screen.width / 2,
                Input.mousePosition.y - Screen.height / 2
            );
        
            Vector3 trajectory = BulletTrajectory(mousePos);
            
            IEnumerator routine = SpawnBullet(bulletInstance, bullet, trajectory);
            StartCoroutine(routine);
           
            canFire = false;
            StartCoroutine(SetDelay());
        }
    }

    private Vector3 BulletTrajectory(Vector2 mousePos)
    {
        float angle = Mathf.Atan2(mousePos.x, mousePos.y);
        return new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
    }
    
    private IEnumerator SpawnBullet(
        GameObject bulletInstance,
        Rigidbody bullet,
        Vector3 trajectory
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

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(1 / fireRate);
        canFire = true;
        
        yield return null;
    }
}