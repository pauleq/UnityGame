using UnityEngine;




public class AudioArea: MonoBehaviour
{
    public AudioSource audioObject; // drag the music player here or...

    void Start()
    { // find it at Start:
      // supposing that the music player is named "MusicPlayer":
        audioObject = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { // only an object tagged Player starts the sound
            audioObject.Play();
            Debug.Log("Player entered!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    //use ontriggerexit 2D instead of no 2D because of collider

    {
        if (other.tag == "Player")
        { // only an object tagged Player stops the sound
            audioObject.Stop();
            Debug.Log("Player exit!");
        }
    }
}
