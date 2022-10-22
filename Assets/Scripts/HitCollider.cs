using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public float m_Life;
    //public DronEnemy m_DrownEnemy;
    public void Hit()
    {
        //m_DrownEnemy.Hit(m_Life);
        if (DronEnemy.instacne.DronIsHit)
        {
            DronEnemy.instacne.Hit(0);
        }
        else
        {
            DronEnemy.instacne.Hit(m_Life);
        }
        //Debug.Log(m_Life);
    }
}
   
