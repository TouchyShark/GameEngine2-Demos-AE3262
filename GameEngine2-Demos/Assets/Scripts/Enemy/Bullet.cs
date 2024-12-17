using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;

        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hitTransform.GetComponent<PlayerHealth>()?.TakeDamage(10); // Null-check PlayerHealth
        }

        Destroy(gameObject); // Destroy bullet on any collision
    }
}
