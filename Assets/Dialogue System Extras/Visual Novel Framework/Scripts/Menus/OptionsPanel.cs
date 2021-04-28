using UnityEngine;

namespace PixelCrushers.DialogueSystem.VisualNovelFramework
{
	
	public class OptionsPanel : GeneralPanel
    {

        public UnityEngine.UI.Slider musicVolumeSlider;
        public UnityEngine.UI.Slider sfxVolumeSlider;
        public UnityEngine.UI.Slider characterSpeedSlider;

        protected override void Start()
        {
            if (musicVolumeSlider != null) musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            if (sfxVolumeSlider != null) sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume");
            if (characterSpeedSlider != null) characterSpeedSlider.value = PlayerPrefs.GetFloat("CharacterSpeed");
        }

        public void MusicVolumeChanged()
        {
            SetVolume(musicVolumeSlider, GameObject.Find("Music Audio Source"), "MusicVolume");
        }

        public void SfxVolumeChanged()
        {
            SetVolume(sfxVolumeSlider, DialogueManager.Instance.gameObject, "SfxVolume");
        }

        private void SetVolume(UnityEngine.UI.Slider slider, GameObject audioSourceObject, string playerPrefsKey)
        {
            if (slider == null) return;
            var audioSource = (audioSourceObject != null) ? audioSourceObject.GetComponent<AudioSource>() : null;
            if (audioSource != null) audioSource.volume = slider.value;
            PlayerPrefs.SetFloat(playerPrefsKey, slider.value);
        }

        public void TypewriterSpeedChanged()
        {
            var typewriterEffect = FindObjectOfType<AbstractTypewriterEffect>();
            if (characterSpeedSlider == null || typewriterEffect == null) return;
            typewriterEffect.charactersPerSecond = characterSpeedSlider.value;
            PlayerPrefs.SetFloat("CharacterSpeed", characterSpeedSlider.value);
            DialogueManager.DisplaySettings.subtitleSettings.subtitleCharsPerSecond = Mathf.Min(characterSpeedSlider.value, DialogueManager.DisplaySettings.subtitleSettings.subtitleCharsPerSecond);
        }
	}

}