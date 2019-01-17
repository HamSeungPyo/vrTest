using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimControl : MonoBehaviour
{
    public Animator ani;
    public GameObject buttonModel;
    public GameObject follow;
    Vector3 controllerPos;
    GameObject controller;
    float buttonPushing = 0;
    float buttonPush = 0.015f;
    bool bButtonStart = false;
    void Update()
    {
        if (controller != null)
        {
            follow.transform.position = controller.transform.position;
            buttonPushing = follow.transform.localPosition.z - controllerPos.z;
            if (buttonModel.transform.localPosition.z >= 0.01f)
            {
                bButtonStart = true;
            }
        }
        else
        {
            if (buttonPushing > 0)
            {
                buttonPushing -= Time.deltaTime;
            }
            else
            {
                buttonPushing = 0;
            }
        }
        buttonModel.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(buttonPushing, 0, buttonPush));

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Controller")
        {
            if (controller == null)
            {
                controller = col.gameObject;
                follow.transform.position = controller.transform.position;
                controllerPos = follow.transform.localPosition;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Controller")
        {
            if (controller == col.gameObject)
            {
                controller = null;
                controllerPos = Vector3.zero;

                if (bButtonStart)
                {
                    if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Anim"))
                    {
                        ani.SetTrigger("Start");
                    }
                }
            }
        }
    }
}
