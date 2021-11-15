using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    override public IEnumerator Shoot(Vector3 targetPosition)
    {
        canShoot = false;
        Quaternion bulletRotate = Quaternion.LookRotation(targetPosition - shootPoint.position);
        Projectile bullet = Instantiate(bulletPref, shootPoint.position, bulletRotate).GetComponent<Projectile>();
        bullet.direction = (targetPosition - shootPoint.position).normalized;
        bullet.speed = bulletSpeed;
        bullet.damage = damage;
        bullet.headShotDamageScale = headShotDamageScale;
        yield return new WaitForSeconds(attackRate);
        canShoot = true;
    }
}
