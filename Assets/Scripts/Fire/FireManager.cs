using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public enum Firekind { Basic, Time, Water };
    public Firekind firekind;

    [Header ("시간 선택시 활성")]
    public float maxScale = 3;
    public float HPTime = 100;
    public float maxTime = 2;
    
    [Header("물 선택시 활성")]
    public float waterValueHP = 30;
    [Header("공통")]
    public float HP;
    public float receiveDamageValue;
    public float damageTime;
    public bool bDownHP = false;
    bool bSwitching = false;
    bool bBasic = false;
    private void Awake()
    {
        StartCoroutine(StartFire());
    }
    IEnumerator StartFire()
    {
        

        switch ((int)firekind)
        {
            case 0:
                HP = Random.Range(100, 200);
                bBasic = true;
                break;
            case 1:
                bSwitching = true;
                break;
            case 2:
                bSwitching = false;
                break;
        }
        Vector3 scale = transform.localScale;
        Vector3 fireScaleTime = ((new Vector3(1,1,1)* maxScale)+ scale) / maxTime;
        float HPLimit = (HPTime + HP) / maxTime;

        float damage = HP/receiveDamageValue ;
        Vector3 damageScale = scale / receiveDamageValue;
        while (true)
        {
            if (bDownHP)
            {
                HP -= (Time.deltaTime * damage) * damageTime;
                scale -= (damageScale * Time.deltaTime)* damageTime;
            }
            else
            {
                if(!bBasic)
                {
                    if (bSwitching)
                    {
                        if (HP < HPLimit)
                        {
                            scale += fireScaleTime * Time.deltaTime;
                            HP += HPTime * Time.deltaTime;
                        }
                    }
                }
            }
            transform.localScale = scale;
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
            bDownHP = true;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            if (!bSwitching)
            {
                HP += waterValueHP;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Attack")
        {
            bDownHP = false;
        }
    }
}
