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
    public CarStats m_CarStats;
    
    private float m_fNitroCoolDownTime;
    private float m_fTotalTime;
    private float m_fSaveTime;
    

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


    void NitroBoost()
    { 
        m_fCurrentEngineTorque = m_fNormalEngineTorque + m_fAdditionalNitroTorque;
        m_fSaveTime = Time.time;
        InvokeRepeating("NitroCooldown", m_CarStats.m_NitroPayload, Time.deltaTime);
    }

    void NitroCooldown()
    {
        m_fCurrentEngineTorque = Mathf.Lerp(m_fCurrentEngineTorque, m_fNormalEngineTorque, Time.deltaTime);
        m_fTotalTime += Time.deltaTime; 
        if(m_fTotalTime >= m_fNitroCoolDownTime)
        {
            CancelInvoke("NitroCooldown");
        }
    }

}
