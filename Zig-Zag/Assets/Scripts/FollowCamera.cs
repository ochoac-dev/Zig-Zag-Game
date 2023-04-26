using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{


    public Transform target;

    private Vector3 offset;
    // Start is called before the first frame update
    void Awake()
    {

        offset = transform.position - target.position;
        
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;  
    }


}
