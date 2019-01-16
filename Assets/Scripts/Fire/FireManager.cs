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
    public float waterValueScale = 3;
    [Header("공통")]
    public float HP;
    public float receiveDamageValue;
    public float damageTime;
    public bool bDownHP = false;
    private void Awake()
    {
        StartCoroutine(StartFire());
    }
    IEnumerator StartFire()
    {
        bool bSwitching=false;

        switch ((int)firekind)
        {
            case 0:
                HP = Random.Range(100, 200);
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
        Vector3 damageScale = new Vector3(1,1,1)* (scale.y / receiveDamageValue);
        Debug.Log(damageScale + "\n" + damage);
        while (true)
        {
            if (bDownHP)
            {
                HP -= (Time.deltaTime * damage) * damageTime;
                scale -= (damageScale * Time.deltaTime)* damageTime;
            }
            else
            {
                if (bSwitching)
                {
                    if (HP < HPLimit)
                    {
                        scale += fireScaleTime * Time.deltaTime;
                        HP += HPTime * Time.deltaTime;
                    }
                }
                else
                {
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
    public void SetHP(bool set)
    {
        bDownHP = set;
    }
}
