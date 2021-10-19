using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemment : MonoBehaviour
{
    public Rigidbody rigi;
    public float speed;
    private void Awake()
    {

    }

    private void Start()
    {
        stepRayUpper.localPosition = new Vector3(stepRayUpper.localPosition.x, stepHeight, stepRayUpper.localPosition.z);
    }
    public void Locomantion(float xInput, float zInput)
    {   
        if(rigi.velocity.y<-1){return;}

        Vector3 cameraDirection = (transform.position - CameraDirection.position).normalized;
        Vector3 DirectionFoward = new Vector3(cameraDirection.x, 0, cameraDirection.z) * zInput;
        Vector3 DirectionHorizontal = new Vector3(cameraDirection.z, 0, (cameraDirection.x * -1)) * xInput;
        // Vector3 direction = (DirectionFoward + DirectionHorizontal).normalized * speed * Time.deltaTime;
        rigi.velocity = ((DirectionFoward + DirectionHorizontal) * speed) + new Vector3(0, rigi.velocity.y, 0);
        RotationPlayer(DirectionFoward, DirectionHorizontal);
        StepClimb();
    }
    private void RotationPlayer(Vector3 DirectionFoward, Vector3 DirectionHorizontal)
    {
        Vector3 angulho = (DirectionFoward + DirectionHorizontal) + transform.position;
        transform.LookAt(new Vector3(angulho.x, transform.position.y, angulho.z));
    }

    private void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                rigi.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {
            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                rigi.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                rigi.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    public void JumpPlayer()
    {   
        Vector3 directionFoward= transform.forward;
        float forceAcellaration = Mathf.Abs(rigi.velocity.z+rigi.velocity.x)+1;
        rigi.AddForce(((transform.up.normalized * 2)) * forceJump,ForceMode.Impulse);
        rigi.AddForce(directionFoward.normalized * forceJumpFoward * forceAcellaration,ForceMode.Force);
    }
    [Header("Player")]

    [SerializeField] float forceJump;
    [SerializeField] float forceJumpFoward;
    [SerializeField] Transform stepRayLower, stepRayUpper;
    [SerializeField] private float stepSmooth;
    [SerializeField] private float stepHeight = 0.126f;
    [Header("Components")]
    [SerializeField] Transform CameraDirection;

}
