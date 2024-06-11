using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStopper : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x,40, Player.transform.position.z);
    }
}
