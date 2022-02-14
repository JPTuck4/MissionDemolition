/***
 * Date Created: 2/9/22
 * Created By: JP Tucker
 * 
 * Last Edited On: 2/14/22
 * Last Editied By: JP
 * 
 * Description: activates the slingshot
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector)")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRB;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); // finds launchpoint object
        launchPoint = launchPointTrans.gameObject; //references object
        launchPoint.SetActive(false); //disable halo
        launchPos = launchPointTrans.position; //references position
    }

    void Update()
    {
        // If Slingshot is not in aimingMode, don't run this code
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition; //get current mouse position
        mousePos2D.z = -Camera.main.transform.position.z; //set z
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; // Find the delta from the launchPos to the mousePos3D

        // Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize(); //same direction but length 1
            mouseDelta *= maxMagnitude;
        }
        // Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
          // The mouse has been released
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;
            FollowCam.POI = projectile; //set camera
            projectile = null;

        }
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true); //turn on halo
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false); //turn off halo
    }

    void OnMouseDown()
    {
        aimingMode = true; //player is aiming
        projectile = Instantiate(prefabProjectile) as GameObject; //instantiate projectile
        projectile.transform.position = launchPos; //start at launchPoint
        projectileRB = projectile.GetComponent<Rigidbody>(); //initalize RB
        projectileRB.isKinematic = true; //make projectile kinematic
    }
}
