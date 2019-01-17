using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerControl : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    GameObject itemHolding;
    public GameObject hand;
    bool bTriggerPressDown = false;
    bool bTouchpadPressDown = false;
    bool bItemToHandHolded=false;
    void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	void Update ()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);


        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            bTriggerPressDown = true;
            if (itemHolding != null)
            {
                if(itemHolding.GetComponent<FireExtManager>())
                    itemHolding.GetComponent<FireExtManager>().GetPinState();
                else if(itemHolding.GetComponent<FireExtBottelManager>())
                    itemHolding.GetComponent<FireExtBottelManager>().ItemHolding(true);
            }
        }
        else
        {
            bTriggerPressDown = false;
            if (bItemToHandHolded)
            {
                bItemToHandHolded = false;
                if (itemHolding != null)
                {
                    if (itemHolding.GetComponent<FireExtManager>())
                        itemHolding.GetComponent<FireExtManager>().StartingFireExtinguisher(false);
                    else if (itemHolding.GetComponent<FireExtBottelManager>())
                        itemHolding.GetComponent<FireExtBottelManager>().ItemHolding(false);
                    else if (itemHolding.GetComponent<FireHeadFollow>())
                        itemHolding.GetComponent<FireHeadFollow>().StopFollow();

                    itemHolding.GetComponent<CapsuleCollider>().enabled = true;
                    
                    if (itemHolding.GetComponent<Rigidbody>())
                    {
                        itemHolding.GetComponent<Rigidbody>().isKinematic = false;
                        itemHolding.transform.parent = null;
                        var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
                        itemHolding.GetComponent<Rigidbody>().velocity = origin.TransformVector(device.velocity);
                        itemHolding.GetComponent<Rigidbody>().angularVelocity = origin.TransformVector(device.angularVelocity);
                        
                    }
                    itemHolding = null;
                }
            }
        }
        hand.SetActive(!bItemToHandHolded);
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            bTouchpadPressDown = true;
        }
        else
        {
            bTouchpadPressDown = false;
        }
        if (bItemToHandHolded)
        {
            if (itemHolding.GetComponent<FireExtManager>())
                itemHolding.GetComponent<FireExtManager>().StartingFireExtinguisher(bTouchpadPressDown);
        }

    }
    bool uiButtonChack = true;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("UI") && uiButtonChack)
        {
            Debug.Log("asd");
            uiButtonChack = false;
            col.GetComponent<RayButtonControl>().touchEvent.Invoke();
        }
    }
    private void OnTriggerStay(Collider coll)
    {
        if (!bItemToHandHolded)
        {
            if (bTriggerPressDown)
            {
                if (coll.tag == "Item")
                {
                    bItemToHandHolded = true;
                    itemHolding = coll.gameObject;
                    if (itemHolding.GetComponent<FireHeadFollow>())
                    {
                        itemHolding.GetComponent<FireHeadFollow>().StartFollow(gameObject);
                    }
                    else
                    {
                        itemHolding.GetComponent<Rigidbody>().isKinematic = true;
                        itemHolding.GetComponent<CapsuleCollider>().enabled = false;
                        itemHolding.transform.parent = transform;
                        itemHolding.transform.localPosition = Vector3.zero;
                        itemHolding.transform.localEulerAngles = Vector3.zero;
                    }
                }
                else if (coll.tag == "OperationItem")
                {
                    if (coll.GetComponent<FireExtPinControl>())
                    {
                        coll.GetComponent<FireExtPinControl>().SetPinMove(transform.position);
                    }
                }

            }
            else
            {
                if (coll.tag == "OperationItem")
                {
                    if (coll.GetComponent<FireExtPinControl>())
                    {
                        coll.GetComponent<FireExtPinControl>().ResetPinMove();
                    }
                }
            }
        }
        else
        {
            if (coll.tag == "Player")
            {

            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "OperationItem")
        {
            if (col.GetComponent<FireExtPinControl>())
            {
                col.GetComponent<FireExtPinControl>().ResetPinMove();
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            uiButtonChack = true;
        }
    }
}
