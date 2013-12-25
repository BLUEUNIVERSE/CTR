using UnityEngine;
using System.Collections;

public class InGameCarUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Finish")
            Debug.Log("finish line crossed");
        else
            return;
    }
}
