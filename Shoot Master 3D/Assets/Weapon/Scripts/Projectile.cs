using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float speed;
    [HideInInspector] public int damage;
    [HideInInspector] public int headShotDamageScale;
    private Transform _transform;

    private void Start() 
    {
        Destroy(gameObject, 5f);
        _transform = gameObject.GetComponent<Transform>();
    }

    private void Update() 
    {
        _transform.position += direction * speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Head"))
        {
            other.GetComponentInParent<EnemyBehavior>().TakeDamage(damage * headShotDamageScale);
            other.GetComponent<Rigidbody>().AddForce(direction * speed * 300);
        }
        else if(other.CompareTag("Body"))
        {
            other.GetComponentInParent<EnemyBehavior>().TakeDamage(damage);
            other.GetComponent<Rigidbody>().AddForce(direction * speed * 100);
        }
        
        gameObject.SetActive(false);
    }
}
