using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CarScript2 : MonoBehaviour {

    public enum AllPowers
    {
       MISSILE = 1,
        OIL = 2,
        NITRO = 3,
        WALL = 4,
        EMPTY = 5
    }

    public bool[] isPassedThroughCheckpoint;
    public int lapsCompletedCount = 0;
    public GameObject finishLine;
    public int maxNumberOfLaps = 3;
    public GameObject powerPickupParticles;
    public GameObject cameraLook;


    public GameObject missile;
    public GameObject oil;
    public GameObject nitroSpark;

    public Transform frontPowerSpawnPoint;
    public Transform backPowerSpawnPoint;
    public Transform nitroSpawnPoint;


    public AllPowers currentPowerInSlot1;
    public AllPowers currentPowerInSlot2;
    public AllPowers currentPowerInSlot3;


    public UIImageButton slot1;
    public UIImageButton slot2;
    public UIImageButton slot3;

    /// UI and HUD stuff
    public UILabel time;
    public UILabel laps;

    // temp values
    private float tempTime;

    
	// Use this for initialization
	void Start () {
	    GetComponent<NewDriving>().enabled = false;
	    currentPowerInSlot1 = AllPowers.EMPTY;
	    currentPowerInSlot2 = AllPowers.EMPTY;
	    currentPowerInSlot3 = AllPowers.EMPTY;
	    laps.text = "1/" + maxNumberOfLaps.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {

        ResetLap();
        StopNitro(3);

        if (lapsCompletedCount == (maxNumberOfLaps-1) && isPassedThroughCheckpoint[1] == true)
        {
            finishLine.renderer.enabled = true;
        }

        if(Time.timeSinceLevelLoad > 5)
        {
            if (lapsCompletedCount < (maxNumberOfLaps ))
            GetComponent<NewDriving>().enabled = true;
            else
               GetComponent<NewDriving>().enabled = false;


            time.text = ((int)((Time.timeSinceLevelLoad - 5) % 60)%60).ToString() + ":" + ((int)(Time.timeSinceLevelLoad - 5) % 60).ToString();
            if (lapsCompletedCount >= (maxNumberOfLaps))
                lapsCompletedCount = (maxNumberOfLaps-1);
            laps.text = (lapsCompletedCount+1).ToString() + "/" + maxNumberOfLaps.ToString();
        }
	
	}

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Check Point")
        {
            isPassedThroughCheckpoint[Convert.ToInt32(collider.gameObject.name)] = true;
            //Debug.Log("passed through checkpoint " + Convert.ToInt32(collider.gameObject.name));
        }

        if(collider.gameObject.tag == "Finish")
        {
            if (isPassedThroughCheckpoint[0] == true && isPassedThroughCheckpoint[1] == true)
            {
               
                lapsCompletedCount++;
                
                isPassedThroughCheckpoint[0] = false;
                isPassedThroughCheckpoint[1] = false;
                //Debug.Log(lapsCompletedCount);
            }
        }

        if(collider.gameObject.tag == "Power")
        {
            if (currentPowerInSlot1 == AllPowers.EMPTY || currentPowerInSlot2 == AllPowers.EMPTY || currentPowerInSlot3 == AllPowers.EMPTY)
            {
                cameraLook = (GameObject)Instantiate(powerPickupParticles, collider.transform.position, collider.transform.rotation);
                Destroy(collider.gameObject);
                AssignPower(4);
            }
            else
            {
                return;
            }
        }
    }

    void AssignPower(int range)
    {  
        int temp = Random.Range(1, range);
        if (currentPowerInSlot1 == AllPowers.EMPTY)
        {
            currentPowerInSlot1 = (AllPowers)temp;
            slot1.normalSprite = "Shield bar BG";
            slot1.enabled = false;
            slot1.enabled = true;
            Debug.Log(currentPowerInSlot1 + " in slot 1");
        }
        else if (currentPowerInSlot2 == AllPowers.EMPTY)
        {
            currentPowerInSlot2 = (AllPowers)temp;
            slot2.normalSprite = "Shield bar BG";
            slot2.enabled = false;
            slot2.enabled = true;
            Debug.Log(currentPowerInSlot2 +" in slot 2");
        }
        else if (currentPowerInSlot3 == AllPowers.EMPTY)
        {
            currentPowerInSlot3 = (AllPowers)temp;
            slot3.normalSprite = "Shield bar BG";
            slot3.enabled = false;
            slot3.enabled = true;
            Debug.Log(currentPowerInSlot3 + " in slot 3");
        }
    }


    void OnPower1()
    { 
        Debug.Log(currentPowerInSlot1 + " power shot");
        if(currentPowerInSlot1 == AllPowers.MISSILE)
        {
            Instantiate(missile, frontPowerSpawnPoint.position, frontPowerSpawnPoint.rotation);
        }
        else if (currentPowerInSlot1 == AllPowers.OIL)
        {
            Instantiate(oil, backPowerSpawnPoint.position, backPowerSpawnPoint.rotation);
        }
        else if (currentPowerInSlot1 == AllPowers.NITRO)
        {
            nitroSpawnPoint.GetComponent<TrailRenderer>().enabled = true;
            nitroSpark.renderer.enabled = true;
            GetComponent<NewDriving>().isNitroOn = true;
            tempTime = Time.timeSinceLevelLoad;
        }
        currentPowerInSlot1 = AllPowers.EMPTY;
        slot1.normalSprite = "dim BG";
        
       
    }

    void OnPower2()
    { 
        Debug.Log(currentPowerInSlot2 + " power shot");
        if (currentPowerInSlot2 == AllPowers.MISSILE)
        {
            Instantiate(missile, frontPowerSpawnPoint.position, frontPowerSpawnPoint.rotation);
        }
        else if (currentPowerInSlot2 == AllPowers.OIL)
        {
            Instantiate(oil, backPowerSpawnPoint.position, backPowerSpawnPoint.rotation);
        }
        else if (currentPowerInSlot2 == AllPowers.NITRO)
        {
            nitroSpawnPoint.GetComponent<TrailRenderer>().enabled = true;
            nitroSpark.renderer.enabled = true;
            GetComponent<NewDriving>().isNitroOn = true;
            tempTime = Time.timeSinceLevelLoad;
        }
        currentPowerInSlot2 = AllPowers.EMPTY;
        slot2.normalSprite = "dim BG";
       
    }

    void OnPower3()
    { 
        Debug.Log(currentPowerInSlot3 + " power shot");
        if (currentPowerInSlot3 == AllPowers.MISSILE)
        {
            Instantiate(missile, frontPowerSpawnPoint.position, frontPowerSpawnPoint.rotation);
        }
        else if (currentPowerInSlot3 == AllPowers.OIL)
        {
            Instantiate(oil, backPowerSpawnPoint.position, backPowerSpawnPoint.rotation);
        }
        else if (currentPowerInSlot3 == AllPowers.NITRO)
        {
            nitroSpawnPoint.GetComponent<TrailRenderer>().enabled = true;
            nitroSpark.renderer.enabled = true;
            GetComponent<NewDriving>().isNitroOn = true;
            tempTime = Time.timeSinceLevelLoad;
        }
        currentPowerInSlot3 = AllPowers.EMPTY;
        slot3.normalSprite = "dim BG";
       
    }

    void CheckIfLapCompleted()
    {
        for (int i = 0; i < isPassedThroughCheckpoint.Length; i++)
        {
            if(isPassedThroughCheckpoint[i] == true)
            {
                lapsCompletedCount++;
            }
        }
    }

    void StopNitro(float duration)
    {
        if(Time.timeSinceLevelLoad > tempTime+duration)
        {
            nitroSpawnPoint.GetComponent<TrailRenderer>().enabled = false;
        }
    }

   

    void ResetLap()
    {
        
        //for (int i = 0; i < isPassedThroughCheckpoint.Length; i++)
        //{
        //    if (isPassedThroughCheckpoint[i] == true)
        //}
    }
 }
