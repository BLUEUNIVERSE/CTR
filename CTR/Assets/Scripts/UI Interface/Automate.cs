using UnityEngine;
using System.Collections;

public class Automate : MonoBehaviour {

    public bool isSplashScreen = false;
    public bool AutoRotate = false;
    public float roationSpeed = 10f;
    public bool AutoDestruct = false;
    public float destroyTime;

    public bool mainMenuRotate = false;

    public float startAngle = 0.0f;
    public float finishAngle = 0.0f;
    public float speedOfCameraRotation = 0.4f;
    private int time1 = 5;
    private int time2 = 10;
    private float startAngleZ = 0.0f;
    private float finishAngleZ = 180.0f;
    private int tempTime = 0;
    public TweenPosition mainCameraMove;
	// Use this for initialization
	void Start () {

        startAngle = Random.Range(0.0f, 180.0f) + 20.0f;
        finishAngle = Random.Range(180.0f, 360.0f);
	
	}
	
	// Update is called once per frame
	void Update () {

        if (AutoRotate)
        {
            gameObject.transform.Rotate(new Vector3(0.0f, roationSpeed * Time.deltaTime, 0.0f));
        }

        if (isSplashScreen)
        {
            if(Time.timeSinceLevelLoad > 7)
            Application.LoadLevel("Main Menu Scene");
        }

        if (AutoDestruct)
        {
            Destroy(gameObject, destroyTime);
        }

        //if (mainMenuRotate)
        //{
        //    if (Time.timeSinceLevelLoad > tempTime)
        //    {


                
        //        mainCameraMove.GetComponent<TweenPosition>().from = new Vector3(Random.Range(-50, 50), Random.Range(-9, 30), Random.Range(0, 50));
        //        mainCameraMove.GetComponent<TweenPosition>().to = new Vector3(Random.Range(-100, 100), Random.Range(-9, 30), Random.Range(0, 50));
               
        //        mainCameraMove.Play(true);
        //        tempTime += 5;
        //    }
            //else
            //{
            //    float angle = Mathf.LerpAngle(startAngle, finishAngle, Time.time * speedOfCameraRotation);
            //    //float anglez = Mathf.LerpAngle(finishAngleZ, startAngleZ, Time.time * speedOfCameraRotation);
            //    transform.eulerAngles = new Vector3(0, angle, 0);
            //    // Debug.Log("moving");
            //}
        //}
	
	}
}
