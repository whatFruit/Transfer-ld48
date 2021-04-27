using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _GM;

    public static GameManager GM {
        get{
            return _GM;
        }
    }

    private cameraManager camManager;

    public List<GameObject> pipeCaps;

    public List<spawner> spawners;

    public List<Material> changeMaterials;

    private int levelCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if(_GM != null && _GM != this){
            Destroy(this.gameObject);
        }else{
            _GM = this;
            camManager = gameObject.AddComponent<cameraManager>();
            Screen.SetResolution(1920,1080,FullScreenMode.Windowed);
        }
    }

    void Start() {
        newLevel();
    }

    private void OnDestroy() {
        Destroy(camManager);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void newLevel(){
        Debug.Log("new level");
        foreach (var cap in pipeCaps)
        {
            cap.SetActive(false);
        }
        pipeCaps[Random.Range(0,pipeCaps.Count)].SetActive(true);
        foreach(var spawner in spawners){
            spawner.spawn();
        }
        foreach (var mat in changeMaterials)
        {
            mat.SetInt("_Intensity", levelCount);
            if(mat.name == "enemy"){
                mat.SetColor("_Color",Color.Lerp(Color.black,Color.red,(1f/8f)*levelCount));
            }else{
                mat.SetColor("_Color",Color.Lerp(Color.white,Color.red,(1f/8f)*levelCount));
            }
        }



        levelCount++;
    }

    public void reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        levelCount = 0;
    }

    public cameraManager getCameraManager(){
        return camManager;
    }

    //MUST be called before starting new scene.
    public void resetSceneObjects(){
        Destroy(camManager);

        camManager = gameObject.AddComponent<cameraManager>();
    }
}
