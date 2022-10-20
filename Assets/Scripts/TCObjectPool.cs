using UnityEngine;
using System.Collections.Generic;

public class TCObjectPool 
{
    List<GameObject> m_ObjectsPool;
    int m_CurrentElemtId = 0;
    public TCObjectPool(int ElementsCount,GameObject Elemnt)
    {
        m_ObjectsPool = new List<GameObject>();
        for(int i = 0; i < ElementsCount; ++i)
        {
            GameObject l_Element = GameObject.Instantiate(Elemnt);
            l_Element.SetActive(false);
            m_ObjectsPool.Add(l_Element);
        }
        m_CurrentElemtId = 0;
    }

    public GameObject GetNextElemnt()
    {
        GameObject l_Elemnt = m_ObjectsPool[m_CurrentElemtId];
        ++m_CurrentElemtId;
        if(m_CurrentElemtId >= m_ObjectsPool.Count)
        {
            m_CurrentElemtId = 0;
        }

        return l_Elemnt;
    }
}
