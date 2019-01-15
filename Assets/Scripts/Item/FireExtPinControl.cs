using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtPinControl : MonoBehaviour
{
    public GameObject pinMove;
    float pinMovePosSave;
    bool bPosSave=false;
    public void SetPinMove(Vector3 pos)
    {
       
        pinMove.transform.position = pos;
        if (!bPosSave)
        {
            bPosSave = true;
            pinMovePosSave = pinMove.transform.localPosition.z;
        }
        transform.localPosition = new Vector3(0, 0, Mathf.Clamp(pinMove.transform.localPosition.z- pinMovePosSave,-1,0));
        if (transform.localPosition.z < -0.025f)
        {
            gameObject.SetActive(false);
        }
    }
    public void ResetPinMove()
    {
        bPosSave = false;
    }
}
