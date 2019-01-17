using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskManager : MonoBehaviour
{
    public GameObject cap;
    public GameObject gasMaskFolded;
    bool bCapHolding = false;
    bool bDestroy = false;
    public bool destroy { set { bDestroy = value; } }
    public void GetHolding()
    {
        gasMaskFolded.GetComponent<BoxCollider>().enabled = true;
        if (!bCapHolding)
            cap.GetComponent<BoxCollider>().enabled = true;
    }
    public void CapHolding()
    {
        gasMaskFolded.SetActive(true);
        bCapHolding = true;
    }
    public void GasMaskReset()
    {
        if (!bCapHolding)
        {
            cap.GetComponent<BoxCollider>().enabled = false;
        }
        else if (bDestroy)
        {
            gameObject.layer = LayerMask.NameToLayer("JunkItem");
            Destroy(gameObject, 1);
        }
        else
        {
            gasMaskFolded.GetComponent<BoxCollider>().enabled = false;
        }
        
    }
}
