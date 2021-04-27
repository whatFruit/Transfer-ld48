using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{

    public List<ManagedCamera> managedCameras = new List<ManagedCamera>();

    public List<Camera> overlayCameras = new List<Camera>(); //does not manage indices

    private int currentPlayerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ManagedCamera setCurrentCamera(int newCameraIndex){
        if(newCameraIndex >= managedCameras.Count){
            Debug.Log(newCameraIndex + " is out of playerCamera's range");
            return managedCameras[currentPlayerIndex];
        }

        //switch between cameras
        managedCameras[currentPlayerIndex].toggle(false);
        managedCameras[newCameraIndex].toggle(true);
        
        currentPlayerIndex = newCameraIndex;

        return managedCameras[newCameraIndex];
    }

    public int registerCamera(ManagedCamera cam){
        cam.toggle(false);
        managedCameras.Add(cam);
        return managedCameras.Count -1;
    }

    public void registerOverlayCamera(Camera cam){
        overlayCameras.Add(cam);
    }

    public ManagedCamera getCurrentCamera(){
        return managedCameras[currentPlayerIndex];
    }
}
