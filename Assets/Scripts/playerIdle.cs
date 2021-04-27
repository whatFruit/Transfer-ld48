using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(playerController))]
public class playerIdle : MonoBehaviour
{

    public float playerSpeed;
    public float lookSpeed;
    private playerController mainController;

    public Renderer playerRenderer;

    private ManagedCamera playerCamera;

    private Quaternion baseCameraRotation;

    private Rigidbody playerRB;

    private float walkSignal = 0f;

    private float lookXSignal = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mainController = GetComponent<playerController>();
        playerRB = GetComponent<Rigidbody>();
        playerRenderer = GetComponentInChildren<Renderer>();
        playerRenderer.material.SetColor("_Color",Random.ColorHSV());
        playerCamera = GetComponentInChildren<ManagedCamera>();
        baseCameraRotation = playerCamera.transform.rotation;
        InvokeRepeating("changeDirection",0f,2f);
    }

    // Update is called once per frame
    void Update()
    {
    }


    void FixedUpdate() {

        if(!mainController.enabled){
            //transform.LookAt(GameManager.GM.getCameraManager().getCurrentCamera().transform);

            Vector3 walkVelocity = playerSpeed*(transform.forward * walkSignal).normalized; 

            Vector3 turnVelocity = lookSpeed*playerRB.mass*(transform.up * lookXSignal);

            playerRB.AddTorque(turnVelocity, ForceMode.Force);

            //playerRB.AddForce(walkVelocity,ForceMode.Force);
            transform.position += walkVelocity;

            playerCamera.transform.rotation = baseCameraRotation * playerRB.rotation;

        }

    }

    void changeDirection(){
        

        float walkRoll = Random.Range(0,1f);
        float turnRoll = Random.Range(0,1f);

        if(walkRoll < 0.7f){
            walkSignal = 1;
        }else{
            walkSignal = 0;
        }

        if(turnRoll < 0.2f){
            lookXSignal = 0;
        }else if(turnRoll < 0.6f){
            lookXSignal = 1f;
        }else{
            lookXSignal = -1f;
        }

    }
}
