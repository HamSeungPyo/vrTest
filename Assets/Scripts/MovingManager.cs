using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    public GameObject[] controller = new GameObject[2];
    public GameObject moveAxis;
	void Start ()
    {
		
	}
	
	void Update ()
    {
        Vector3[] controllerPos = { controller[0].transform.localPosition, controller[1].transform.localPosition };
        

    }
}
