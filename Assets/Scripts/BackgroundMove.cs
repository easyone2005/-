using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 0.01f;

    private MeshRenderer meshRenderer = null;
    private Vector2 offset = Vector2.zero;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        if (meshRenderer == null) return;
        offset.y += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
