using UnityEngine;
using System.Collections;

/// <summary>
/// Aniket Kayande 
/// 17/12/2013
/// </summary>

public class InputManager : MonoBehaviour
{
    public bool m_bUseSliderControls;
    public bool m_bUseTouchControls;
    public bool m_bUseAccelerationControls;
    public bool m_bUseKeyboardControls;

    public UISlider m_SteeringSlider;
    public UIButton m_Throttle;
    public UIButton m_Break;
    
    private float m_fMidPointValue = 0.5f;

    private float m_fPositivePeak = 1.0f;
    private float m_fNegativePeak = -1.0f;
    public float m_fScreenDistanceThreshold = 100;
    private Vector2 m_vInitialTouchPosition;
    private Vector2 m_vFinalTouchPosition;
    private Vector2 m_vFingerMoveDirection;
    private float m_fTouchMovedDistance;
    public float m_fInputValueToPass;
    public float m_fThrottleValue;
    private bool m_bBrakPressed;
    public bool m_bInvertAccelerationAxis;

    void Start()
    {
        //m_bUseSliderControls = true; 
        //m_bUseTouchControls = true;
        //m_bUseAccelerationControls = true;

#if UNITY_EDITOR
        m_bUseKeyboardControls = true;
#endif 
#if UNITY_ANDRIOD
        m_bUseAccelerationControls = true;
#endif 
       
        UIEventListener.Get(m_Throttle.gameObject).onPress += OnThrottlePressed;
        UIEventListener.Get(m_Break.gameObject).onPress += OnBreakPressed;
        UIEventListener.Get(m_SteeringSlider.thumb.gameObject).onPress += OnSteeringPress; 
        m_SteeringSlider.eventReceiver = gameObject;
    }
    void Update()
    {
        if (m_bUseTouchControls)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    m_vInitialTouchPosition = touch.position;
                    print("Started the touch at position : " + touch.position);
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    m_fTouchMovedDistance = Vector2.Distance(m_vInitialTouchPosition, touch.position);
                    m_fInputValueToPass = Mathf.Clamp(m_fTouchMovedDistance, 0, m_fScreenDistanceThreshold) / m_fScreenDistanceThreshold;

                    if (touch.position.x < m_vInitialTouchPosition.x)
                    {
                        m_fInputValueToPass = -m_fInputValueToPass;
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    m_vFinalTouchPosition = touch.position;
                    print("Ended the touch at position : " + m_vFinalTouchPosition);
                    print("Touch Distance : " + m_fTouchMovedDistance);
                }

            }
        }
        if (m_bUseSliderControls)
        {
            m_fInputValueToPass = -(m_fMidPointValue - m_SteeringSlider.sliderValue)/m_fMidPointValue;
            if(!m_bBrakPressed)
                m_fThrottleValue = 1.0f;
        }

        if(m_bUseAccelerationControls)
        {
            m_fInputValueToPass = Mathf.Clamp(Input.acceleration.x, -m_fMidPointValue, m_fMidPointValue)/m_fMidPointValue;

            if (!m_bBrakPressed)
                m_fThrottleValue = 1.0f;
        }
        if (m_bUseKeyboardControls)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_fThrottleValue = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                m_fThrottleValue = -1;
            }
            else
            {
                m_fThrottleValue = 0;
            }

            m_fInputValueToPass = Input.GetAxis("Horizontal");
            
        }
    }


    void OnThrottlePressed(GameObject go, bool state)
    {
        if (state)
        {
            m_fThrottleValue = 1;
        }
        else
            m_fThrottleValue = 0;
    }

    void OnBreakPressed(GameObject go, bool state)
    {
        m_bBrakPressed = state;
        m_fThrottleValue = -1;
    }

    void OnSliderChange()
    {
        
    }

    void OnSteeringPress(GameObject go, bool state)
    {
  
    }

    void OnGUI()
    {
        string str = string.Format("InputValue : {0} Velocity Magniude : {1} and Ve: {2}", m_fInputValueToPass, rigidbody.velocity.magnitude, m_vFinalTouchPosition);
        string value = string.Format("Acceleration : {0} ", Input.acceleration);
        GUI.Button(new Rect(5, 5, 1024, 50), str);
        GUI.Button(new Rect(5, 55, 1024, 50), value);
    }

}
