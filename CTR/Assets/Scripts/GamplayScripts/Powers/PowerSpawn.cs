using UnityEngine;
using System.Collections;

public class PowerSpawn : MonoBehaviour {

    public GameObject power;
    private GameObject powerPrefab;
    private float tempTime = 0;
    private bool canCreatePower = false;
    public float powerSpawnDelay = 2.0f;

	// Use this for initialization
	void Start () {
        powerPrefab = (GameObject)Instantiate(power, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
        if (powerPrefab == null )
        {
            StartCoroutine("Wait");
        }
        else
        {
            StopCoroutine("Wait");
        }
	}

  

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(powerSpawnDelay);
        powerPrefab = (GameObject)Instantiate(power, transform.position, transform.rotation);
    }
                                                                                     
}
