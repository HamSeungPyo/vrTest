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


        
        /*if (origin != null)
        {
            rigidbody.velocity = origin.TransformVector(device.velocity);
            rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }*/
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            bTriggerPressDown = true;
            if (itemHolding != null)
            {
                if(itemHolding.GetComponent<FireExtManager>())
                    itemHolding.GetComponent<FireExtManager>().GetPinState();
                else
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
                    else
                        itemHolding.GetComponent<FireExtBottelManager>().ItemHolding(false);

                    itemHolding.GetComponent<Rigidbody>().isKinematic = false;
                    itemHolding.GetComponent<CapsuleCollider>().enabled = true;
                    itemHolding.transform.parent = null;
                    var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
                    itemHolding.GetComponent<Rigidbody>().velocity = origin.TransformVector(device.velocity);
                    itemHolding.GetComponent<Rigidbody>().angularVelocity = origin.TransformVector(device.angularVelocity);
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
                    itemHolding.GetComponent<Rigidbody>().isKinematic = true;
                    itemHolding.GetComponent<CapsuleCollider>().enabled = false;
                    itemHolding.transform.parent = transform;
                    itemHolding.transform.localPosition = Vector3.zero;
                    itemHolding.transform.localEulerAngles = Vector3.zero;
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
    private void OnTriggerExit(Collider coll)
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
