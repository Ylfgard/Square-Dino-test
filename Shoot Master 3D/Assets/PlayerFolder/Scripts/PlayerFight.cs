using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private MenuFunctional menuFunctional;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerMovement playerMovement; 
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private GameObject startWeapon;
    [SerializeField] private LayerMask ignoreLayer;
    private Weapon weaponInHand;

    private void Start() 
    {
        TakeWeapon(startWeapon);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if(weaponInHand.canShoot)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 100, ignoreLayer))
                    StartCoroutine(weaponInHand.Shoot(hit.point));
                else
                    StartCoroutine(weaponInHand.Shoot(ray.direction * 100));
            }
        }
    }

    public void TakeWeapon(GameObject weapon)
    {
        weaponInHand = Instantiate(startWeapon, weaponPoint.position, weaponPoint.rotation).GetComponent<Weapon>();
        weaponInHand.transform.SetParent(weaponPoint);
    }

    public void TakeDamage()
    {
        playerMovement.animator.SetTrigger("Death");
        StartCoroutine(menuFunctional.DelayedRestart());
    }
}
