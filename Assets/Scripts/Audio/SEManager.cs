using UnityEngine;
using UnityEngine.UI;

public class SEManagert : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        this.audioSource.volume = GameData.instance.SE_Volume;
    }

    private void Update()
    {
        this.audioSource.volume = GameData.instance.SE_Volume;
    }
}
