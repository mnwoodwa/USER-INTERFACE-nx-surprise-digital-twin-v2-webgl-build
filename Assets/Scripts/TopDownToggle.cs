using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownToggle : MonoBehaviour
{
    private GameObject mainCam;
    private Camera camComponent;
    private bool isOrthographic;
    private bool transitioning;
    private float defaultFOV;

    //Distance to zoom out in orthographic mode
    private float camDist = 0f;
    public float defaultCamDist;
    private float currentCamDist;
    private float currentCamY;
    private float currentCamRotationX;

    private Vector3 lookAtTarget;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = this.gameObject;
        camComponent = mainCam.GetComponent<Camera>();
        defaultFOV = camComponent.fieldOfView;
        transitioning = false;
        defaultCamDist = mainCam.transform.localPosition.z;
        currentCamRotationX = mainCam.transform.localRotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Initiate switch from Perpective to Orthographic
        if(isOrthographic == false && Input.GetKeyUp(KeyCode.Space))
        {
            isOrthographic = true;
            transitioning = true;
            Debug.Log("Call ortho");

            lookAtTarget = new Vector3(mainCam.transform.position.x, 1f, mainCam.transform.position.z +10f);
        }
        //Initiate switch Orthographic to Perspective
        else if(isOrthographic == true && Input.GetKeyUp(KeyCode.Space))
        {
            isOrthographic = false;
            transitioning = true;
            Debug.Log("Call persp");

            lookAtTarget = new Vector3(mainCam.transform.position.x, 1f, mainCam.transform.position.z);
        }

        
    }

    void FixedUpdate() 
    {
        //Transition from Perspective to Orthographic
        if(transitioning == true && isOrthographic == true)
        {
            camComponent.fieldOfView = Mathf.Lerp(camComponent.fieldOfView, 9.8f, 0.07f);

            

            //Distance Lerping
                //Translate on Z
                currentCamDist = Mathf.Lerp(currentCamDist, camDist, 0.07f);
                mainCam.transform.localPosition = new Vector3(mainCam.transform.localPosition.x, mainCam.transform.localPosition.y, currentCamDist);

                //Translate on Y
                currentCamY = Mathf.Lerp(currentCamY, 100f, 0.07f);
                mainCam.transform.localPosition = new Vector3(mainCam.transform.localPosition.x, currentCamY, mainCam.transform.localPosition.z);

            //Rotation Lerping
            currentCamRotationX = Mathf.Lerp(currentCamRotationX, 90f, 0.07f);
            mainCam.transform.localRotation = Quaternion.Euler( new Vector3(currentCamRotationX, mainCam.transform.localRotation.y, mainCam.transform.localRotation.z));

            mainCam.transform.LookAt(lookAtTarget);
        }
        //Transition from Orthographic to Perspective
        else if (transitioning == true && isOrthographic == false)
        {
            camComponent.fieldOfView = Mathf.Lerp(camComponent.fieldOfView, defaultFOV + 0.2f, 0.07f);

            //Distance Lerping
                //Translate on z
                currentCamDist = Mathf.Lerp(currentCamDist, defaultCamDist, 0.07f);
                mainCam.transform.localPosition = new Vector3(mainCam.transform.localPosition.x, mainCam.transform.localPosition.y, currentCamDist);

                //Translate on Y
                currentCamY = Mathf.Lerp(currentCamY, 1f, 0.07f);
                mainCam.transform.localPosition = new Vector3(mainCam.transform.localPosition.x, currentCamY, mainCam.transform.localPosition.z);

            //Position Lerping

            //Rotation Lerping
            currentCamRotationX = Mathf.Lerp(currentCamRotationX, 0f, 0.07f);
            mainCam.transform.localRotation = Quaternion.Euler( new Vector3(currentCamRotationX, mainCam.transform.localRotation.y, mainCam.transform.localRotation.z));

            mainCam.transform.LookAt(lookAtTarget);
        }

        //Finish transition
        if(transitioning == true && (camComponent.fieldOfView == 9.9f || camComponent.fieldOfView == defaultFOV + 0.1f ))
        {
            transitioning = false;
            Debug.Log("Finish Transition");
        }
    }
}
