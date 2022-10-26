using TMPro;
using UnityEngine;

public class HitColliderTarget : MonoBehaviour
{
    public float m_Points;
    public float totalPoints;
    public static HitColliderTarget instance;
    //public TMP_Text textScore;
    private void Start()
    {        
        instance = this;
        totalPoints = 0;
    }
    public void GivePoints()
    {
        totalPoints += m_Points;
        
    }
    




}
   
