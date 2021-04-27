using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCamera : MonoBehaviour
{

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Overlay Awake");
        cam = GetComponent<Camera>();
        GameManager.GM.getCameraManager().registerOverlayCamera(cam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
