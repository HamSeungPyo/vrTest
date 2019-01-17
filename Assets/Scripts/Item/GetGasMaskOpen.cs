using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGasMaskOpen : MonoBehaviour
{
    public GameObject gasMaskOpen;

    public GameObject GetGasMask()
    {
        Destroy(gameObject);
        return gasMaskOpen;
    }
}
