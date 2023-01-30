using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRay : MonoBehaviour
{
    private Camera m_Camera;
    public Camera cam
    {
        get
        {
            if (m_Camera == null)
                m_Camera = GetComponent<Camera>();
            return m_Camera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
            {
                var block = hit.transform.GetComponent<Block>();
                if (block == null)
                    return;

                block.OnRayClick();
            }
        }
    }
}
