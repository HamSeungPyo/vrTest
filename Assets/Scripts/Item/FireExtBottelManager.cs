using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtBottelManager : MonoBehaviour
{
    public GameObject renderer;
    public GameObject effect;
    public float impactPower = 2;
    bool bVelocity = false;
    bool bHolding = false;

    public void ItemHolding(bool set)
    {
        bHolding = set;
    }
    private void Update()
    {
        if (bHolding)
        {
            bVelocity = false;
            return;
        }
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        float velocityPower = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.y) + Mathf.Abs(velocity.z);
        if (velocityPower > impactPower)
        {
            bVelocity = true;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ground" && bVelocity)
        {
            StartCoroutine(StartEffect());
        }
    }

    IEnumerator StartEffect()
    {
        GameObject startEffect = Instantiate(effect, transform.position, Quaternion.identity);
        renderer.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(startEffect);
        Destroy(gameObject);
    }
}
