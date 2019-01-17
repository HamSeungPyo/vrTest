using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtSprayManager : MonoBehaviour
{
    public GameObject particle;

    public void StartingFireExtinguisher(bool set)
    {
        particle.SetActive(set);
    }
}
