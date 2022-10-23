using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public float m_Life;
    public DronEnemy m_DroneEnemy;
    public void Hit()
    {
        //m_DrownEnemy.Hit(m_Life);
        if (!m_DroneEnemy.DronIsHit)
        {
           m_DroneEnemy.Hit(m_Life);
        }
        //Debug.Log(m_Life);
    }
}
   
