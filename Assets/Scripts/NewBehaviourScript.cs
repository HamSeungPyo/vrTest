using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool uiButtonChack = true;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("UI")&& uiButtonChack)
        {
            uiButtonChack = false;
            col.GetComponent<RayButtonControl>().touchEvent.Invoke();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            uiButtonChack = true;
        }
    }
}
