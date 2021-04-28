using UnityEngine;

namespace PixelCrushers.DialogueSystem.VisualNovelFramework
{
	
	public class LoadVolume : MonoBehaviour
    {

        public string playerPrefsKey = "Volume";

        public void Start()
        {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null) audioSource.volume = PlayerPrefs.GetFloat(playerPrefsKey, 1);
        }
	}

}