using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cameraFollower : MonoBehaviour
{

    private Vector3 cameraDisplacement;

    public float cameraScreenCorrection;


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GameManager.GM.getCameraManager().getCurrentCamera().transform);

        cameraDisplacement = transform.position - GameManager.GM.getCameraManager().getCurrentCamera().transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = GameManager.GM.getCameraManager().getCurrentCamera().transform.position + cameraDisplacement + cameraScreenCorrection*(-1*transform.up*Screen.height/2 + -1*transform.right*Screen.width/2);
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right*5);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up*5);
    }
}
