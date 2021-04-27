using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public Vector3 size;

    public GameObject spawnPrefab;

    public int howManyToSpawn;

    public bool startSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if(startSpawn){
            spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn(){
        for (int i = 0; i < howManyToSpawn; i++)
        {
            Instantiate(spawnPrefab, transform.position + new Vector3(Random.Range(0,size.x),Random.Range(0,size.y),Random.Range(0,size.z)), Quaternion.identity);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + Vector3.Scale(Vector3.one,size)/2, size);
    }
}
