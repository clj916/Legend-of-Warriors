using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGroud;
    public LayerMask groudLayer;
    public float checkRaduis;
    public Vector2 bottomOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    public void Check()
    {
        isGroud = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groudLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
