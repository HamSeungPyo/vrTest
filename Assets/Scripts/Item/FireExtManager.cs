using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtManager : MonoBehaviour
{
    public GameObject pin;
    public GameObject particle;
    public GameObject trigger;
    bool bStartingFireExtinguisher=false;
    private void Awake()
    {
        StartCoroutine(FireExtinguisherChack());
    }

    IEnumerator FireExtinguisherChack()
    {
        while (true)
        {
            if (!pin.activeSelf)
            {
                bStartingFireExtinguisher = true;
            }

            if (bStartingFireExtinguisher)
                break;

            yield return null;
        }
    }
    public void StartingFireExtinguisher(bool set)
    {
        if (bStartingFireExtinguisher)
        {
            particle.SetActive(set);
            trigger.SetActive(set);
        }
    }
    public bool GetPinState()
    {
        return bStartingFireExtinguisher;
    }
}
