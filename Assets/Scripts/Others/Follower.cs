using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{

    public Transform toFollow;
    public bool axisY, axisX;

    void Update()
    {
        if(axisX && axisY)
            transform.position = new Vector3(toFollow.position.x, toFollow.position.y, 0);            
        if (axisX)
            transform.position = new Vector3(toFollow.position.x,transform.position.y,0);
        if(axisY)
            transform.position = new Vector3(transform.position.x, toFollow.position.y, 0);
    }
}
