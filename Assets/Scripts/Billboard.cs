using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    
    //THIS SCRIPT IS TO MAKE AN OBJECT(Specifically, a flat 2D object) ALWAYS FACE THE CAMERA.

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
