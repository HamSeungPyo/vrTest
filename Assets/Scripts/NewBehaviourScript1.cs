using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public GameObject lookAt;
    void Update ()
    {
        transform.LookAt(lookAt.transform);
	}
}
