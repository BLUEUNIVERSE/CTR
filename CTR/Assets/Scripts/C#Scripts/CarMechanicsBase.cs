using UnityEngine;
using System.Collections;

public class CarMechanicsBase : MonoBehaviour
{

    // Here's the basic car script described in my tutorial at www.gotow.net/andrew/blog.
    // A Complete explaination of how this script works can be found at the link above, along
    // with detailed instructions on how to write one of your own, and tips on what values to 
    // assign to the script variables for it to work well for your application.

    // These variables allow the script to power the wheels of the car.
    public WheelCollider m_FrontLeftWheel;
    public WheelCollider m_FrontRightWheel;
    public Transform m_FronLeftWheelTransform;
    public Transform m_FrontRightWheelTransform;

    public float m_fMaxWheelTurnAngle; // This angle is for turning wheels
    public float m_fMaxSteerAngle;     //this angle is for applying steering force in angle

    // These variables are for the gears, the array is the list of ratios. The script
    // uses the defined gear ratios to determine how much torque to apply to the wheels.
    public float[] m_aGearRatio = new float[1];
    public int m_iCurrentGear = 0;

    // These variables are just for applying torque to the wheels and shifting gears.
    // using the defined Max and Min Engine RPM, the script can determine what gear the
    // car needs to be in.
    public float m_fEngineTorque = 600.0f;
    public float m_fMaxEngineRPM = 3000.0f;
    public float m_fMinEngineRPM = 1000.0f;
    protected float m_fEngineRPM = 0.0f;

    protected int m_iAppropriateGear;


    protected void Init()
    {

    }

    protected void CarUpdate(Vector3 input)
    {
        // This is to limit the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
        // but it's easy, and it doesn't interfere with the physics processing.
        rigidbody.drag = rigidbody.velocity.magnitude / 250;

        // Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear void
        m_fEngineRPM = (m_FrontLeftWheel.rpm + m_FrontRightWheel.rpm) / 2 * m_aGearRatio[m_iCurrentGear];

        ShiftGears();

        // set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
        // up to twice it's pitch, where it will suddenly drop when it switches gears.
        audio.pitch = Mathf.Abs(m_fEngineRPM / m_fMaxEngineRPM) + 1.0f;
        // this line is just to ensure that the pitch does not reach a value higher than is desired.
        if (audio.pitch > 2.0f)
        {
            audio.pitch = 2.0f;
        }

        // finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
        // multiplied by the user input variable.
        m_FrontLeftWheel.motorTorque = m_fEngineTorque / m_aGearRatio[m_iCurrentGear] * input.y;
        m_FrontRightWheel.motorTorque = m_fEngineTorque / m_aGearRatio[m_iCurrentGear] * input.y;

        // the steer angle is an arbitrary value multiplied by the user input.
        m_FrontLeftWheel.steerAngle = 5 * input.x; //Input.GetAxis("Horizontal");
        m_FrontRightWheel.steerAngle = 5 * input.x; //Input.GetAxis("Horizontal");

        m_FronLeftWheelTransform.localEulerAngles = Vector3.up * m_fMaxSteerAngle * input.x * Time.deltaTime;
        m_FrontRightWheelTransform.localEulerAngles = Vector3.up * m_fMaxSteerAngle * input.x * Time.deltaTime;
    }

    void ShiftGears()
    {
        // this function shifts the gears of the vehicle, it loops through all the gears, checking which will make
        // the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
        if (m_fEngineRPM >= m_fMaxEngineRPM)
        {
            int AppropriateGear = m_iCurrentGear;

            for (int i = 0; i < m_aGearRatio.Length; i++)
            {
                if (m_FrontLeftWheel.rpm * m_aGearRatio[i] < m_fMaxEngineRPM)
                {
                    AppropriateGear = i;
                    break;
                }
            }

            m_iCurrentGear = AppropriateGear;
        }

        if (m_fEngineRPM <= m_fMinEngineRPM)
        {
            m_iAppropriateGear = m_iCurrentGear;
            for (var j = m_aGearRatio.Length - 1; j >= 0; j--)
            {
                if (m_FrontLeftWheel.rpm * m_aGearRatio[j] > m_fMinEngineRPM)
                {
                    m_iAppropriateGear = j;
                    break;
                }
            }
            m_iCurrentGear = m_iAppropriateGear;
        }
    }

}
