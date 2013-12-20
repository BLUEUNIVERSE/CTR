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
    protected PlayerStats m_PlayerStats;

    void Start()
    {
        // I usually alter the center of mass to make the car more stable. Its less likely to flip this way.
        rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, -1.5f, rigidbody.centerOfMass.z);
    }

    void Update()
    {
        

        // Move object
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
