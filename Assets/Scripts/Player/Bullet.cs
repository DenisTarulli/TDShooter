using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float hitEffectDuration;
    [SerializeField] private float rayCheckDistance;
    [SerializeField] private GameObject hitEffectWall;
    [SerializeField] private GameObject hitEffectEnemy;
    [SerializeField] private Transform hitEffectRotation;

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))        
            HitEffect(hitEffectEnemy);        
        else        
            HitEffect(hitEffectWall);
    }

    private void HitEffect(GameObject hitPrefab)
    {
        GameObject hit = Instantiate(hitPrefab, transform.position, hitEffectRotation.rotation);
        Destroy(hit, hitEffectDuration);
        Destroy(gameObject);
    }
}
