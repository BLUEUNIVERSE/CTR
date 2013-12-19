using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public TweenPosition settingsPanel;
    public TweenPosition creditsPanel;
    public TweenPosition playerProfilePanel;
    public bool creditsOnSameScreen = false;
    public UILabel playerNameLabel;
    public TweenPosition profilePage;
    public float animationSpeed = 1.0f;

    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Vector3 temp; 
    public Vector3 temp2;
    private bool moveCamera = true;

    //private TweenPosition mainCameraMove;

    

    public UIInput playerName;

    private int tempTime = 0;


	// Use this for initialization
	void Start () {
        //GetComponent<TweenPosition>().from = point1.position;
        //GetComponent<TweenPosition>().to = point2.position;

        //temp = new Vector3(Random.Range(-50, 50), Random.Range(-9, 30), Random.Range(-100, 100));
        //temp2 = new Vector3(Random.Range(-100, 100), Random.Range(-9, 30), Random.Range(-100, 100));
        //GetComponent<TweenRotation>().from = new Vector3(point1.eulerAngles.x, point1.eulerAngles.y, point1.eulerAngles.z);
        //GetComponent<TweenRotation>().to = new Vector3(point2.eulerAngles.x, point2.eulerAngles.y, point2.eulerAngles.z);
	    //Time.timeScale = 0.0f;
        
	}
	
	// Update is called once per frame
	void Update () {

	    
	       //transform.position = Vector3.Lerp(point1.position, point2.position, 5.0f * Time.deltaTime); ));
        
        if (Time.timeSinceLevelLoad > tempTime)
        {
            gameObject.GetComponent<TweenPosition>().from = new Vector3(Random.Range(-50, 50), Random.Range(9, 10), Random.Range(-100, 100));
            gameObject.GetComponent<TweenPosition>().to = new Vector3(Random.Range(-100, 100), Random.Range(9, 30), Random.Range(-100, 100));
            //temp = new Vector3(Random.Range(-50, 50), Random.Range(9, 10), Random.Range(-100, 100));
            //temp2 = new Vector3(Random.Range(-100, 100), Random.Range(9, 30), Random.Range(-100, 100));
            gameObject.GetComponent<TweenPosition>().Play(true);
            Debug.Log(temp.x + " " + temp.y + " " + temp.z);
            Debug.Log(temp2.x + " " + temp2.y + " " + temp2.z);
            tempTime += 5;
        }

    
        
        
        
        //transform.position = Vector3.Slerp(point1.position, point2.position, animationSpeed );
        //if (Input.GetMouseButtonUp(0))
        //{
        //    GetComponent<TweenPosition>().from = point3.position;
        //    GetComponent<TweenPosition>().to = point4.position;

        //    GetComponent<TweenPosition>().Play(true);
        //}

        //GetComponent<TweenRotation>().from = new Vector3(point1.eulerAngles.x, point1.eulerAngles.y, point1.eulerAngles.z);
        //GetComponent<TweenRotation>().to = new Vector3(point2.eulerAngles.x, point2.eulerAngles.y, point2.eulerAngles.z);

        transform.LookAt(point3);
   
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

    void OnOptionsOk()
    {
        settingsPanel.Play(false);
    }

    void OnExit()
    {
        Application.Quit();
    }
}

