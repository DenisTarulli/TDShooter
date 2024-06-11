using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletForce;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Vector3 bulletSpread;
    private float nextTimeToShoot;

    public float BulletDamage { get => bulletDamage; }

    private void Start()
    {
        nextTimeToShoot = Time.time;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
            Shoot();
    }

    private void Shoot()
    {
        if (Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / fireRate;
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

            Vector3 spread = new 
                (
                Random.Range(-bulletSpread.x, bulletSpread.x), 
                Random.Range(-bulletSpread.y, bulletSpread.y), 
                Random.Range(-bulletSpread.z, bulletSpread.z)
                );

            bullet.GetComponent<Rigidbody>().AddForce((spawnPoint.forward + spread) * bulletForce, ForceMode.Impulse);

            muzzleFlash.Play();
        }
    }
}
