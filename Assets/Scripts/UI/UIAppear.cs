using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class UIAppear : MonoBehaviour
{
    public GameObject UI;
    private void Start()
    {
        UI.SetActive(false);
    }
    private void OnTriggerEnter(Collider col)
    {
        UI.SetActive(true);
    }
    private void OnTriggerExit(Collider col)
    {
        UI.SetActive(false);
    }
}
