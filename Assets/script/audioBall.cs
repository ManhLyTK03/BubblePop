using UnityEngine;

public class audioBall : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Lấy AudioSource từ đối tượng hiện tại
        audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy(){
        PlayExplosionSound();
    }
    void PlayExplosionSound()
    {
        Debug.Log("Explosion");
        // Phát âm thanh nổ
        audioSource.Play();
    }
}
