using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HitCollider : MonoBehaviour
{
    public float m_Life;
    public DronEnemy m_DrownEnemy;
    public void Hit()
    {
        m_DrownEnemy.Hit(m_Life);
    }
}

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
    
    private void Awake()
    {
        m_NavMasAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetIdleState();
       
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
            MoveNextPatrolPoint();
        }

        if (HearsPlayer())
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
        if (SeePlayer())
        {
            SetChaseState();
        }
        
        if(!HearsPlayer())
        {
            SetPatrolState();
        }
        

    }
    void SetHitState()
    {
        m_State = TSTATE.HIT;
    }

    void UpdateHitState()
    {

    }
    void SetAttackState()
    {
        m_State = TSTATE.ATTACK;
        m_NavMasAgent.destination = transform.position;
    }

    void UpdateAttackState()
    {

    }
    void SetDieState()
    {
        m_State = TSTATE.DIE;
    }

    void UpdateDieState()
    {

    }
    void SetChaseState()
    {
        m_State = TSTATE.CHASE;
    }

    void UpdateChaseState()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        MoveTowardsToPlayer();
        if(Vector3.Distance(l_PlayerPosition,transform.position) <= RangeToShootPlayer)
        {
            SetAttackState();
        }
       
    }

    bool HearsPlayer()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        return Vector3.Distance(l_PlayerPosition, transform.position) <= PlayerinRange && FPSPlayerController.instance.m_PlayerIsMoving;
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
    }

    void MoveTowardsToPlayer()
    {
        Vector3 l_PlayerPosition = FPSPlayerController.instance.transform.position;
        m_NavMasAgent.destination = Vector3.MoveTowards(transform.position, l_PlayerPosition, 1.0f);
    }
}
