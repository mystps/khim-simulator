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
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Note"))
                {
                    GameObject noteObject = hit.collider.gameObject;
                    int noteIndex = int.Parse(noteObject.name.Substring(4)) - 1;

                    if (noteIndex >= 0 && noteIndex < sounds.Length)
                    {
                        AudioSource audioSource = audioSources[noteIndex];

                        if (audioSource != null && audioSource.clip != null)
                        {
                            if (!audioSource.isPlaying)
                            {
                                // Play the audio if it's not already playing
                                audioSource.clip = sounds[noteIndex];
                                audioSource.Play();
                                Debug.Log("Played audio: " + noteIndex);
                            }
                            else
                            {
                                // Stop the audio if it's already playing
                                audioSource.Stop();
                                Debug.Log("Stopped audio: " + noteIndex);
                            }
                        }
                    }
                }
            }
        }
    }
}