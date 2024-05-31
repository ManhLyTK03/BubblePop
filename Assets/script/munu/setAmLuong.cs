using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // Tham chiếu đến thanh kéo
    public AudioSource audioSource; // Tham chiếu đến AudioSource để điều chỉnh âm lượng

    void Start()
    {
        // Thiết lập giá trị ban đầu của thanh kéo từ âm lượng của AudioSource
        volumeSlider.value = audioSource.volume;

        // Đăng ký sự kiện OnValueChanged của thanh kéo
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float volume)
    {
        // Điều chỉnh âm lượng của AudioSource
        audioSource.volume = volume;
    }
}
