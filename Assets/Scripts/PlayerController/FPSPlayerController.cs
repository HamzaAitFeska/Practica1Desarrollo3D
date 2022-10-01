using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{
    float m_Yaw;
    float m_Pitch;
    public float m_PitchRotationalSpeed;
    public float m_YawRotationalSpeed;

    public float m_MinPitch;
    public float m_MaxPitch;

    public Transform m_PitchCotroller;
    public bool m_useYawInverted;
    public bool m_UsePitchInverted;

    public CharacterController m_characterController;
    public float m_PlayerSpeed;
    public float m_FastSpeedMultiplier;
    public KeyCode m_LeftKeyCode;
    public KeyCode m_RightKeyCode;
    public KeyCode m_UpKeyCode;
    public KeyCode m_DownKeyCode;
    public KeyCode m_JumpKeyCode = KeyCode.Space;
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;

    public Camera m_Camera;
    public float m_NormalMovementFOV=60.0f;
    public float m_RunMovementFOV=75.0f;

    float m_VerticalSpeed = 0.0f;
    public bool m_OnGround = true; //REMOVE PUBLIC AFTER FIXED

    public float m_JumpSpeed = 10.0f;

    void Start()
    {
        m_Yaw = transform.rotation.y;
        m_Pitch = m_PitchCotroller.localRotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movement
        Vector3 l_RightDirection = transform.right;
        Vector3 l_ForwardDirection = transform.forward;
        Vector3 l_Direction = Vector3.zero;
        float l_Speed = m_PlayerSpeed;
        float l_FOV = m_NormalMovementFOV;
        Vector3 zero = new Vector3 (0, 0, 0);
        if (Input.GetKey(m_UpKeyCode))
            l_Direction = l_ForwardDirection;
        if (Input.GetKey(m_DownKeyCode))
            l_Direction = -l_ForwardDirection;
        if (Input.GetKey(m_RightKeyCode))
            l_Direction += l_RightDirection;
        if (Input.GetKey(m_LeftKeyCode))
            l_Direction -= l_RightDirection;
        //Jump if SpaceBar is pressed down and player is on ground
        if (Input.GetKeyDown(m_JumpKeyCode) && m_OnGround)
            m_VerticalSpeed = m_JumpSpeed; 
        //Run if shift is pressed
        if (Input.GetKey(m_RunKeyCode))
        {
            l_Speed = m_PlayerSpeed * m_FastSpeedMultiplier;
            if (l_Speed == m_PlayerSpeed)
            {
                l_FOV = m_NormalMovementFOV;
                
            }

           if(l_Speed == m_PlayerSpeed * m_FastSpeedMultiplier && l_Direction != zero)
           {
                l_FOV = m_RunMovementFOV;
           }
            //l_FOV = m_RunMovementFOV;
        }
       
        
        
        m_Camera.fieldOfView = l_FOV;

        l_Direction.Normalize();

        Vector3 l_Movement = l_Direction * l_Speed * Time.deltaTime;
        
        //Rotation
        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");

        m_Yaw = m_Yaw + l_MouseX * m_YawRotationalSpeed * Time.deltaTime*(m_useYawInverted ? -1.0f : 1.0f);
        m_Pitch = m_Pitch + l_MouseY * m_PitchRotationalSpeed * Time.deltaTime * (m_UsePitchInverted ? -1.0f : 1.0f);
        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);

        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchCotroller.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);

        
        //m_characterController.Move(l_Movement);

        m_VerticalSpeed = m_VerticalSpeed + Physics.gravity.y * Time.deltaTime;
        l_Movement.y = m_VerticalSpeed * Time.deltaTime;

        CollisionFlags l_CollisionFlags = m_characterController.Move(l_Movement); //POSIBLE ERROR HERE ON GROUND CONDITION
        if ((l_CollisionFlags & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
            m_VerticalSpeed = 0.0f;
        if ((l_CollisionFlags & CollisionFlags.Below) != 0) //POSIBLE ERROR HERE ON GROUND CONDITION
        {
            m_VerticalSpeed = 0.0f;
            m_OnGround = true;
        }
        else m_OnGround = false;
    }
}
