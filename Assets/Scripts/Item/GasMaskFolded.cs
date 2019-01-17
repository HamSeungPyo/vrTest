using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskFolded : MonoBehaviour
{
    public GameObject gasMaskFolded;
    public GasMaskManager script_GasMask;

    public GameObject GetGasMask()
    {
        Destroy(gameObject);
        script_GasMask.destroy = true;
        return gasMaskFolded;
    }
}
