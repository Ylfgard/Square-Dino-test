using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [HideInInspector] public bool canShoot = true;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected float attackRate;
    [SerializeField] protected GameObject bulletPref;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected int headShotDamageScale;

    public abstract IEnumerator Shoot(Vector3 targetPosition);
}
