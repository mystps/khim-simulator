using UnityEngine;

public class KhimInteraction : MonoBehaviour
{
    public AudioClip[] sounds; // Array to hold the audio clips for each note

    private AudioSource[] audioSources; // Array to hold the audio sources for each note

    void Start()
    {
        audioSources = new AudioSource[sounds.Length];

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject noteObject = GameObject.Find("Note" + (i + 1));
            AudioSource audioSource = noteObject.GetComponent<AudioSource>();
            audioSources[i] = audioSource;

            // Preload and cache the audio clip
            audioSource.clip = sounds[i];
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 inputPosition = Input.touchCount > 0 ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) : Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Note"))
            {
                GameObject noteObject = hit.collider.gameObject;
                int noteIndex = int.Parse(noteObject.name.Substring(4)) - 1;

                if (noteIndex >= 0 && noteIndex < sounds.Length)
                {
                    AudioSource audioSource = audioSources[noteIndex];

                    if (audioSource != null && audioSource.clip != null)
                    {
                        audioSource.clip = sounds[noteIndex];
                        audioSource.Play();
                        Debug.Log("Played audio: " + noteIndex);
                    }
                }
            }
        }
    }
}