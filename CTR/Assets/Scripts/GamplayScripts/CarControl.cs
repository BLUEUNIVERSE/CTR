using UnityEngine;
using System.Collections;
/// <summary>
/// Aniket Kayande
/// 17/12/2013
/// </summary>


[RequireComponent(typeof(Rigidbody))]
public class CarControl : CarMechanicsBase
{
 Vector3 dir = Vector3.zero;
    public InputManager m_InputManager; 
    void Start()
    {
        // I usually alter the center of mass to make the car more stable. Its less likely to flip this way.
        rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, -1.5f, rigidbody.centerOfMass.z);
    }

    void Update()
    {

	// we assume that the device is held parallel to the ground
	// and the Home button is in the right hand

	// remap the device acceleration axis to game coordinates:
	//  1) XY plane of the device is mapped onto XZ plane
	//  2) rotated 90 degrees around Y axis

    //dir = Input.acceleration;
	
    ////clamp acceleration vector to the unit sphere 
    //if (dir.sqrMagnitude > 1)
    //    dir.Normalize();

    //// Make it move 10 meters per second instead of 10 meters per frame...
    //dir *= Time.deltaTime;

    //// Move object
    //transform.Translate (dir * speed);
		float inputSteer = m_InputManager.m_fInputValueToPass;
		float inputTorque = m_InputManager.m_fThrottleValue;
        Vector3 input = new Vector3(inputSteer,inputTorque,0);
       	CarUpdate(input);
       
    }

    void OnGUI()
    {
        //string str  = "X = " + dir.x + "  Y = " + dir.y + "  Z = " + dir.z;
        //GUI.Button(new Rect(5, 5, 1024, 50),str);
    }

}
