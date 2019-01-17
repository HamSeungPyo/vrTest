using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskManager : MonoBehaviour
{
    public GameObject cap;
    public GameObject gasMaskFolded;
    bool bCapHolding = false;
    public void GetHolding()
    {
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
        if(!bCapHolding)
            cap.GetComponent<BoxCollider>().enabled = false;
    }
}
