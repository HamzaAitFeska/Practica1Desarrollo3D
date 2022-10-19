using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FPSPlayerController : MonoBehaviour
{
    float m_Yaw;
    float m_Pitch;
    public float m_PitchRotationalSpeed;
    public float m_YawRotationalSpeed;
    public float m_AirTime;
    public float m_MinPitch;
    public float m_MaxPitch;
    

    public static FPSPlayerController instance;
    
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
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;


    public Camera m_Camera;
    public Camera m_CameraWeapon;
    public float m_NormalMovementFOV=60.0f;
    public float m_RunMovementFOV=75.0f;
    public GameObject PrefabBulletHole;
    public bool m_Shooting;
    public bool m_IsReloading;

    public Animation m_Animation;
    public AnimationClip m_IdleClip;
    public AnimationClip m_ShotClip;
    public AnimationClip m_ReloadClip;
    public AnimationClip m_RunClip;

    public bool m_PlayerIsMoving = false;

    float m_VerticalSpeed = 0.0f;
    public bool m_OnGround = true; //REMOVE PUBLIC AFTER FIXED

    public float m_JumpSpeed = 10.0f;
    bool m_AngleLocked = false;
    bool m_AimLocked = true;
    public bool m_TargetHit = false;
    void Start()
    {
        m_Yaw = transform.rotation.y;
        m_Pitch = m_PitchCotroller.localRotation.x;
        Cursor.lockState = CursorLockMode.Locked;
        //m_AimLocked = Cursor.lockState = CursorLockMode.Locked;
        SetIdleWeaponAnimation();
        m_Shooting = false;
        m_IsReloading = false;
        instance = this;
    }

#if UNITY_EDITOR
    void UpadteInputDebug()
    {
        if (Input.GetKeyDown(m_DebugLockAngleKeyCode))
            m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }
#endif
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpadteInputDebug();
#endif
        //Movement
        Vector3 l_RightDirection = transform.right;
        Vector3 l_ForwardDirection = transform.forward;
        Vector3 l_Direction = Vector3.zero;
        float l_Speed = m_PlayerSpeed;
        float l_FOV = m_NormalMovementFOV;
        float FOV_Speed = 0.3f;
        
        if (Input.GetKey(m_UpKeyCode))
            l_Direction = l_ForwardDirection;
        if (Input.GetKey(m_DownKeyCode))
            l_Direction = -l_ForwardDirection;
        if (Input.GetKey(m_RightKeyCode))
            l_Direction += l_RightDirection;
        if (Input.GetKey(m_LeftKeyCode))
            l_Direction -= l_RightDirection;
        //Jump if SpaceBar is pressed down and player is on ground
        if (Input.GetKeyDown(m_JumpKeyCode) && m_AirTime < 0.1f)
            m_VerticalSpeed = m_JumpSpeed; 
        //Run if shift is pressed
        if (Input.GetKey(m_RunKeyCode) && l_Direction != Vector3.zero & !m_IsReloading)
        {
            l_Speed = m_PlayerSpeed * m_FastSpeedMultiplier;
            l_FOV = m_RunMovementFOV;
            SetRunWeaponAnimation();
            //l_FOV = m_RunMovementFOV;
        }
        if (Input.GetKeyUp(m_RunKeyCode))
        {
            SetIdleWeaponWithRunAnimation();          
        }

        if(l_Direction != Vector3.zero)
        {
            m_PlayerIsMoving = true;
        }
        else
        {
            m_PlayerIsMoving = false;
        }

        

        
        m_Camera.fieldOfView = Mathf.Lerp(m_Camera.fieldOfView, l_FOV, FOV_Speed);
        //m_CameraWeapon.fieldOfView = Mathf.Lerp(m_CameraWeapon.fieldOfView, l_FOV, FOV_Speed);


        //m_Camera.fieldOfView = l_FOV;

        l_Direction.Normalize();

        Vector3 l_Movement = l_Direction * l_Speed * Time.deltaTime;
        
        //Rotation
        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");
#if UNITY_EDITOR
        if (m_AngleLocked)
        {
            l_MouseX = 0.0f;
            l_MouseY = 0.0f;
        }
#endif
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
            m_AirTime = 0;
        }
        else 
        {
            m_AirTime += Time.deltaTime;
            m_OnGround = false;
        }

        if(Input.GetMouseButtonDown(0) & CanShhot() & !m_IsReloading & PlayerAmmo.instance.currentAmmo > 0)
        {
            Shoot();
        }

        
    }

    public IEnumerator EndShoot()
    {
        yield return new WaitForSeconds(m_ShotClip.length);
        m_Shooting = false;
    }
    bool CanShhot()
    {
        return !m_Shooting;
    }
    public float m_MaxShootDistance = 50.0f;
    public LayerMask m_ShootingLayerMask;
    void Shoot()
    {
        Ray l_Ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit l_RaycastHit;
        if(Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxShootDistance, m_ShootingLayerMask))
        {
            CreatShootHitParticle(l_RaycastHit.collider, l_RaycastHit.point, l_RaycastHit.normal);
        }
        SetShootWeaponAnimation();
        m_Shooting = true;
        StartCoroutine(EndShoot());
        if(PlayerAmmo.instance.currentAmmo > 0)
        {
            PlayerAmmo.instance.LoseAmmo();
        }
        if(PlayerAmmo.instance.currentAmmo <= 0)
        {
            PlayerAmmo.instance.currentAmmo = 0;
        }
        if (l_RaycastHit.collider.CompareTag("DronCollider"))
        {
            l_RaycastHit.collider.GetComponent<HitCollider>().Hit();
        }
        if (l_RaycastHit.collider.CompareTag("TargetCollider"))
        {
            l_RaycastHit.collider.GetComponent<HitColliderTarget>().Hit();
            m_TargetHit = true;
        }
        
    }

    void CreatShootHitParticle(Collider collider,Vector3 position,Vector3 Normal)
    {
        //Debug.DrawRay(position, Normal * 5.0f, Color.red, 2.0f);
        GameObject.Instantiate(PrefabBulletHole, position, Quaternion.LookRotation(Normal));

    }

    void SetIdleWeaponAnimation()
    {
        m_Animation.CrossFade(m_IdleClip.name);
    }

    void SetShootWeaponAnimation()
    {
        m_Animation.CrossFade(m_ShotClip.name,0.1f);
        m_Animation.CrossFadeQueued(m_IdleClip.name, 0.1f);
    }
    void SetRunWeaponAnimation()
    {
        m_Animation.CrossFade(m_RunClip.name, 0.1f);
        m_Animation.CrossFadeQueued(m_IdleClip.name, 0.1f);
    }
    void SetIdleWeaponWithRunAnimation()
    {
        m_Animation.CrossFade(m_IdleClip.name, 0.1f);
        m_Animation.CrossFadeQueued(m_RunClip.name, 0.1f);
    }
    
}
