using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class enemy : MonoBehaviour
{

    public float speed;

    private playerController targetPlayer = null;

    private Rigidbody enemyRB;

    private Quaternion lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponentInChildren<Rigidbody>();
        InvokeRepeating("changeDirection",0f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        
        
        enemyRB.AddForce(-1*Physics.gravity*enemyRB.mass, ForceMode.Force);

        if(targetPlayer){
            transform.LookAt(targetPlayer.transform);
        }else{
            transform.rotation = lookDirection;
        }

        transform.position += speed*transform.forward;
    }

    private void OnTriggerEnter(Collider other) {
        playerController playerCollision = other.gameObject.GetComponent<playerController>();
        if(playerCollision && playerCollision.isMainPlayer()){
            targetPlayer = playerCollision;
        }
    }

    private void OnTriggerExit(Collider other) {
        playerController playerCollision = other.gameObject.GetComponent<playerController>();
        if(playerCollision && playerCollision.isMainPlayer()){
            targetPlayer = null;
        }
    }

    void changeDirection(){
        

        lookDirection = Random.rotation;

    }
}
