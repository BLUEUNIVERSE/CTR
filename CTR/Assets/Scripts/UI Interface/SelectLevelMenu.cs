using UnityEngine;
using System.Collections;

public class SelectLevelMenu : MonoBehaviour {

    public int currentSlideNumber = 1;
    public int numberOfSlides = 3;
    public TweenPosition slide1;
    public Transform sliderVertical;
    public float animationSpeed = 0.2f;


    public UIImageButton[] episode1Buttons;


    /// for touch interactions
    Vector2 swipeDist;
    Vector2 swipeDistx;
    private Vector2 startPos;
    private float avgDistance;
    private float minSwipeDistance = 10;


    public TweenScale race1Details;
    public TweenScale race2Details;
    public TweenScale race3Details;

	// Use this for initialization
	void Start () {

        race1Details.Play(false);
        race2Details.Play(false);
        race3Details.Play(false);

        
	
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
                swipeDist = new Vector2( 0 ,(touch.position.y - startPos.y));
                //swipeDistx = new Vector2((touch.position.x - startPos.x), 0);
                // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
                //float avgDistY = touch.position.y - startPos.y;
                if (swipeDist.y > (Screen.height/3))
                {
                    if (currentSlideNumber < numberOfSlides)
                        currentSlideNumber++;
                }
                else if (swipeDist.y < -(Screen.height/3))
                {
                    if (currentSlideNumber > 1)
                        currentSlideNumber--;
                }
            }
        }

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                startPos = Input.mousePosition;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                //swipeDist = (new Vector2(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                swipeDist = new Vector2(0, (Input.mousePosition.y - startPos.y));
                //swipeDistx = new Vector2((touch.position.x - startPos.x), 0);
                // avgDistance = Mathf.Sqrt((swipeDist*swipeDist) + (swipeDistx*swipeDistx));
                Debug.Log(swipeDist.y);
                //float avgDistY = touch.position.y - startPos.y;
                if (swipeDist.y > Screen.height / 3)
                {
                    if (currentSlideNumber < numberOfSlides)
                        currentSlideNumber++;
                }
                else if (swipeDist.y < -Screen.height / 3)
                {
                    if (currentSlideNumber > 1)
                        currentSlideNumber--;
                }
            }
        }

        if (currentSlideNumber == 1)
        {
            sliderVertical.localPosition = Vector3.Lerp(sliderVertical.localPosition,
               new Vector3(sliderVertical.localPosition.x, 300, sliderVertical.localPosition.z), animationSpeed * Time.deltaTime);
        }
        else if (currentSlideNumber == 2)
        {
            sliderVertical.localPosition = Vector3.Lerp(sliderVertical.localPosition,
               new Vector3(sliderVertical.localPosition.x, 800, sliderVertical.localPosition.z), animationSpeed * Time.deltaTime);
        }
        else if (currentSlideNumber == 3)
        {
            sliderVertical.localPosition = Vector3.Lerp(sliderVertical.localPosition,
               new Vector3(sliderVertical.localPosition.x, 1300, sliderVertical.localPosition.z), animationSpeed * Time.deltaTime);
        }
       
        //sliderVertical.position

         //for (int i = 1; i <= numberOfSlides; i++)
         //{
         //    if (currentSlideNumber == i)
         //        transform.position = Vector3.Lerp(transform.position, new Vector3((i - 1) * 10, transform.position.y, transform.position.z), 5.0f * Time.deltaTime);
         //}

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
         {
             if (Input.GetKeyUp(KeyCode.DownArrow))
             {
                 if (currentSlideNumber < numberOfSlides)
                     currentSlideNumber++;
             }
             if (Input.GetKeyUp(KeyCode.UpArrow))
             {
                 if (currentSlideNumber > 1)
                     currentSlideNumber--;
             }
         }
	}
    void OnMenu()
    {
        Application.LoadLevel("Main Menu Scene");
    }

    void OnCars()
    {
        Application.LoadLevel("Select Car C");
    }

    void OnRace1()
    {
        race1Details.Play(true);
        //Application.LoadLevel("Select Car C");
    }

    void OnRace2()
    {
        race2Details.Play(true);
        //Application.LoadLevel("Select Car B");
    }

    void OnRace3()
    {
        race3Details.Play(true);
        
    }
    void AcceptRace1()
    {
        Application.LoadLevel("Select Car C");
    }

    void AcceptRace2()
    {
        Application.LoadLevel("Select Car B");
    }

    void AcceptRace3()
    {
        Application.LoadLevel("Select Car A");
    }

    void CloseRace3()
    {
        race1Details.Play(false);
        race2Details.Play(false);
        race3Details.Play(false);
    }
}
