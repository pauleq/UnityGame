using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject leftBoundary;
    [SerializeField] private GameObject rightBoundary;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private GameObject chatBubblePrefab;
    [SerializeField] private int intervalMinimum = 10;
    [SerializeField] private int intervalMaximum = 30;
    [SerializeField] private List<string> textPhrases;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private float leftCap;
    private float rightCap;
    private GameObject chatBubble;
    private TextMeshPro textMesh;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get left and right caps from GameObjects
        if (leftBoundary != null)
            leftCap = leftBoundary.transform.position.x;
        else
            Debug.LogWarning("Left boundary not assigned to Enemy script.");

        if (rightBoundary != null)
            rightCap = rightBoundary.transform.position.x;
        else
            Debug.LogWarning("Right boundary not assigned to Enemy script.");

        StartCoroutine(ShowChatBubblePeriodically());
    }

    void Update()
    {
        // Move the enemy
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (transform.position.x >= rightCap)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (transform.position.x <= leftCap)
            {
                Flip();
            }
        }

        // Update chat bubble position if it exists
        if (chatBubble != null)
        {
            Vector3 bubblePosition = transform.position + Vector3.up * 1.5f;
            chatBubble.transform.position = bubblePosition;
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator ShowChatBubblePeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(intervalMinimum, intervalMaximum));

            if (chatBubblePrefab != null && textPhrases.Count > 0)
            {
                // Destroy previous chat bubble if exists
                if (chatBubble != null)
                    Destroy(chatBubble);

                // Spawn new chat bubble
                chatBubble = Instantiate(chatBubblePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                textMesh = chatBubble.GetComponentInChildren<TextMeshPro>();

                if (textMesh != null)
                {
                    string randomText = textPhrases[Random.Range(0, textPhrases.Count)];
                    textMesh.text = randomText;
                }

                StartCoroutine(HideChatBubble());
            }
        }
    }

    IEnumerator HideChatBubble()
    {
        yield return new WaitForSeconds(6f);

        if (chatBubble != null)
        {
            Destroy(chatBubble);
            chatBubble = null;
        }
    }

    void OnDestroy()
    {
        // Stop the chat bubble coroutines if the enemy is destroyed
        StopCoroutine(ShowChatBubblePeriodically());
        StopCoroutine(HideChatBubble());

        if (chatBubble != null)
        {
            Destroy(chatBubble);
            chatBubble = null;
        }
    }
}
