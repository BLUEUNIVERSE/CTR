using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public TweenPosition settingsPanel;
    public TweenPosition creditsPanel;
    public TweenPosition playerProfilePanel;
    public bool creditsOnSameScreen = false;
    public UILabel playerNameLabel;
    public TweenPosition profilePage;

    public UIInput playerName;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPlay()
    {
        Application.LoadLevel("Select Level Menu");
    }

    void OnCars()
    {
        Application.LoadLevel("Select Car Menu");
    }

    void OnSettings()
    {
        settingsPanel.Play(true);
    }

    void OnSettingsBack()
    {
        settingsPanel.Play(false);
    }

    void OnCredits()
    {
        if(creditsOnSameScreen)
        creditsPanel.Play(true);
        else
        {
            Application.LoadLevel("Credits");
        }
    }

    void OnCreditsBack()
    {
        creditsPanel.Play(false);
    }

    void OnPlayerProfile()
    {
        //playerProfilePanel.Play(true);
        profilePage.Play(true);
    }

    void OnProfileBack()
    {
        playerProfilePanel.Play(false);
    }

    void OnOk()
    {
        if (playerName != null)
        {
           playerNameLabel.text = playerName.text; 
           
        }
         if(playerName.label.text == "Player Name")
            {
                playerNameLabel.text = "You";
            }    
       
        profilePage.Play(false);
        
    }
}

