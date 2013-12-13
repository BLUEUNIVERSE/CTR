using UnityEngine;
using System.Collections;
using System;


public class CarSelectMenu : MonoBehaviour {

    public int numberOfCars = 3;
    private int maxPos;
    private int minPos;
    private int currentCarNumber = 1;
    public TweenPosition statsTweener;
   // public twe
    private bool canScrollCars = true;

    public UILabel totalCoinsLabel;
    public UILabel upgradeCostLabel;

    public TweenPosition upgradeButton;
    public TweenPosition purchaseButton;

    public int[] allCarsUnlockCost;

    public int totalCoins = 1000;
    public int[] upgradeCostCar1;
    public int[] upgradeCostCar2;
    public int[] upgradeCostCar3;
    public int[] upgradeCostCar4;


    public UISlider speed;
    public UISlider acceleration;
    public UISlider handling;
    public UISlider nitro;
    public UISlider shield;

    public UISlider newSpeed;
    public UISlider newAcc;
    public UISlider newHandling;
    public UISlider newNitro;
    public UISlider newShield;

    public int[] car1Stats;
    public int[] car2Stats;
    public int[] car3Stats;
    public int[] car4Stats;

    private int carSpeed1;
    private int carAcc1;
    private int carHandling1;
    private int carNitro1;
    private int carShield1;
    private int carSpeed2;
    private int carAcc2;
    private int carHandling2;
    private int carNitro2;
    private int carShield2;

    /// for touch interactions
    float swipeDist;
    Vector2 swipeDistx;
    private Vector2 startPos;
    private float avgDistance;
    private float minSwipeDistance = 10;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.A))
          statsTweener.Play(true);
        else if(Input.GetKey(KeyCode.S))
        {
            statsTweener.Play(false);  
        }

        if (Input.touchCount > 0 && canScrollCars)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                swipeDist = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                swipeDistx = new Vector2((touch.position.x - startPos.x), 0);
                // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
                //float avgDistY = touch.position.y - startPos.y;
                if (swipeDistx.x > 0)
                {
                    if (currentCarNumber > 1)
                        currentCarNumber--;
                }
                else if (swipeDistx.x < 0)
                {
                    if (currentCarNumber < numberOfCars)
                        currentCarNumber++;
                }
            }
        }

	    totalCoinsLabel.text = "$" + totalCoins.ToString();

        if(currentCarNumber == 1)
        {
            purchaseButton.Play(true);
            upgradeButton.Play(false);

            if(car1Stats[1] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar1[0].ToString();
                carSpeed2 = car1Stats[7];   carSpeed1 = car1Stats[2];     carAcc2 = car1Stats[8];     carAcc1 = car1Stats[3];    carHandling2 = car1Stats[9]; carHandling1 = car1Stats[4];
                carNitro2 = car1Stats[10]; carNitro1 = car1Stats[5];      carShield2 = car1Stats[11]; carShield1 = car1Stats[6];
            }
            else if (car1Stats[1] == 2)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar1[1].ToString();
                carSpeed2 = car1Stats[12]; carSpeed1 = car1Stats[7];      carAcc2 = car1Stats[13]; carAcc1 = car1Stats[8];    carHandling2 = car1Stats[14]; carHandling1 = car1Stats[9];
                carNitro2 = car1Stats[15]; carNitro1 = car1Stats[10];     carShield2 = car1Stats[16]; carShield1 = car1Stats[11];
            }
        }
        else if (currentCarNumber == 2)
        {
            if (car2Stats[0] == 1)
            {
               upgradeButton.Play(false);
                purchaseButton.Play(true);
            }
            else 
            {
                carSpeed2 = car2Stats[2]; carSpeed1 = car2Stats[2]; carAcc2 = car2Stats[3]; carAcc1 = car2Stats[3]; carHandling2 = car2Stats[4]; carHandling1 = car2Stats[4];
                carNitro2 = car2Stats[5]; carNitro1 = car2Stats[5]; carShield2 = car2Stats[6]; carShield1 = car2Stats[6];
                upgradeCostLabel.text = "Unlock cost $" + allCarsUnlockCost[0].ToString();
                upgradeButton.Play(true);
                purchaseButton.Play(false);
            }

            if (car2Stats[1] == 1 && car2Stats[0] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar2[0].ToString();
                carSpeed2 = car2Stats[7]; carSpeed1 = car2Stats[2];  carAcc2 = car2Stats[8]; carAcc1 = car2Stats[3];  carHandling2 = car2Stats[9]; carHandling1 = car2Stats[4];
                carNitro2 = car2Stats[10]; carNitro1 = car2Stats[5];  carShield2 = car2Stats[11]; carShield1 = car2Stats[6];
            }
            else if (car2Stats[1] == 2 && car2Stats[0] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar2[1].ToString();
                carSpeed2 = car2Stats[12]; carSpeed1 = car2Stats[7];  carAcc2 = car2Stats[13]; carAcc1 = car2Stats[8];   carHandling2 = car2Stats[14]; carHandling1 = car2Stats[9];
                carNitro2 = car2Stats[15]; carNitro1 = car2Stats[10];  carShield2 = car2Stats[16]; carShield1 = car2Stats[11];
            }
        }

        else if (currentCarNumber == 3)
        {
            if (car3Stats[0] == 1)
            {
                upgradeButton.Play(false);
                purchaseButton.Play(true);
            }
            else
            {
                carSpeed2 = car3Stats[2]; carSpeed1 = car3Stats[2]; carAcc2 = car3Stats[3]; carAcc1 = car2Stats[3]; carHandling2 = car3Stats[4]; carHandling1 = car3Stats[4];
                carNitro2 = car3Stats[5]; carNitro1 = car3Stats[5]; carShield2 = car3Stats[6]; carShield1 = car3Stats[6];
                upgradeCostLabel.text = "Unlock cost $" + allCarsUnlockCost[1].ToString();
                upgradeButton.Play(true);
                purchaseButton.Play(false);
            }

            if (car3Stats[1] == 1 && car3Stats[0] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar3[0].ToString();
                carSpeed2 = car3Stats[7]; carSpeed1 = car3Stats[2];     carAcc2 = car3Stats[8]; carAcc1 = car3Stats[3];   carHandling2 = car3Stats[9]; carHandling1 = car3Stats[4];
                carNitro2 = car3Stats[10]; carNitro1 = car3Stats[5];    carShield2 = car3Stats[11]; carShield1 = car3Stats[6];
            }
            else if (car3Stats[1] == 2 && car3Stats[0] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar3[1].ToString();
                carSpeed2 = car3Stats[12]; carSpeed1 = car3Stats[7];    carAcc2 = car3Stats[13]; carAcc1 = car3Stats[8];   carHandling2 = car3Stats[14]; carHandling1 = car3Stats[9];
                carNitro2 = car3Stats[15]; carNitro1 = car3Stats[10];    carShield2 = car3Stats[16]; carShield1 = car3Stats[11];
            }
        }
        else if (currentCarNumber == 4)
        {
            if (car4Stats[0] == 1)
            {
                upgradeButton.Play(false);
                purchaseButton.Play(true);
            }
            else
            {
                carSpeed2 = car4Stats[2]; carSpeed1 = car4Stats[2]; carAcc2 = car4Stats[3]; carAcc1 = car4Stats[3]; carHandling2 = car4Stats[4]; carHandling1 = car4Stats[4];
                carNitro2 = car4Stats[5]; carNitro1 = car4Stats[5]; carShield2 = car4Stats[6]; carShield1 = car4Stats[6];
                upgradeCostLabel.text = "Unlock cost $" + allCarsUnlockCost[1].ToString();
                upgradeButton.Play(true);
                purchaseButton.Play(false);
            }

            if (car4Stats[1] == 1 && car4Stats[0] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar3[0].ToString();
                carSpeed2 = car4Stats[7]; carSpeed1 = car4Stats[2]; carAcc2 = car4Stats[8]; carAcc1 = car4Stats[3]; carHandling2 = car4Stats[9]; carHandling1 = car4Stats[4];
                carNitro2 = car4Stats[10]; carNitro1 = car4Stats[5]; carShield2 = car4Stats[11]; carShield1 = car4Stats[6];
            }
            else if (car4Stats[1] == 2 && car4Stats[0] == 1)
            {
                upgradeCostLabel.text = "Cost $" + upgradeCostCar3[1].ToString();
                carSpeed2 = car4Stats[12]; carSpeed1 = car4Stats[7]; carAcc2 = car4Stats[13]; carAcc1 = car4Stats[8]; carHandling2 = car4Stats[14]; carHandling1 = car4Stats[9];
                carNitro2 = car4Stats[15]; carNitro1 = car4Stats[10]; carShield2 = car4Stats[16]; carShield1 = car4Stats[11];
            }
        }

         for (int i = 1; i <= numberOfCars; i++)
         {
             if (currentCarNumber == i)
                 transform.position = Vector3.Lerp(transform.position, new Vector3((i - 1) * 10, transform.position.y, transform.position.z), 5.0f * Time.deltaTime);
         }

        //if(Input.GetKey(KeyCode.Escape))
        //    Application.LoadLevel("main menu");
         newSpeed.foreground.localScale = new Vector3(carSpeed2, newSpeed.foreground.localScale.y, newSpeed.foreground.localScale.z);
         speed.foreground.localScale = new Vector3(carSpeed1, speed.foreground.localScale.y, speed.foreground.localScale.z);

         newAcc.foreground.localScale = new Vector3(carAcc2, newAcc.foreground.localScale.y, newAcc.foreground.localScale.z);
         acceleration.foreground.localScale = new Vector3(carAcc1, acceleration.foreground.localScale.y, acceleration.foreground.localScale.z);

         newHandling.foreground.localScale = new Vector3(carHandling2, newHandling.foreground.localScale.y, newHandling.foreground.localScale.z);
         handling.foreground.localScale = new Vector3(carHandling1, handling.foreground.localScale.y, handling.foreground.localScale.z);

         newNitro.foreground.localScale = new Vector3(carNitro2, newNitro.foreground.localScale.y, newNitro.foreground.localScale.z);
         nitro.foreground.localScale = new Vector3(carNitro1, nitro.foreground.localScale.y, nitro.foreground.localScale.z);

         newShield.foreground.localScale = new Vector3(carShield2, newShield.foreground.localScale.y, newShield.foreground.localScale.z);
         shield.foreground.localScale = new Vector3(carShield1, shield.foreground.localScale.y, shield.foreground.localScale.z);

	    if (Application.platform == RuntimePlatform.WindowsEditor && canScrollCars)
                {
            if(Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (currentCarNumber < numberOfCars)
                    currentCarNumber++;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (currentCarNumber > 1)
                    currentCarNumber--;
            }
        }
	}

    void OnUpgrade()
    {
        if (currentCarNumber == 1 && car1Stats[1] == 1 && totalCoins >= upgradeCostCar1[0])
        {
            car1Stats[1] = 2;
            totalCoins -= upgradeCostCar1[0];
        }
        else if (currentCarNumber == 2 && car2Stats[1] == 1 && totalCoins >= upgradeCostCar2[0])
        {
            car2Stats[1] = 2;
            totalCoins -= upgradeCostCar2[0];
        }
        else if (currentCarNumber == 3 && car3Stats[1] == 1 && totalCoins >= upgradeCostCar3[0])
        {
            car3Stats[1] = 2;
            totalCoins -= upgradeCostCar3[0];
        }
        else if (currentCarNumber == 4 && car4Stats[1] == 1 && totalCoins >= upgradeCostCar4[0])
        {
            car4Stats[1] = 2;
            totalCoins -= upgradeCostCar4[0];
        }
    }

    void OnPurchase()
    {
         if (currentCarNumber == 2 && car2Stats[0] != 1 && totalCoins >= allCarsUnlockCost[0])
        {
            car2Stats[0] = 1;
            totalCoins -= allCarsUnlockCost[0];
            upgradeCostLabel.text = "Cost $" + upgradeCostCar2[0].ToString();
             upgradeButton.Play(false);
             purchaseButton.Play(true);

        }
         else if (currentCarNumber == 3 && car3Stats[0] != 1 && totalCoins >= allCarsUnlockCost[1] && car2Stats[0] == 1)
         {
             car3Stats[0] = 1;
             totalCoins -= allCarsUnlockCost[1];
             upgradeCostLabel.text = "Cost $" + upgradeCostCar3[0].ToString();
             upgradeButton.Play(false);
             purchaseButton.Play(true);

         }
         else if (currentCarNumber == 4 && car4Stats[0] != 1 && totalCoins >= allCarsUnlockCost[1] && car3Stats[0] == 1)
         {
             car4Stats[0] = 1;
             totalCoins -= allCarsUnlockCost[2];
             upgradeCostLabel.text = "Cost $" + upgradeCostCar4[0].ToString();
             upgradeButton.Play(false);
             purchaseButton.Play(true);

         }
    }

    void OnCarsSelect()
    {
        statsTweener.Play(false);
        canScrollCars = true;
    }

    void OnRace()
    {
        Application.LoadLevel("TestScene");
    }

    void OnMenu()
    {
        Application.LoadLevel("Main Menu Scene");
    }

    void OnSelectLevel()
    {
        Application.LoadLevel("Select Level Menu");
    }

    //}
}
