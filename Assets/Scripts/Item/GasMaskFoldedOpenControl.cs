using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskFoldedOpenControl : MonoBehaviour
{
    public GameObject gasMaskOpen;
    float shakeVelocty = 5.5f;
    public GameObject GetGasMask(Vector3 velo, GameObject obj)
    {
        float vel = Mathf.Abs(velo.x) + Mathf.Abs(velo.y) + Mathf.Abs(velo.z);
        if (vel < shakeVelocty)
            return obj;
        else
        {
            Destroy(gameObject);
            return gasMaskOpen;
        }
    }
}
