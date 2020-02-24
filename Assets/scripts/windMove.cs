using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed = 1.0f;
    public Vector3 test;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(rotateSpeed * Time.deltaTime ,90 ,-90);
        transform.RotateAround(transform.position, test, rotateSpeed * Time.deltaTime);
    }
}
