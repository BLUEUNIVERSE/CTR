using UnityEngine;
using System.Collections;

public class NewCarSelectMenu : MonoBehaviour {

    // to move camera to next car
    public UILabel carLevel;
    public int numberOfCars = 3;
    private int maxPos;
    private int minPos;
    private int currentCarNumber = 1;
    public GameObject spawnParticles;
    public Transform spawnParticlesPoint;

    // info about coins and costs for cars
    public UILabel totalCoinsLabel;
    public UILabel upgradeCostLabel;
    public int totalCoins = 1000;
    public int maxLevelForCars = 3;

    // array of cars
    public GameObject[] cars;
    //public GameObject tempCar;
    public CarProperties tempCarProperties;
    int oldvalue = 0;
    int newValue = 0;


    // graphics stats for cars
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

    // upgrade and purchase buttons
    public TweenPosition upgradeButton;
    public TweenPosition unlockButton;
    public TweenPosition raceButton;
    public TweenPosition stickerButtons;
    public TweenPosition purchaseSticker2;
    public Light sun;
    public TweenPosition statsTween;


    /// for touch interactions
    Vector2 swipeDist;
    Vector2 swipeDistx;
    private Vector2 startPos;
    private float avgDistance;
    private float minSwipeDistance = 10;

    // money stats
    public UILabel coins;


    // Perks stuff
    public TweenPosition perkTween;
    private bool isSelectingPerk = false;
    public UIImageButton perk1;
    public UIImageButton perk2;
    public UIImageButton perk3;
    private int currentPerkSlotNumber;
    public int slot1Perk;
    public int slot2Perk;
    public int slot3Perk;


    // camera stufff
    public bool isCameraOn = false;
    public TweenPosition perkButtonTween;


    // power slots
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;



	// Use this for initialization
	void Start () {

        totalCoinsLabel.text = totalCoins.ToString();
        //for (int i = 1; i <= numberOfCars; i++)
        //{
        //    int temp = i - 1;
        //    cars[temp].renderer.material.mainTexture = cars[temp].GetComponent<CarProperties>().stickers[0];
        //}
	
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (isCameraOn == true)
                {
                    cars[currentCarNumber - 1].GetComponent<Automate>().enabled = false;
                    Vector2 tempSwipeDist = new Vector2((Input.mousePosition.x - startPos.x), 0);
                    
                        if (tempSwipeDist.x > 100)
                        {
                            tempSwipeDist.x = 100;
                        }
                        else if (tempSwipeDist.x < -100)
                        {
                            tempSwipeDist.x = -100;
                        }

                    cars[currentCarNumber - 1].transform.Rotate(new Vector3(0.0f, -tempSwipeDist.x * Time.deltaTime, 0.0f));
                }

            }

            if (touch.phase == TouchPhase.Ended)
            {
                //swipeDist = (new Vector2(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                //swipeDist = new Vector2(0, (touch.position.y - startPos.y));
                swipeDistx = new Vector2(0, (touch.position.x - startPos.x));
                // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
                //float avgDistY = touch.position.y - startPos.y;

                if (isSelectingPerk == false && isCameraOn == false)
                {
                    if (swipeDistx.x > (Screen.height / 5))
                    {
                        if (currentCarNumber < numberOfCars)
                            currentCarNumber++;
                    }
                    else if (swipeDistx.x < -(Screen.height / 5))
                    {
                        if (currentCarNumber > 1)
                        {
                            //spawnParticlesPrefab = (GameObject)Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
                            currentCarNumber--;
                        }
                    }
                }
            }
        }

        MoveCamera();
        for (int i = 1; i <= numberOfCars; i++)
        {
            if (currentCarNumber == i)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3((i - 1) * 10, transform.position.y, transform.position.z), 5.0f * Time.deltaTime);
                
                int temp = i - 1;
                tempCarProperties = cars[temp].GetComponent<CarProperties>();
                carLevel.text = "Car Level " + tempCarProperties.carLevel;

                CheckCarPerk(temp);
                
                if(tempCarProperties.carLevel == 0)
                {
                    oldvalue = 0;
                    newValue = 0;

                    upgradeButton.Play(true);
                    unlockButton.Play(false);
                    raceButton.Play(true);
                    purchaseSticker2.Play(false);
                    stickerButtons.Play(true);

                    slot1.localPosition = new Vector3(slot1.localPosition.x, slot1.localPosition.y, 2000);
                    slot2.localPosition = new Vector3(slot2.localPosition.x, slot2.localPosition.y, 2000);
                    slot3.localPosition = new Vector3(slot3.localPosition.x, slot3.localPosition.y, 2000);

                    sun.intensity = 0.2f;
                    upgradeCostLabel.text = "Unlock $" + cars[temp].GetComponent<CarProperties>().coinsToUnlock.ToString();
                }
                else if (tempCarProperties.carLevel == 1)
                {
                    oldvalue = 0;
                    newValue = 1;
                    upgradeButton.Play(false);
                    unlockButton.Play(true);
                    if (isCameraOn == true)
                    {
                        raceButton.Play(true);
                        stickerButtons.Play(true);
                    }
                    else
                    {
                        raceButton.Play(false);
                        stickerButtons.Play(false);
                    }
                    
                    sun.intensity = 8.0f;

                    slot1.localPosition = new Vector3(slot1.localPosition.x, slot1.localPosition.y, 0);
                    slot2.localPosition = new Vector3(slot2.localPosition.x, slot2.localPosition.y, 2000);
                    slot3.localPosition = new Vector3(slot3.localPosition.x, slot3.localPosition.y, 2000);
                }
                else if (tempCarProperties.carLevel == 2)
                {
                    oldvalue = 1;
                    newValue = 2;
                    if (isCameraOn == true)
                    {
                        raceButton.Play(true);
                        stickerButtons.Play(true);
                    }
                    else
                    {
                        raceButton.Play(false);
                        stickerButtons.Play(false);
                    }

                    slot1.localPosition = new Vector3(slot1.localPosition.x, slot1.localPosition.y, 0);
                    slot2.localPosition = new Vector3(slot2.localPosition.x, slot2.localPosition.y, 0);
                    slot3.localPosition = new Vector3(slot3.localPosition.x, slot3.localPosition.y, 2000);

                    upgradeButton.Play(false);
                    sun.intensity = 8.0f;
                }
                else if (tempCarProperties.carLevel == 3)
                {
                    oldvalue = 2;
                    newValue = 2;
                    upgradeButton.Play(true);
                    unlockButton.Play(true);
                    if (isCameraOn == true)
                    {
                        raceButton.Play(true);
                        stickerButtons.Play(true);
                    }
                    else
                    {
                        raceButton.Play(false);
                        stickerButtons.Play(false);
                    }

                    slot1.localPosition = new Vector3(slot1.localPosition.x, slot1.localPosition.y, 0);
                    slot2.localPosition = new Vector3(slot2.localPosition.x, slot2.localPosition.y, 0);
                    slot3.localPosition = new Vector3(slot3.localPosition.x, slot3.localPosition.y, 0);
                    
                    sun.intensity = 8.0f;
                    upgradeCostLabel.text = "MAX LEVEL";
                }

                newSpeed.foreground.localScale = new Vector3(tempCarProperties.speedLevels[newValue], newSpeed.foreground.localScale.y, newSpeed.foreground.localScale.z);
                speed.foreground.localScale = new Vector3(tempCarProperties.speedLevels[oldvalue], speed.foreground.localScale.y, speed.foreground.localScale.z);

                newAcc.foreground.localScale = new Vector3(tempCarProperties.accelerationLevels[newValue], newAcc.foreground.localScale.y, newAcc.foreground.localScale.z);
                acceleration.foreground.localScale = new Vector3(tempCarProperties.accelerationLevels[oldvalue], acceleration.foreground.localScale.y, acceleration.foreground.localScale.z);

                newHandling.foreground.localScale = new Vector3(tempCarProperties.handlingLevels[newValue], newHandling.foreground.localScale.y, newHandling.foreground.localScale.z);
                handling.foreground.localScale = new Vector3(tempCarProperties.handlingLevels[oldvalue], handling.foreground.localScale.y, handling.foreground.localScale.z);

                newShield.foreground.localScale = new Vector3(tempCarProperties.ShieldLevels[newValue], newShield.foreground.localScale.y, newShield.foreground.localScale.z);
                shield.foreground.localScale = new Vector3(tempCarProperties.ShieldLevels[oldvalue], shield.foreground.localScale.y, shield.foreground.localScale.z);
            }
        }
	}

    void OnUpgrade()
    {
        if (tempCarProperties.carLevel == 1 && totalCoins >= tempCarProperties.coinsToUpgradeLevel2)
            {
                tempCarProperties.carLevel++;
                totalCoins -= tempCarProperties.coinsToUpgradeLevel2;
                totalCoinsLabel.text = totalCoins.ToString();
                Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
            }
        else if (tempCarProperties.carLevel == 2 && totalCoins >= tempCarProperties.coinsToUpgradeLevel3)
            {
                tempCarProperties.carLevel++;
                totalCoins -= tempCarProperties.coinsToUpgradeLevel3;
                totalCoinsLabel.text = totalCoins.ToString();
                Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
            }
       // Debug.Log(tempCarProperties.carLevel);
    }

    void OnPurchase()
    {
        if (totalCoins >= tempCarProperties.coinsToUnlock)
        {
            Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
            tempCarProperties.carLevel++;
            totalCoins -= tempCarProperties.coinsToUnlock;
            totalCoinsLabel.text = totalCoins.ToString();
            //cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker = cars[currentCarNumber - 1].GetComponent<CarProperties>().stickers[0];
            //cars[currentCarNumber - 1].renderer.material.mainTexture = cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker;
        }
            //Debug.Log(cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel);
       
        
    }

    void OnRace()
    {

        //if (tempCarProperties.sticker2Unlocked == false)
        //    tempCarProperties.currentSticker = cars[currentCarNumber - 1].GetComponent<CarProperties>().stickers[0];

        if (tempCarProperties.carLevel > 0)
            Application.LoadLevel("In game UI");
    }

    void OnPerk1()
    {
        currentPerkSlotNumber = 1;
        isSelectingPerk = true;
        perkTween.Play(true);
    }
    void OnPerk2()
    {
        currentPerkSlotNumber = 2;
        isSelectingPerk = true;
        perkTween.Play(true);
    }
    void OnPerk3()
    {
        currentPerkSlotNumber = 3;
        isSelectingPerk = true;
        perkTween.Play(true);
    }

    void OnPerkExit()
    {
        isSelectingPerk = false;
        perkTween.Play(false);
    }

    void OnEnergyPerk()
    {
        if (currentPerkSlotNumber == 1)
        {
            slot1Perk = 1;
            perk1.normalSprite = "exit button";
            perk1.hoverSprite = "exit button";
            perk1.pressedSprite = "exit button";
            tempCarProperties.perk1 = 1;
            //perk1.enabled = false;
            //perk1.enabled = true;

            print("slot 1 perk = energy");
        }
        else if (currentPerkSlotNumber == 2)
        {
            slot2Perk = 1;
            perk2.normalSprite = "exit button";
            perk2.hoverSprite = "exit button";
            perk2.pressedSprite = "exit button";
            tempCarProperties.perk2 = 1;

            print("slot 2 perk = energy");
        }
        else if (currentPerkSlotNumber == 3)
        {
            slot3Perk = 1;
            perk3.normalSprite = "exit button";
            perk3.hoverSprite = "exit button";
            perk3.pressedSprite = "exit button";

            tempCarProperties.perk3 = 1;
            print("slot 3 perk = energy");
        }

    }

    void OnPerkTime()
    {
        if (currentPerkSlotNumber == 1)
        {
            slot1Perk = 1;
            perk1.normalSprite = "exit button";
            perk1.hoverSprite = "exit button";
            perk1.pressedSprite = "exit button";
            tempCarProperties.perk1 = 2;
            //perk1.enabled = false;
            //perk1.enabled = true;

            print("slot 1 perk = energy");
        }
        else if (currentPerkSlotNumber == 2)
        {
            slot2Perk = 1;
            perk2.normalSprite = "exit button";
            perk2.hoverSprite = "exit button";
            perk2.pressedSprite = "exit button";
            tempCarProperties.perk2 = 2;

            print("slot 2 perk = energy");
        }
        else if (currentPerkSlotNumber == 3)
        {
            slot3Perk = 1;
            perk3.normalSprite = "exit button";
            perk3.hoverSprite = "exit button";
            perk3.pressedSprite = "exit button";

            tempCarProperties.perk3 = 2;
            print("slot 3 perk = energy");
        }
    }

    void CheckCarPerk(int carNumber)
    {
        if (cars[carNumber].GetComponent<CarProperties>().perk1 == 0)
        {
            perk1.normalSprite = "profile BG light";
            perk1.hoverSprite = "profile BG light";
            perk1.pressedSprite = "profile BG light";
            perk1.enabled = false;
            perk1.enabled = true;
        }
        else if (cars[carNumber].GetComponent<CarProperties>().perk1 == 1)
        {
            perk1.normalSprite = "dim BG";
            perk1.hoverSprite = "dim BG";
            perk1.pressedSprite = "dim BG";
            perk1.enabled = false;
            perk1.enabled = true;
        }
        else if (cars[carNumber].GetComponent<CarProperties>().perk1 == 2)
        {
            perk1.normalSprite = "exit button";
            perk1.hoverSprite = "exit button";
            perk1.pressedSprite = "exit button";
            perk1.enabled = false;
            perk1.enabled = true;
        }
    }

    void OnCamera()
    {
        if (isCameraOn == false)
        {
            statsTween.Play(true);
            isCameraOn = true;
        }
        else if (isCameraOn == true)
        {
            statsTween.Play(false);
            isCameraOn = false;
        }
    }


    void MoveCamera()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            
            startPos = Input.mousePosition;
            
        }
        if (Input.GetButton("Fire1"))
        {
            if (isCameraOn == true)
            {
                cars[currentCarNumber - 1].GetComponent<Automate>().enabled = false;
                Vector2 tempSwipeDist = new Vector2((Input.mousePosition.x - startPos.x), 0);

                if (tempSwipeDist.x > 100)
                {
                    tempSwipeDist.x = 100;
                }
                else if (tempSwipeDist.x < -100)
                {
                    tempSwipeDist.x = -100;
                }

                cars[currentCarNumber - 1].transform.Rotate(new Vector3(0.0f, -tempSwipeDist.x * Time.deltaTime, 0.0f));
            }
            
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cars[currentCarNumber - 1].GetComponent<Automate>().enabled = true;
            //swipeDist = (new Vector2(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
            swipeDist = new Vector2( (Input.mousePosition.x - startPos.x), 0);
            //swipeDistx = new Vector2((touch.position.x - startPos.x), 0);
            // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
            //Debug.Log(swipeDist.x);
           //cars[currentCarNumber-1].transform.Rotate(new Vector3(0.0f, swipeDist.x * Time.deltaTime, 0.0f));
            //float avgDistY = touch.position.y - startPos.y;
            if (swipeDist.x < -50)
            {
                if (isSelectingPerk == false && isCameraOn == false)
                {
                    if (currentCarNumber < numberOfCars)
                    {
                        //spawnParticlesPrefab = (GameObject)Instantiate(spawnParticles, cars[currentCarNumber].transform.position, cars[currentCarNumber].transform.rotation);
                        currentCarNumber++;
                    }
                }
            }
            else if (swipeDist.x > 50)
            {
                if (isSelectingPerk == false && isCameraOn == false)
                {
                    if (currentCarNumber > 1)
                    {
                        //spawnParticlesPrefab = (GameObject)Instantiate(spawnParticles, cars[currentCarNumber-2].transform.position, cars[currentCarNumber-2].transform.rotation);
                        currentCarNumber--;
                    }
                }
            }
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
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
}
