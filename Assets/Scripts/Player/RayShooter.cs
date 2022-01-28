using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot){
            ShootRaycast();
        }
    }

    void ShootRaycast(){
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, 2f, LayerMask.GetMask("Hittable"))){
            hitInfo.collider.gameObject.SendMessage("Hit");
        }
    }
}
