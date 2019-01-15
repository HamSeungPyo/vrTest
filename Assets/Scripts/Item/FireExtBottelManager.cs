using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtBottelManager : MonoBehaviour
{
    public GameObject effect;
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ground")
        {
            StartCoroutine(StartEffect());
        }
    }

    IEnumerator StartEffect()
    {
        GameObject startEffect = Instantiate(effect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(startEffect);
    }
}
