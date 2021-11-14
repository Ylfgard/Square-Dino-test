using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform pistolPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRate;
    private bool canShoot = true;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if(canShoot && Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100, playerLayer))
            {
                StartCoroutine(Shoot(hit.point));
            }
        }
    }

    private IEnumerator Shoot(Vector3 targetPosition)
    {
        canShoot = false;
        Quaternion rotate = Quaternion.LookRotation(Vector3.up, targetPosition);
        PistolBullet bullet = Instantiate(bulletPref, pistolPoint.position, rotate).GetComponent<PistolBullet>();
        bullet.direction = (targetPosition - pistolPoint.position).normalized;
        yield return new WaitForSeconds(attackRate);
        canShoot = true;
    }

    public void TakeDamage()
    {
        Debug.Log("You died!");
        Time.timeScale = 0;
    }
}
