using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonscript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;
    float timebetween;
    public float starttimebetween;

    [SerializeField] private AudioSource bulletLaunchSoundEffect;
    public Transform player;
    [SerializeField] private float proximityDistance = 25f;

    // Start is called before the first frame update
    void Start()
    {
        timebetween = starttimebetween;   
    }

    // Update is called once per frame
    void Update()
    {
        if (timebetween <= 0)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);

            if (player != null)
                if (Vector3.Distance(transform.position, player.position) < proximityDistance)
                    bulletLaunchSoundEffect.Play();

            timebetween = starttimebetween;
        }
        else
        {
            timebetween-= Time.deltaTime;
        }
    }
}
