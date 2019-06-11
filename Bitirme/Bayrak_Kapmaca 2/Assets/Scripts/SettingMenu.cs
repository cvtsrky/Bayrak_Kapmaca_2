using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
// Ayarlar menüsündeki ses ayarının yapıldığı sınıftır.
public class SettingMenu : MonoBehaviour 
{
	public AudioMixer audioMixer;

	public void SetVolume(float volume)
	{
		audioMixer.SetFloat ("volume",volume);
	}

}
