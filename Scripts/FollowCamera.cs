using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // This things position should be the same as the car's position
    [SerializeField] GameObject thingToFollow;

    //Late Update is the last thing to be updated in the update loop in Unity.
    void LateUpdate()
    {
        transform.position = thingToFollow.transform.position + new Vector3(0,0,-10);
    }
}
