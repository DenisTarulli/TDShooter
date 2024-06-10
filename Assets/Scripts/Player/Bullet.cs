using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float hitEffectDuration;
    [SerializeField] private float rayCheckDistance;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Transform hitEffectRotation;

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = Instantiate(hitEffect, transform.position, hitEffectRotation.rotation);
        Destroy(hit, hitEffectDuration);
        Destroy(gameObject);
    }
}
