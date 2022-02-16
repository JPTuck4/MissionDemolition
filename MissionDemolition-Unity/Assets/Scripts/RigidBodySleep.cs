/***
 * Date Created: 2/16/22
 * Created By: JP Tucker
 * 
 * Last Edited On: 2/16/22
 * Last Editied By: JP
 * 
 * Description: sets objects to sleep
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RigidBodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
