using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    public bool fallup=false;

    [SerializeField] Animator anim;
    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        anim.SetTrigger("End");
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (fallup) rb.gravityScale = -1;
        rb.mass = 4f;
        Destroy(gameObject, destroyDelay);
    }
}
