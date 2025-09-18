using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        this.audioSource.volume = GameData.instance.BGM_Volume;
    }

    private void Update()
    {
        this.audioSource.volume = GameData.instance.BGM_Volume;
    }

}
