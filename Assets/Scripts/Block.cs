using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Renderer m_Renderer;
    private Renderer render
    {
        get
        {
            if (m_Renderer == null)
                m_Renderer = GetComponent<Renderer>();
            return m_Renderer;
        }
    }
    public enum eType
    {
        Soil,
        Root,
    }
    [System.Flags]
    public enum eRoot
    {
        E = 1 << 0,
        S = 1 << 1,
        W = 1 << 2,
        N = 1 << 3,
    }
    [SerializeField] eType m_Type = eType.Soil;
    private eType CacheType = eType.Soil;

    [SerializeField] GameObject m_Soil;
    [SerializeField] GameObject m_Root;

    [Header("Root")]
    [SerializeField] eRoot m_RType = (eRoot)0;
    private eRoot CacheRoot = default;
    [SerializeField] GameObject m_RootE, m_RootS, m_RootW, m_RootN;


    private void OnValidate()
    {
        if (CacheType != m_Type)
        {
            Set(m_Type);
        }
        if (CacheRoot != m_RType)
        {
            Set(m_RType);
        }
    }

    public void OnRayClick()
    {
        // Toggle();
        if (m_Type != eType.Root)
        {
            Set(eType.Root);
        }
        else
        {
            ToggleRootLogic();
        }
    }

    public void Set(eType _type)
    {
        CacheType = m_Type = _type;
        m_Soil?.SetActive(m_Type == eType.Soil);
        m_Root?.SetActive(m_Type == eType.Root);
    }

    public void Set(eRoot _type)
    {
        CacheRoot = m_RType = _type;
        m_RootE.SetActive((_type & eRoot.E) != 0);
        m_RootS.SetActive((_type & eRoot.S) != 0);
        m_RootW.SetActive((_type & eRoot.W) != 0);
        m_RootN.SetActive((_type & eRoot.N) != 0);
    }

    private void ToggleRootLogic()
    {
        
    }

    private void Toggle()
    {
        render.enabled = !render.enabled;
    }
}
