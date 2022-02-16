/***
 * Date Created: 2/14/22
 * Created By: JP Tucker
 * 
 * Last Edited On: 2/16/22
 * Last Editied By: JP
 * 
 * Description: follows projectiles
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    static public GameObject POI; // The static point of interest

    [Header(" Set Dynamically")]
    public float camZ; // The desired Z pos of the camera
    void Awake()
    {
        camZ = this.transform.position.z;
    }
    void FixedUpdate()
    {
        //// if there's only one line following an if, it doesn't need braces
        //if (POI == null) return; // return if there is no poi
        //                         // Get the position of the poi
        //Vector3 destination = POI.transform.position;
        Vector3 destination;
        // If there is no poi, return to P:[ 0, 0, 0 ]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Get the position of the poi
            destination = POI.transform.position;
            // If poi is a Projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                // if it is sleeping (that is, not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    POI = null;
                    // in the next update
                    return;
                }
            }
        }

        //limit x and y values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
        //set orthographic size to keep in ground view
        Camera.main.orthographicSize = destination.y + 10;
    }
}
