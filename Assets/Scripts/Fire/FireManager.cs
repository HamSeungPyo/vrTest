using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public enum Firekind { Basic, Time, Water };
    public Firekind firekind;
    GameObject attackChack;
    [Header ("시간 선택시 활성")]
    public float maxScale = 3;
    public float increaseHP = 100;
    
    [Header("물 선택시 활성")]
    public float waterValueHP = 30;
    [Header("공통")]
    public float HP;
    public float[] receiveDamageValue=new float[2];
    public bool bDownHP = false;
    bool bSwitching = false;
    bool bBasic = false;
    private void Awake()
    {
        StartCoroutine(StartFire());
    }
    IEnumerator StartFire()
    {
        Vector3 scale = transform.localScale;
        float maxHP = (maxScale+ scale.y)/(HP + increaseHP);
        Vector3 fireScaleTime = scale / HP;
        Debug.Log(fireScaleTime.z);
        float HPLimit = (increaseHP + HP);
        switch ((int)firekind)
        {
            case 0:
                HP = Random.Range(100, 200);
                fireScaleTime = scale / HP;
                bBasic = true;
                break;
            case 1:
                fireScaleTime = new Vector3(maxHP, maxHP, maxHP);
                bSwitching = true;
                break;
            case 2:
                bSwitching = false;
                break;
        }
        while (true)
        {
            if (bDownHP)
            {
                HP -= (Time.deltaTime * receiveDamageValue[0]);
            }
            else
            {
                if(!bBasic)
                {
                    if (bSwitching)
                    {
                        if (HP < HPLimit)
                        {
                            HP += increaseHP * Time.deltaTime;
                        }
                    }
                }
            }
            transform.localScale = fireScaleTime * HP;

            if (attackChack == null)
            {
                bDownHP = false;
            }
            else if (attackChack.activeSelf == false)
            {
                bDownHP = false;
            }
            else
            {
                bDownHP = true;
            }
            if (HP < 0)
            {
                break;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Attack")
        {
            attackChack = col.gameObject;
            bDownHP = true;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Water")
        {
            if (!bSwitching)
            {
                HP += waterValueHP;
            }
        }
        else if (col.tag == "BottelAttack")
        {
            HP -= receiveDamageValue[1];
            col.GetComponent<SphereCollider>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Attack")
        {
            attackChack = null;
            bDownHP = false;
        }
    }
}
