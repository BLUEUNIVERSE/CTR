using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {

    public UILabel countdown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.timeSinceLevelLoad < 2)
        {
            countdown.text = "3";
        }
        else if (Time.timeSinceLevelLoad < 3)
        {
            countdown.text = "2";
        }
        else if (Time.timeSinceLevelLoad < 4)
        {
            countdown.text = "1";
        }
        else if (Time.timeSinceLevelLoad < 5)
        {
            countdown.text = "GO!!";
        }
        else if (Time.timeSinceLevelLoad < 6)
        {
            countdown.text = "";
        }
	
	}

    void OnPause()
    {
        Application.LoadLevel("Main Menu Scene");
    }
}
