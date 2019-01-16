using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingManager : MonoBehaviour
{
    public GameObject[] controllerFollower = new GameObject[2];
    public GameObject moveAxis;
    public GameObject headAxis;
    public PlayerCollider script_playerCollider;
    [Header("수정 가능한것")]
    public float frictionValue = 2;
    public float accelerationTime = 2;
    public float moveSpeed = 5;
    public float maxFindingRange_AxisY = 0.15f;
    public float maxFindingRange_AxisX = 0.3f;
    public float maxFindingRange__AxisZ = 0.1f;
    public float maxFindedToControllerRange = 0.4f;
    public float minAccelerationRange = 2.5f;
    public float HP = 100;
    public float receiveDamageValue = 15;

    [Header("가속도 값(참고용)")]
    public float debug_Acceleration;

    float move = 0;
    float[] saveLength = new float[2];
    float movingTime = 0;
    bool[] bMovementValueSwitching = new bool[2];
    bool bMovementFunctionSwitching = false;


    void Start ()
    {
        
    }

    void Update()
    {
        float controllerAxisX = (controllerFollower[0].transform.position.x + controllerFollower[1].transform.position.x) / 2;
        float controllerAxisY = (controllerFollower[0].transform.position.z + controllerFollower[1].transform.position.z) / 2;
        Vector3 axis = new Vector3(controllerAxisX, 0, controllerAxisY);
        Vector3 targetPos = new Vector3(axis.x, headAxis.transform.position.y, axis.z);
        headAxis.transform.LookAt(targetPos);
        moveAxis.transform.eulerAngles = headAxis.transform.eulerAngles;
        /* 좌우 흔들림 보정 만들어야함
                float headAxisAngleY = headAxis.transform.eulerAngles.y;
                float moveAxisAngleY = moveAxis.transform.eulerAngles.y;

                if (headAxisAngleY > 180)
                {
                    if (moveAxisAngleY > 180)
                    {
                    }
                    else
                    {

                    }
                }
                else
                {

                }*/

        //moveAxis.transform.eulerAngles = new Vector3(0, Mathf.Lerp(moveAxis.transform.eulerAngles.y, headAxis.transform.eulerAngles.y, Time.deltaTime), 0); // headAxis.transform.eulerAngles;

        Vector3[] controllerPos = { controllerFollower[0].transform.localPosition, controllerFollower[1].transform.localPosition };
        MovingControl(ref controllerPos);

        transform.Translate(moveAxis.transform.forward * (move * moveSpeed) * Time.deltaTime);
        if (script_playerCollider.ReceiveDamage && HP > 0)
        {
            HP -= receiveDamageValue * Time.deltaTime;
        }
        else if (HP <= 0)
        {

        }

    }
    
    void MovingControl(ref Vector3[] controllerPos)
    {
        float valueX = Mathf.Abs(controllerPos[0].x - controllerPos[1].x);
        float valueY = Mathf.Abs(controllerPos[0].y - controllerPos[1].y);
        if (!bMovementFunctionSwitching)
        {
            float valueZ = Mathf.Abs(controllerPos[0].z - controllerPos[1].z);
            if (valueX < maxFindingRange_AxisX&& valueY < maxFindingRange_AxisY&& valueZ< maxFindingRange__AxisZ)
            {
                bMovementFunctionSwitching = true;
            }
            move = 0;
            saveLength[0]= saveLength[1] = 0;
        }
        else
        {
            if (valueX > maxFindedToControllerRange)
            {
                bMovementFunctionSwitching = false;
            }
            float acceleration = AccelerationCalculator(valueY);
            debug_Acceleration = acceleration;
            if (acceleration > minAccelerationRange)
            {
                move = 1;
            }
            else
            {
                if (move >= 0.2f)
                {
                    move -= Time.deltaTime* frictionValue;
                }
                else
                {
                    move = 0;
                }
            }
            //Debug.Log(acceleration);
        }
    }
    float AccelerationCalculator(float moveDistance)
    {
        if (saveLength[0] == 0 && saveLength[1] == 0)
        {
            saveLength[1] = moveDistance;
            saveLength[0] = moveDistance;
            movingTime = 0;
            return 0;
        }
        if (saveLength[1] < moveDistance)
        {
            saveLength[1] = moveDistance;
            bMovementValueSwitching[1] = true;
        }
        else if (bMovementValueSwitching[1] && saveLength[0] != moveDistance)
        {
            saveLength[0] = moveDistance;
            movingTime = 0;
            bMovementValueSwitching[1] = false;
        }

        if (saveLength[0] > moveDistance)
        {
            saveLength[0] = moveDistance;
            bMovementValueSwitching[0] = true;
        }
        else if (bMovementValueSwitching[0] && saveLength[1] != moveDistance)
        {
            saveLength[1] = moveDistance;
            movingTime = 0;
            bMovementValueSwitching[0] = false;
        }
        movingTime += Time.deltaTime * accelerationTime;

        float movementMeanValue = Mathf.Abs(saveLength[0] - saveLength[1]);
        return (float)(movementMeanValue / movingTime);
    }
}
