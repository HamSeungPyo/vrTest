using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPosFollower : MonoBehaviour
{
    public GameObject controller;
	void Update ()
    {
        transform.position = controller.transform.position;
    }
}
