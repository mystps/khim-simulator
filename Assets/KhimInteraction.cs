using UnityEngine;

public class KhimInteraction : MonoBehaviour
{
    public AudioClip[] sounds; // Array to hold the audio clips for each note

    private AudioSource lastAudioSource; // Track the last played audio source

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Note"))
            {
                GameObject noteObject = hit.collider.gameObject;
                int noteIndex = int.Parse(noteObject.name.Substring(4));

                if (noteIndex >= 1 && noteIndex <= sounds.Length)
                {
                    AudioSource audioSource = noteObject.GetComponent<AudioSource>();

                    if (audioSource != null && audioSource.clip != null)
                    {
                        int audioIndex = noteIndex - 1;

                        if (lastAudioSource == audioSource)
                        {
                            if (audioSource.isPlaying)
                            {
                                audioSource.Stop();
                                audioSource.Play();
                                Debug.Log("Restarted audio");
                            }
                            else
                            {
                                audioSource.clip = sounds[audioIndex];
                                audioSource.Play();
                                Debug.Log("Played audio");
                            }
                        }
                        else
                        {
                            audioSource.clip = sounds[audioIndex];
                            audioSource.Play();
                            Debug.Log("Played audio");

                            if (lastAudioSource != null && lastAudioSource.isPlaying)
                            {
                                lastAudioSource.Stop();
                                Debug.Log("Stopped previous audio");
                            }

                            lastAudioSource = audioSource;
                        }
                    }
                }
            }
        }
    }
}