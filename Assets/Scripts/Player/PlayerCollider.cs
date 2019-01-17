using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public GameObject gasMask;
    bool bReceiveDamage;
    GameObject chack;
    bool bGasMask = false;

    public void SasMaskWear(bool set)
    {
        bGasMask = set;
        gasMask.SetActive(bGasMask);
    }
    public bool GetGasMask()
    {
        return bGasMask;
    }
    public bool ReceiveDamage
    {
        get
        {
            if (chack == null)
            {
                bReceiveDamage = false;
            }
            return bReceiveDamage;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy_Attack"))
        {
            if (col.tag == "Attack")
            {
                chack = col.gameObject;
                bReceiveDamage = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy_Attack"))
        {
            if (col.tag == "Attack")
            {
                chack = null;
                bReceiveDamage = false;
            }
        }
    }
}
