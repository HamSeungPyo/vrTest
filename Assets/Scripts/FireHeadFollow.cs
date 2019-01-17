using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeadFollow : MonoBehaviour
{
    public GameObject[] tube = new GameObject[2];
    public GameObject axis;
    public GameObject fireHead;
    GameObject controller;
    bool bBasic=true;
    void Update()
    {
        tube[0].SetActive(bBasic);
        tube[1].SetActive(!bBasic);
        if (controller == null)
        {
            bBasic = true;
            return;
        }
        bBasic = false;
        fireHead.transform.position = controller.transform.position;
        fireHead.transform.eulerAngles = controller.transform.eulerAngles;
    }
    public void StartFollow(GameObject con)
    {
        controller = con;
    }
    public void StopFollow()
    {
        controller = null;
    }
}
