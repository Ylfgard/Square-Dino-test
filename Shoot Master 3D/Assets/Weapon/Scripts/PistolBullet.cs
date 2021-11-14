using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int headShotDamageScale;
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

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Enter!");
        if(other.CompareTag("Head"))
        {
            other.GetComponentInParent<EnemyBehavior>().TakeDamage(damage * headShotDamageScale);
        }
        else if(other.CompareTag("Body"))
        {
            other.GetComponentInParent<EnemyBehavior>().TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
