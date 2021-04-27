using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class ManagedCamera : MonoBehaviour
{
    private int cameraIndex;

    private Camera cam;

    private AudioListener listener;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        listener = GetComponent<AudioListener>();
        cameraIndex = GameManager.GM.getCameraManager().registerCamera(this);
        var camData = cam.GetUniversalAdditionalCameraData();
        foreach (var cam in GameManager.GM.getCameraManager().overlayCameras)
        {
            camData.cameraStack.Add(cam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeMain(){
        GameManager.GM.getCameraManager().setCurrentCamera(cameraIndex);
    }

    public void toggle(bool toggleTo = false){
        cam.enabled = toggleTo;
        listener.enabled = toggleTo;
        Camera.SetupCurrent(cam);
    }
}
