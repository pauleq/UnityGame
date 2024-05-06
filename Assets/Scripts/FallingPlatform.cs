using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;
    public bool fallup=false;
    private bool falling = false;
    public AudioSource audioObject;
    public AudioClip FallClip;

    [SerializeField] Animator anim;
    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!falling)
            {
                falling = true;
                StartCoroutine(Fall());
            }
        }
    }

    private IEnumerator Fall()
    {
        audioObject.Play();
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        audioObject.Stop();
        anim.SetTrigger("End");
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (fallup) rb.gravityScale = -1;
        audioObject.clip = FallClip;
        audioObject.Play();
        rb.mass = 4f;
        Destroy(gameObject, destroyDelay);
    }
}
