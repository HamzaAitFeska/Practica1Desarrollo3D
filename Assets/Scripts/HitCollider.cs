﻿using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public float m_Life;
    public DronEnemy m_DroneEnemy;
    public void Hit()
    {
        
        if (!m_DroneEnemy.DronIsHit)
        {
           m_DroneEnemy.Hit(m_Life);
        }
        
    }
}
   
