using UnityEngine;


public class CarProperties : MonoBehaviour 
{

    public int carLevel = 0;

    public int expToUnlock = 0;
    public int coinsToUnlock = 0;
    public int coinsToUpgradeLevel2;
    public int coinsToUpgradeLevel3;

    public int[] speedLevels;
    public int[] accelerationLevels;
    public int[] handlingLevels;
    public int[] nitroLevels;
    public int[] ShieldLevels;
    public Texture2D[] stickers;
    public Texture2D currentSticker;
    public bool sticker1Unlocked = true;
    public bool sticker2Unlocked = false;
    public bool sticker3Unlocked = false;
    public int perk1;
    public int perk2;
    public int perk3;

    public int coinsForSticker2 = 1000;
    
}
