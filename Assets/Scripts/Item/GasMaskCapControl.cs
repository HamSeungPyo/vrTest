using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskCapControl : MonoBehaviour
{
    public GasMaskManager script_GasMask;
    public void CapDestroy()
    {
        Destroy(gameObject,1);
    }
    public void GetHolding()
    {
        script_GasMask.CapHolding();
    }
}
