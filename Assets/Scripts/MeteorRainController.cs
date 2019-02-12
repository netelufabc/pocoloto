using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRainController : MonoBehaviour {

    GameObject meteoro;

    private void Awake()
    {
        meteoro = Resources.Load("prefabs/Meteor") as GameObject;
    }

    void Start ()
    {       
        StartCoroutine( spawnwaves());
	}
	
    IEnumerator spawnwaves()
    {
        yield return new WaitForSeconds(1);
        while(true)
        {           
            Instantiate(meteoro, new Vector3(Random.Range(-13, 6), 7, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            yield return new WaitForSeconds(1);
        }
    }
}
