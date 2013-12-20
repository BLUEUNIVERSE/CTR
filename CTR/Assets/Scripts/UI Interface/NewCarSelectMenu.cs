using UnityEngine;
using System.Collections;

public class NewCarSelectMenu : MonoBehaviour {

    // to move camera to next car
    public int numberOfCars = 3;
    private int maxPos;
    private int minPos;
    private int currentCarNumber = 1;
    public GameObject spawnParticles;
    private GameObject spawnParticlesPrefab;
    public Transform spawnParticlesPoint;

    // info about coins and costs for cars
    public UILabel totalCoinsLabel;
    public UILabel upgradeCostLabel;
    public int totalCoins = 1000;
    public int maxLevelForCars = 3;

    // array of cars
    public GameObject[] cars;


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

            if (touch.phase == TouchPhase.Ended)
            {
                //swipeDist = (new Vector2(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                //swipeDist = new Vector2(0, (touch.position.y - startPos.y));
                swipeDistx = new Vector2(0, (touch.position.x - startPos.x));
                // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
                //float avgDistY = touch.position.y - startPos.y;

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

        MoveCamera();
        for (int i = 1; i <= numberOfCars; i++)
        {
            if (currentCarNumber == i)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3((i - 1) * 10, transform.position.y, transform.position.z), 5.0f * Time.deltaTime);
                
                
                int temp = i - 1;
                if (cars[temp].GetComponent<CarProperties>().sticker2Unlocked == true)
                {
                    purchaseSticker2.Play(false);
                }
                else
                    purchaseSticker2.Play(true);


                CheckCarPerk(temp);

                if (cars[temp].GetComponent<CarProperties>().carLevel == 0) 
               
                {
                    upgradeButton.Play(true);
                    unlockButton.Play(false);
                    raceButton.Play(true);
                    purchaseSticker2.Play(false);
                    stickerButtons.Play(false);
                    sun.intensity = 0.2f;
                   
                    //perk1.enabled = false;
                    //perk1.enabled = true;
                    upgradeCostLabel.text = "Unlock $" + cars[temp].GetComponent<CarProperties>().coinsToUnlock.ToString();
                    newSpeed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[0], newSpeed.foreground.localScale.y, newSpeed.foreground.localScale.z);
                    speed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[0], speed.foreground.localScale.y, speed.foreground.localScale.z);

                    newAcc.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[0], newAcc.foreground.localScale.y, newAcc.foreground.localScale.z);
                    acceleration.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[0], acceleration.foreground.localScale.y, acceleration.foreground.localScale.z);

                    newHandling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[0], newHandling.foreground.localScale.y, newHandling.foreground.localScale.z);
                    handling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[0], handling.foreground.localScale.y, handling.foreground.localScale.z);

                    newNitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[0], newNitro.foreground.localScale.y, newNitro.foreground.localScale.z);
                    nitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[0], nitro.foreground.localScale.y, nitro.foreground.localScale.z);

                    newShield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[0], newShield.foreground.localScale.y, newShield.foreground.localScale.z);
                    shield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[0], shield.foreground.localScale.y, shield.foreground.localScale.z);
                }
                else if (cars[temp].GetComponent<CarProperties>().carLevel == 1)
                {
                    upgradeButton.Play(false);
                    unlockButton.Play(true);
                    raceButton.Play(false);
                    stickerButtons.Play(true);
                    sun.intensity = 8.0f;
                   
                   
                    upgradeCostLabel.text = "Upgrade $" + cars[temp].GetComponent<CarProperties>().coinsToUpgradeLevel2.ToString();
                    newSpeed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[1], newSpeed.foreground.localScale.y, newSpeed.foreground.localScale.z);
                    speed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[0], speed.foreground.localScale.y, speed.foreground.localScale.z);

                    newAcc.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[1], newAcc.foreground.localScale.y, newAcc.foreground.localScale.z);
                    acceleration.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[0], acceleration.foreground.localScale.y, acceleration.foreground.localScale.z);

                    newHandling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[1], newHandling.foreground.localScale.y, newHandling.foreground.localScale.z);
                    handling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[0], handling.foreground.localScale.y, handling.foreground.localScale.z);

                    newNitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[1], newNitro.foreground.localScale.y, newNitro.foreground.localScale.z);
                    nitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[0], nitro.foreground.localScale.y, nitro.foreground.localScale.z);

                    newShield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[1], newShield.foreground.localScale.y, newShield.foreground.localScale.z);
                    shield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[0], shield.foreground.localScale.y, shield.foreground.localScale.z);
                }
                else if (cars[temp].GetComponent<CarProperties>().carLevel == 2)
                {
                    raceButton.Play(false);
                    stickerButtons.Play(true);
                    upgradeButton.Play(false);
                    sun.intensity = 8.0f;
                    upgradeCostLabel.text = "Upgrade $" + cars[temp].GetComponent<CarProperties>().coinsToUpgradeLevel3.ToString();
                    newSpeed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[2], newSpeed.foreground.localScale.y, newSpeed.foreground.localScale.z);
                    speed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[1], speed.foreground.localScale.y, speed.foreground.localScale.z);

                    newAcc.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[2], newAcc.foreground.localScale.y, newAcc.foreground.localScale.z);
                    acceleration.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[1], acceleration.foreground.localScale.y, acceleration.foreground.localScale.z);

                    newHandling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[2], newHandling.foreground.localScale.y, newHandling.foreground.localScale.z);
                    handling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[1], handling.foreground.localScale.y, handling.foreground.localScale.z);

                    newNitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[2], newNitro.foreground.localScale.y, newNitro.foreground.localScale.z);
                    nitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[1], nitro.foreground.localScale.y, nitro.foreground.localScale.z);

                    newShield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[2], newShield.foreground.localScale.y, newShield.foreground.localScale.z);
                    shield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[1], shield.foreground.localScale.y, shield.foreground.localScale.z);
                }
                else if (cars[temp].GetComponent<CarProperties>().carLevel == 3)
                {
                    upgradeButton.Play(true);
                    unlockButton.Play(true);
                    raceButton.Play(false);
                    stickerButtons.Play(true);
                    sun.intensity = 8.0f;
                    upgradeCostLabel.text = "MAX LEVEL";
                    newSpeed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[2], newSpeed.foreground.localScale.y, newSpeed.foreground.localScale.z);
                    speed.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().speedLevels[2], speed.foreground.localScale.y, speed.foreground.localScale.z);

                    newAcc.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[2], newAcc.foreground.localScale.y, newAcc.foreground.localScale.z);
                    acceleration.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().accelerationLevels[2], acceleration.foreground.localScale.y, acceleration.foreground.localScale.z);

                    newHandling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[2], newHandling.foreground.localScale.y, newHandling.foreground.localScale.z);
                    handling.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().handlingLevels[2], handling.foreground.localScale.y, handling.foreground.localScale.z);

                    newNitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[2], newNitro.foreground.localScale.y, newNitro.foreground.localScale.z);
                    nitro.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().nitroLevels[2], nitro.foreground.localScale.y, nitro.foreground.localScale.z);

                    newShield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[2], newShield.foreground.localScale.y, newShield.foreground.localScale.z);
                    shield.foreground.localScale = new Vector3(cars[temp].GetComponent<CarProperties>().ShieldLevels[2], shield.foreground.localScale.y, shield.foreground.localScale.z);
                }
            }
        }
	}

    void OnUpgrade()
    {

        
            if (cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel == 1 && totalCoins >=  cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsToUpgradeLevel2)
            {
                cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel++;
                totalCoins -= cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsToUpgradeLevel2;
                totalCoinsLabel.text = totalCoins.ToString();
                spawnParticlesPrefab = (GameObject)Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
            }
            else if (cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel == 2 && totalCoins >=  cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsToUpgradeLevel3)
            {
                cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel++;
                totalCoins -= cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsToUpgradeLevel3;
                totalCoinsLabel.text = totalCoins.ToString();
                spawnParticlesPrefab = (GameObject)Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
            }
            Debug.Log(cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel);
        
    }

    void OnPurchase()
    {

        if (totalCoins >= cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsToUnlock)
        {
            spawnParticlesPrefab = (GameObject)Instantiate(spawnParticles, spawnParticlesPoint.position, spawnParticlesPoint.rotation);
            cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel++;
            totalCoins -= cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsToUnlock;
            totalCoinsLabel.text = totalCoins.ToString();
            //cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker = cars[currentCarNumber - 1].GetComponent<CarProperties>().stickers[0];
            //cars[currentCarNumber - 1].renderer.material.mainTexture = cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker;
        }
            Debug.Log(cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel);
       
        
    }

    void OnRace()
    {

        if (cars[currentCarNumber - 1].GetComponent<CarProperties>().sticker2Unlocked == false)
            cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker = cars[currentCarNumber - 1].GetComponent<CarProperties>().stickers[0];
           

        if (cars[currentCarNumber-1].GetComponent<CarProperties>().carLevel > 0)
        Application.LoadLevel("TestScene");
        

    }

    void OnSticker1()
    {
        if (cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel == 1)
        {
            cars[currentCarNumber - 1].renderer.material.mainTexture = cars[0].GetComponent<CarProperties>().stickers[0];

            //if (cars[currentCarNumber - 1].GetComponent<CarProperties>().sticker1Unlocked == true)
            //    cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker = cars[currentCarNumber - 1].GetComponent<CarProperties>().stickers[0];

        }
    }
    void OnSticker2()
    {
        if (cars[currentCarNumber - 1].GetComponent<CarProperties>().carLevel > 0)
        {
            cars[currentCarNumber - 1].renderer.material.mainTexture = cars[0].GetComponent<CarProperties>().stickers[1];

            if (cars[currentCarNumber - 1].GetComponent<CarProperties>().sticker2Unlocked == true)
            {
                cars[currentCarNumber - 1].GetComponent<CarProperties>().currentSticker = cars[currentCarNumber - 1].GetComponent<CarProperties>().stickers[1];
                raceButton.Play(false);
            }
        }
    }

    void OnPurchaseSticker2()
    {
        if(totalCoins > cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsForSticker2)
        {
            totalCoins -= cars[currentCarNumber - 1].GetComponent<CarProperties>().coinsForSticker2;
            cars[currentCarNumber - 1].GetComponent<CarProperties>().sticker2Unlocked = true;
            totalCoinsLabel.text = totalCoins.ToString();
            raceButton.Play(true);

        }
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
            cars[currentCarNumber - 1].GetComponent<CarProperties>().perk1 = 1;
            perk1.enabled = false;
            perk1.enabled = true;

            print("slot 1 perk = energy");
        }
        else if (currentPerkSlotNumber == 2)
        {
            slot2Perk = 1;
            perk2.normalSprite = "exit button";
            perk2.hoverSprite = "exit button";
            perk2.pressedSprite = "exit button";
            cars[currentCarNumber - 1].GetComponent<CarProperties>().perk2 = 1;

            print("slot 2 perk = energy");
        }
        else if (currentPerkSlotNumber == 3)
        {
            slot3Perk = 1;
            perk3.normalSprite = "exit button";
            perk3.hoverSprite = "exit button";
            perk3.pressedSprite = "exit button";

            cars[currentCarNumber - 1].GetComponent<CarProperties>().perk3 = 1;
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
            cars[currentCarNumber - 1].GetComponent<CarProperties>().perk1 = 2;
            perk1.enabled = false;
            perk1.enabled = true;

            print("slot 1 perk = energy");
        }
        else if (currentPerkSlotNumber == 2)
        {
            slot2Perk = 1;
            perk2.normalSprite = "exit button";
            perk2.hoverSprite = "exit button";
            perk2.pressedSprite = "exit button";
            cars[currentCarNumber - 1].GetComponent<CarProperties>().perk2 = 2;

            print("slot 2 perk = energy");
        }
        else if (currentPerkSlotNumber == 3)
        {
            slot3Perk = 1;
            perk3.normalSprite = "exit button";
            perk3.hoverSprite = "exit button";
            perk3.pressedSprite = "exit button";

            cars[currentCarNumber - 1].GetComponent<CarProperties>().perk3 = 2;
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


    void MoveCamera()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            //swipeDist = (new Vector2(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
            swipeDist = new Vector2( (Input.mousePosition.x - startPos.x), 0);
            //swipeDistx = new Vector2((touch.position.x - startPos.x), 0);
            // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
            Debug.Log(swipeDist.x);
            //float avgDistY = touch.position.y - startPos.y;
            if (swipeDist.x < -50)
            {
                if (isSelectingPerk == false)
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
                if (isSelectingPerk == false)
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
