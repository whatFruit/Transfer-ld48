using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{

    public Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) {
        playerController playerCollision = other.gameObject.GetComponent<playerController>();
        if(playerCollision && playerCollision.isMainPlayer()){
            playerCollision.transform.position = destination.position;
            GameManager.GM.newLevel();
        }
    }
}
