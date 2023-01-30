using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Grid m_Instance = null;
    public Grid instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<Grid>();
            return m_Instance;
        }
    }

    [SerializeField] GameObject m_BlockPrefab = null;
    [SerializeField] int m_Height = 12;
    [SerializeField] int m_Width = 12;
    [SerializeField] Transform m_Parent = null;
    private Dictionary<Vector3Int, Block> blocks = new Dictionary<Vector3Int, Block>();
    
    private void OnEnable()
    {
        for (int x = 0; x <= m_Width; ++x)
        {
            for (int y = 0; y <= m_Height; ++y)
            {
                var token   = Instantiate(m_BlockPrefab, m_Parent, false);
                var _x      = x;
                var _y      = y;
                var id      = new Vector3Int(_x, _y, 0);
                token.transform.localPosition = (Vector3)id;
                var block   = token.GetComponent<Block>();
                if (block != null)
                {
                    //TODO: generate method.
                    block.Set(Block.eType.Soil);
                    blocks.Add(id, block);
                }
            }
        }
    }
    private void OnDisable()
    {
        List<Block> list = new List<Block>(blocks.Values);
        blocks.Clear();

        int cnt = list.Count;
        while (cnt-- > 0)
        {
            Destroy(list[cnt].gameObject);
        }
    }
}
