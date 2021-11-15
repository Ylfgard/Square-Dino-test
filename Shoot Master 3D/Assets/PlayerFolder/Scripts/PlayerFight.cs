using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private MenuFunctional menuFunctional;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerMovement playerMovement; 
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform pistolPoint;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private float attackRate;
    private bool canShoot = true;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if(canShoot)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 100, ignoreLayer))
                    StartCoroutine(Shoot(hit.point));
                else
                    StartCoroutine(Shoot(ray.direction * 100));
            }
        }
    }

    private IEnumerator Shoot(Vector3 targetPosition)
    {
        canShoot = false;
        Quaternion bulletRotate = Quaternion.LookRotation(targetPosition - pistolPoint.position);
        PistolBullet bullet = Instantiate(bulletPref, pistolPoint.position, bulletRotate).GetComponent<PistolBullet>();
        bullet.direction = (targetPosition - pistolPoint.position).normalized;
        yield return new WaitForSeconds(attackRate);
        canShoot = true;
    }

    public void TakeDamage()
    {
        playerMovement.animator.SetTrigger("Death");
        StartCoroutine(menuFunctional.DelayedRestart());
    }
}
