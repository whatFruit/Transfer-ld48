using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class playerController : MonoBehaviour
{

    public float playerSpeed;

    public float lookSpeed;

    private float lookXAxisAngle;

    public float lookYConstraint;
    private ManagedCamera  playerCamera;

    private Quaternion baseCameraRotation;

    private Rigidbody playerRB;

    private playerController transferTarget;

    private Animator playerAnimator;

    private pipeTrigger currentPipe;

    private bool isClimbing;

    //marks the player prefab that the player should start with
    public bool isStartPlayer = false;

    private float playerMass;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<ManagedCamera>();
        playerAnimator = GetComponent<Animator>();
        baseCameraRotation = playerCamera.transform.rotation;
        if(!isStartPlayer){
            enabled = false;
        }

        playerMass = playerRB.mass;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnValidate() {
        playerCamera = GetComponentInChildren<ManagedCamera>();
    }

    void OnEnable() {
        
    }

    void FixedUpdate() {

        if(isStartPlayer){
            receiveTransfer();
        }
        

        float walkSignal = Input.GetAxis("Walk");
        float strifeSignal = Input.GetAxis("Strife");
        float useSignal = Input.GetAxis("Use");

        float lookXSignal = Input.GetAxis("MouseX");
        float lookYSignal = Input.GetAxis("MouseY");

        float transferSignal = Input.GetAxis("Transfer");

        if(transferSignal > 0){
            transfer();
        }

        if(useSignal != 0 && currentPipe != null && isClimbing == false){
            isClimbing = true;
            transform.position = currentPipe.transform.position; 
        }

        Vector3 walkVelocity;

        if(isClimbing){
            playerRB.AddForce(-1*Physics.gravity*playerRB.mass, ForceMode.Force);
            walkVelocity = playerSpeed*(-1*currentPipe.transform.forward * Mathf.Abs(walkSignal) ).normalized;
        }else{
            playerRB.mass = playerMass;
            walkVelocity = playerSpeed*(transform.forward * walkSignal + transform.right * strifeSignal).normalized;
        }

        Vector3 turnVelocity = lookSpeed*playerRB.mass*(transform.up * lookXSignal);

        lookXAxisAngle = Mathf.Clamp(lookXAxisAngle + lookSpeed*0.5f*lookYSignal,-lookYConstraint,lookYConstraint);

        playerCamera.transform.rotation = baseCameraRotation * playerRB.rotation * Quaternion.Euler(-lookXAxisAngle,0,0);

        playerRB.AddTorque(turnVelocity, ForceMode.Force);

        //playerRB.AddForce(walkVelocity,ForceMode.Force);
        transform.position += walkVelocity;
    }

    void playerDeath(){
        GameManager.GM.reset();
    }

    private void OnCollisionEnter(Collision other) {
        enemy enemyCollision = other.gameObject.GetComponent<enemy>();
        if(enemyCollision){
            playerDeath();
        }    
    }

    void OnTriggerEnter(Collider other) {

        pipeTrigger detectedPipe = other.gameObject.GetComponent<pipeTrigger>();

        if(detectedPipe){
            Debug.Log("found pipe");
            currentPipe = detectedPipe;
        }
    }


    void OnTriggerExit(Collider other) {
        pipeTrigger detectedPipe = other.gameObject.GetComponent<pipeTrigger>();

        if(detectedPipe && detectedPipe.Equals(currentPipe)){
            Debug.Log("lost pipe");
            currentPipe = null;
            isClimbing = false;
        }
    }

    public void transfer(){

        Debug.Log("transfer");

        RaycastHit transferResult;

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,out transferResult)){
            transferTarget = transferResult.collider.gameObject.GetComponent<playerController>();
            if(transferTarget){
                playerAnimator.SetTrigger("transfer");
            }
        }

    }

    public void completeTransfer(){
        playerAnimator.ResetTrigger("transfer");
        transferTarget.enabled = true;
        isStartPlayer = false;
        transferTarget.receiveTransfer();
        this.enabled = false;
        
    }

    public void receiveTransfer(){
        isStartPlayer = true;
        playerCamera.makeMain();
    }

    public bool isMainPlayer(){
        return isStartPlayer;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * 10);
    }
}
