using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PowerLauncher : MonoBehaviour 
{

    public List<Power> m_PowersList;
    public GameObject m_Parent;
    public Transform m_SpawnPoint;
	




    void FirePower(string powerName)
    {
        foreach (Power power in m_PowersList)
        {
            if (power.m_sName == powerName)
            {
                if(power.m_Type == PowerType.Projectile)
                    Throwable(power.gameObject);
                if (power.m_Type == PowerType.Advantage)
                {

                }
            }
        }
    }


    void Throwable(GameObject go)
    {
         GameObject instance =  Instantiate(go, m_SpawnPoint.position, m_SpawnPoint.rotation) as GameObject;
    }

    void Advantage()
    {
        
    }
}
