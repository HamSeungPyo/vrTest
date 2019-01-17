using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneBottonControl : MonoBehaviour
{
    public enum SceneButtonType
    {
         Reset, Museum, PracticePlace01, PracticePlace02, PracticePlace03, PracticePlace04, PracticePlace05, PracticePlace07, PracticePlace08
    };
    public SceneButtonType sceneButtonType;
    public GameObject buttonModel;
    public GameObject follow;
    Vector3 controllerPos;
    GameObject controller;
    float buttonPushing=0;
    float buttonPush = 0.015f;
    bool bButtonStart = false;
    void Update ()
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
                    string name = "" + sceneButtonType;
                    if (name == "Reset")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    else
                    {
                        SceneManager.LoadScene(name);
                    }
                }
            }
        }
    }
}
