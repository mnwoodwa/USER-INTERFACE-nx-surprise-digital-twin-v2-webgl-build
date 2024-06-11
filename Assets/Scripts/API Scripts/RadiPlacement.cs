using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiPlacement : MonoBehaviour
{
    [SerializeField] 
    private GameObject MileRadiCircle;
    [SerializeField] 
    private GameObject MileRadiDome;
    [SerializeField] 
    private Boolean Dimension3D;
    
    private GameObject InstantiatedDisplay;
    

    private void Update()
    {
        if(Dimension3D == true)
        {
            InstantiatedDisplay = MileRadiDome;
        }
        else
        {
            InstantiatedDisplay = MileRadiCircle;
        }

    }
    public void PlaceRadi(GameObject map_)
    {
        if (Dimension3D == true)
        {
            InstantiatedDisplay = MileRadiDome;
        }
        else
        {
            InstantiatedDisplay = MileRadiCircle;
            
        }
        GameObject instantiatedObject = Instantiate(InstantiatedDisplay);
        instantiatedObject.transform.SetParent(map_.transform, false);
        instantiatedObject.transform.localPosition = new Vector3(0,0,1);
        instantiatedObject.transform.localRotation = new Quaternion(0, 0, 0, 0);

    }
}
