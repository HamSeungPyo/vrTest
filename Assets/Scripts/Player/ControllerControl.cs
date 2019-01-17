using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerControl : MonoBehaviour
{
    public GameObject gasMasItem;
    public PlayerCollider script_PlayerCollider;
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
                if (itemHolding.GetComponent<FireExtManager>())
                    itemHolding.GetComponent<FireExtManager>().GetPinState();
                else if (itemHolding.GetComponent<FireExtBottelManager>())
                    itemHolding.GetComponent<FireExtBottelManager>().ItemHolding(true);
                else if (itemHolding.GetComponent<GasMaskFoldedOpenControl>())
                {
                    GameObject obj = itemHolding;
                    var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
                    itemHolding = itemHolding.GetComponent<GasMaskFoldedOpenControl>().GetGasMask(origin.TransformVector(device.velocity), itemHolding);

                    if (itemHolding != obj)
                    {
                        itemHolding = Instantiate(itemHolding);
                        itemHolding.GetComponent<Rigidbody>().isKinematic = true;
                        if (itemHolding.GetComponent<CapsuleCollider>())
                            itemHolding.GetComponent<CapsuleCollider>().enabled = false;
                        else if (itemHolding.GetComponent<BoxCollider>())
                            itemHolding.GetComponent<BoxCollider>().enabled = false;
                        itemHolding.transform.parent = transform;
                        itemHolding.transform.localPosition = Vector3.zero;
                        itemHolding.transform.localEulerAngles = Vector3.zero;
                    }
                }
                else if (itemHolding.GetComponent<GasMaskOpen_Axis>()&& bPlayerHelmet)
                {
                    Destroy(itemHolding);
                    script_PlayerCollider.SasMaskWear(true);
                    itemHolding = null;
                    bItemToHandHolded = false;
                }
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
                    else if (itemHolding.GetComponent<GasMaskCapControl>())
                        itemHolding.GetComponent<GasMaskCapControl>().CapDestroy();
                    else if (itemHolding.GetComponent<GasMaskManager>())
                        itemHolding.GetComponent<GasMaskManager>().GasMaskReset();


                    if (itemHolding.GetComponent<CapsuleCollider>())
                        itemHolding.GetComponent<CapsuleCollider>().enabled = true;
                    else if (itemHolding.GetComponent<BoxCollider>())
                        itemHolding.GetComponent<BoxCollider>().enabled = true;

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
            else if (itemHolding.GetComponent<FireExtSprayManager>())
                itemHolding.GetComponent<FireExtSprayManager>().StartingFireExtinguisher(bTouchpadPressDown);
        }

    }
    bool uiButtonChack = true;
    bool bPlayerHelmet = false;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("UI") && uiButtonChack)
        {
            if (col.GetComponent<RayButtonControl>())
            {
                uiButtonChack = false;
                col.GetComponent<RayButtonControl>().touchEvent.Invoke();
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("PlayerHelmet")&&test)
        {
            test = false;
            bPlayerHelmet = true;
        }
    }
    bool test = true;
    private void OnTriggerStay(Collider col)
    {
        if (!bItemToHandHolded)
        {
            if (bTriggerPressDown)
            {
                bool bGasMask = false;
                if (col.gameObject.layer == LayerMask.NameToLayer("PlayerHelmet"))
                {
                    bGasMask = script_PlayerCollider.GetGasMask();
                }

                if (bGasMask)
                {                    
                    bItemToHandHolded = true;
                    itemHolding = Instantiate(gasMasItem);
                    script_PlayerCollider.SasMaskWear(false);
                    test = false;
                    itemHolding.GetComponent<Rigidbody>().isKinematic = true;

                    if (itemHolding.GetComponent<CapsuleCollider>())
                        itemHolding.GetComponent<CapsuleCollider>().enabled = false;
                    else if (itemHolding.GetComponent<BoxCollider>())
                        itemHolding.GetComponent<BoxCollider>().enabled = false;
                    itemHolding.transform.parent = transform;
                    itemHolding.transform.localPosition = Vector3.zero;
                    itemHolding.transform.localEulerAngles = Vector3.zero;
                }
                else if (col.tag == "Item")
                {
                    bItemToHandHolded = true;
                    itemHolding = col.gameObject;
                    if (itemHolding.GetComponent<GasMaskFolded>())
                    {
                        GameObject gasMask = itemHolding.GetComponent<GasMaskFolded>().GetGasMask();
                        itemHolding = Instantiate(gasMask);
                    }

                    if (itemHolding.GetComponent<FireHeadFollow>())
                    {
                        itemHolding.GetComponent<FireHeadFollow>().StartFollow(gameObject);
                    }
                    else
                    {
                        itemHolding.GetComponent<Rigidbody>().isKinematic = true;

                        if (itemHolding.GetComponent<CapsuleCollider>())
                            itemHolding.GetComponent<CapsuleCollider>().enabled = false;
                        else if (itemHolding.GetComponent<BoxCollider>())
                            itemHolding.GetComponent<BoxCollider>().enabled = false;
                        itemHolding.transform.parent = transform;
                        itemHolding.transform.localPosition = Vector3.zero;
                        itemHolding.transform.localEulerAngles = Vector3.zero;
                    }
                    if (itemHolding.GetComponent<GasMaskManager>())
                    {
                        itemHolding.GetComponent<GasMaskManager>().GetHolding();
                    }
                    else if (itemHolding.GetComponent<GasMaskCapControl>())
                    {
                        itemHolding.GetComponent<GasMaskCapControl>().GetHolding();
                    }
                }
                else if (col.tag == "OperationItem")
                {
                    if (col.GetComponent<FireExtPinControl>())
                    {
                        col.GetComponent<FireExtPinControl>().SetPinMove(transform.position);
                    }
                }

            }
            else
            {
                if (col.tag == "OperationItem")
                {
                    if (col.GetComponent<FireExtPinControl>())
                    {
                        col.GetComponent<FireExtPinControl>().ResetPinMove();
                    }
                }
            }
        }
        else
        {
            if (col.tag == "Player")
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
        if (col.gameObject.layer == LayerMask.NameToLayer("PlayerHelmet")&& !test)
        {
            bPlayerHelmet = false;
            test = true;
        }
    }
}
