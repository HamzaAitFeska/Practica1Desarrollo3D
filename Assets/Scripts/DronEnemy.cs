using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DronEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TSTATE
    {
        IDLE,
        PATROL,
        ALERT,
        CHASE,
        ATTACK,
        HIT,
        DIE
    }

    public TSTATE m_State;
    NavMeshAgent m_NavMasAgent;
    public List<Transform> m_PatrolPoints;
    int CurrentPatrolID = 0;
    public float PlayerinRange = 2.0f;
    public float m_ShightMax = 10;
    public float m_VisualConeAngle = 60.0f;
    public LayerMask m_ShightLayerMask;
    public float m_EyesPosition = 1.0f;
    public float m_PlayerEyesPosition = 1.0f;
    public float RangeToShootPlayer = 5f;
    public GameObject BulletPrefab;
    public bool IsAlerted;
    public float Dron_Life_MAX = 5;
    public float Dron_Current_Life;
    public float m_MaxShootDistance = 10.0f;
    public LayerMask m_ShootingLayerMask;
    public bool m_Shooting;
    public float TimeBetweenShots = 5f;
    public bool DronIsHit;
    private void Awake()
    {
        m_NavMasAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetIdleState();
        IsAlerted = false;
        Dron_Current_Life = Dron_Life_MAX;
        m_Shooting = false;
        DronIsHit = false;
    }
    private void Update()
    {
        switch (m_State)
        {
            case TSTATE.IDLE:
                UpdateIdleState();
                break;
            case TSTATE.PATROL:
                UpdatePatrolState();
                break;
            case TSTATE.ALERT:
                UpdateAlertState();
                break;
            case TSTATE.CHASE:
                UpdateChaseState();
                break;
            case TSTATE.ATTACK:
                UpdateAttackState();
                break;
            case TSTATE.HIT:
                UpdateHitState();
                break;
            case TSTATE.DIE:
                UpdateDieState();
                break;
            
        }
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        Vector3 l_EyesPosition = transform.position + Vector3.up * m_EyesPosition;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * m_PlayerEyesPosition;
        Debug.DrawLine(l_EyesPosition, l_PlayerEyesPosition, SeePlayer() ? Color.red : Color.blue);
        Debug.Log(m_State);
        Debug.Log(Dron_Current_Life);

    }

   void SetIdleState()
   {
        m_State = TSTATE.IDLE;
   }

    void UpdateIdleState()
    {
        SetPatrolState();
    }
    void SetPatrolState()
    {
        m_State = TSTATE.PATROL;
        m_NavMasAgent.destination = m_PatrolPoints[CurrentPatrolID].position;
    }

    void UpdatePatrolState()
    {
        if (PatrolTargetPosArrived())
        {
            IsAlerted = false;
            MoveNextPatrolPoint();
        }

        if (HearsPlayer() && !IsAlerted)
        {
            SetAlertState();
        }
    }

    bool PatrolTargetPosArrived()
    {
        return !m_NavMasAgent.hasPath && !m_NavMasAgent.pathPending && m_NavMasAgent.pathStatus == NavMeshPathStatus.PathComplete;
        //return false;
    }

    void MoveNextPatrolPoint()
    {
        ++CurrentPatrolID;
        if(CurrentPatrolID == m_PatrolPoints.Count)
        {
            CurrentPatrolID = 0;
        }
        m_NavMasAgent.destination = m_PatrolPoints[CurrentPatrolID].position;

    }
    void SetAlertState()
    {
        m_State = TSTATE.ALERT;
        m_NavMasAgent.destination = transform.position;
        
    }

    void UpdateAlertState()
    {
        
        transform.Rotate(0, .5f, 0);
        if (SeePlayer() && !PlayerInRangeToShoot())
        {
            SetChaseState();
        }

        if(SeePlayer() && PlayerInRangeToShoot())
        {
            SetAttackState();
        }

        if (!SeePlayer() && IsAlerted)
        {
            SetPatrolState();
        }

        if (!SeePlayer())
        {
            StartCoroutine(WaitForBeingAlerted());
        }

        if (DronIsHit)
        {
            SetHitState();
        }

    }
    void SetHitState()
    {
        m_State = TSTATE.HIT;
        m_NavMasAgent.destination = transform.position;
    }

    void UpdateHitState()
    {
        if(Dron_Current_Life <= 0)
        {
            SetDieState();
        }
    }
    void SetAttackState()
    {
        m_State = TSTATE.ATTACK;
        m_NavMasAgent.destination = transform.position;
    }

    void UpdateAttackState()
    {
        if (CanShhot())
        {
           ShotDron();
        }

        if (DronIsHit)
        {
            SetHitState();
        }
    }
    void SetDieState()
    {
        m_State = TSTATE.DIE;
        m_NavMasAgent.destination = transform.position;
    }

    void UpdateDieState()
    {
        Destroy(gameObject,1f);
    }
    void SetChaseState()
    {
        m_State = TSTATE.CHASE;
    }

    void UpdateChaseState()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        MoveTowardsToPlayer();
        if(PlayerInRangeToShoot())
        {
            SetAttackState();
        }

        if (DronIsHit)
        {
            SetHitState();
        }
       
    }

    bool HearsPlayer()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        return Vector3.Distance(l_PlayerPosition, transform.position) <= PlayerinRange; //&& FPSPlayerController.instance.m_PlayerIsMoving;
    }       
    
    bool SeePlayer()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        Vector3 l_DirectionToPlayerXZ = l_PlayerPosition - transform.position;
        l_DirectionToPlayerXZ.y = 0;
        l_DirectionToPlayerXZ.Normalize();
        Vector3 l_ForwarXZ = transform.forward;
        l_ForwarXZ.y = 0;
        l_ForwarXZ.Normalize();
        Vector3 l_EyesPosition = transform.position + Vector3.up * m_EyesPosition;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * m_PlayerEyesPosition;
        Vector3 l_Direction = l_PlayerEyesPosition - l_EyesPosition;
        Ray l_Ray = new Ray(l_EyesPosition,l_Direction);
        float l_lenght = l_Direction.magnitude;
        l_Direction /= l_lenght;

        return Vector3.Distance(l_PlayerPosition, transform.position) < m_ShightMax && Vector3.Dot(l_ForwarXZ, l_DirectionToPlayerXZ) > Mathf.Cos(m_VisualConeAngle * Mathf.Deg2Rad / 2.0f) && !Physics.Raycast(l_Ray, l_lenght, m_ShightLayerMask.value);
    }

    public void Hit(float Life)
    {
        Debug.Log(Life);
        Dron_Current_Life -= Life;
        DronIsHit = true;
        StartCoroutine(EndHit());
    }

    void MoveTowardsToPlayer()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        m_NavMasAgent.destination = Vector3.MoveTowards(transform.position, l_PlayerPosition, 1.0f);
    }

    void ShotDron()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        Vector3 l_EyesPosition = transform.position + Vector3.up * m_EyesPosition;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * m_PlayerEyesPosition;
        Vector3 l_Direction = l_PlayerEyesPosition - l_EyesPosition;
        //Instantiate(BulletPrefab, Vector3.MoveTowards(transform.position, l_PlayerPosition, 1.0f), Quaternion.identity);
        Ray l_Ray = new Ray(l_EyesPosition, l_Direction);
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxShootDistance, m_ShootingLayerMask))
        {
            CreatShootHitParticle(l_RaycastHit.collider, l_RaycastHit.point, l_RaycastHit.normal);
            PlayerLife.instance.DamagePlayer();
        }
        m_Shooting = true;
        StartCoroutine(EndShoot());
    }

    bool PlayerInRangeToShoot()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        return Vector3.Distance(l_PlayerPosition, transform.position) <= RangeToShootPlayer;
    }

    private IEnumerator WaitForBeingAlerted()
    {
        yield return new WaitForSeconds(6.7f);
        IsAlerted = true;
    }

    void CreatShootHitParticle(Collider collider, Vector3 position, Vector3 Normal)
    {
        Debug.DrawRay(position, Normal * 5.0f, Color.red, 2.0f);
        //GameObject.Instantiate(PrefabBulletHole, position, Quaternion.LookRotation(Normal));

    }

    public IEnumerator EndShoot()
    {
        yield return new WaitForSeconds(TimeBetweenShots);
        m_Shooting = false;
    }
    bool CanShhot()
    {
        return !m_Shooting;
    }

    public IEnumerator EndHit()
    {
        yield return new WaitForSeconds(2f);
        DronIsHit = false;
    }
}
