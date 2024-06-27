using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float hitRadius = .2f;
    float speed = 3;
    Vector3 direction;

    public void setDirection(Vector3 dir)
    {
        direction = dir;
        transform.forward = dir;
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
        speed += 1f;

        Collider[] targets = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (var item in targets)
        {
            if (item.tag == "Enemy")
            {
                item.GetComponent<Alien>().Die();
                Destroy(gameObject);
            }
        }
    }
}
