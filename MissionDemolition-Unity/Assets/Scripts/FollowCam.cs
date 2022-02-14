/***
 * Date Created: 2/14/22
 * Created By: JP Tucker
 * 
 * Last Edited On: 2/14/22
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

    static public GameObject POI; // The static point of interest // a
    [Header(" Set Dynamically")]
    public float camZ; // The desired Z pos of the camera
    void Awake()
    {
        camZ = this.transform.position.z;
    }
    void FixedUpdate()
    {
        // if there's only one line following an if, it doesn't need braces
        if (POI == null) return; // return if there is no poi
                                 // Get the position of the poi
        Vector3 destination = POI.transform.position;
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
