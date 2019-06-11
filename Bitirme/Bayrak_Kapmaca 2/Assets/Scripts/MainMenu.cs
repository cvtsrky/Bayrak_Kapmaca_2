using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Bu classta main menüdeki işlemler için gereken fonksiyonlar yer almaktadır.
/// </summary>

public class MainMenu : MonoBehaviour 
{

	public Button Easy;
	public Button Medium;
	public Button Hard;
	//Play menü içinde yer alan değişkenler.
	public InputField playername1; //1. Oyuncu ismini alır.
	public InputField playername2; //2. Oyuncu ismini alır.
	public GameObject uyariPanel;  // Oyuncu isimleri girilmediği takdirde verilecek uyarı için gereken panel.

	//Awake() fonksiyonu .exe çalıştırıldıktan ilk işlenecek komutlar için bir fonksiyondur.
	void Awake()
	{
		// Başlangıçtan itibaren seçilen objeyi yok etme. 
		//Burada oyun açıldığında başlayan müziğin diğer oyun sahnesine geçince yok olmaması sağlanır.
		DontDestroyOnLoad (transform.gameObject);  
	}

	//Start() fonksiyonu oyun başladığı anda yapılması gerekenler için kullanılır. 
	void Start()
	{
		//Uyarı mesajı için gereken paneli başlangıçta pasif yapar.
		uyariPanel.SetActive (false);
		Easy.onClick.AddListener (easy);
		Medium.onClick.AddListener (medium);
		Hard.onClick.AddListener (hard);
	}

	public void easy ()
	{
		AlphaBeta.maxDepth = 2;
	}

	public void medium ()
	{
		AlphaBeta.maxDepth = 3;
	}

	public void hard ()
	{
		AlphaBeta.maxDepth = 4;
	}
	//
	public void PlayGame()
	{
		
		// Oyuncu isimleri girilmediyse sorguya girip uyarı mesaj panelini aktif hale getirir.
		if (playername1.text == "" || playername2.text == "") 
		{
			uyariPanel.SetActive (true);
		}

		// Oyuncu isimleri girildiyse girilen isimleri oyun sahnesinde bulunan skorborda yazar.
		else 
		{
			Score.playernamestr1 = playername1.text; //Score scripti içindeki stringe eşitleme yapar.
			Score.playernamestr2 = playername2.text; //Score scripti içindeki stringe eşitleme yapar.
			//İsmi verilen oyun sahnesine geçiş sağlanır.
			SceneManager.LoadScene("Game");
		}
	}



	public void QuitGame()
	{
		// Oyunun kapatılmasını sağlar.
		Application.Quit ();
	}
}
