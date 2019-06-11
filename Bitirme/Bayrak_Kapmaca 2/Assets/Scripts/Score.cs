using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public static int wscore; //1. Oyuncunun skoru tutulur.
	public static int bscore; //2. Oyuncunun skoru tutulur.

	public static string playernamestr1; //1. Oyuncunun ismini tutar.
	public static string playernamestr2; //2. Oyuncunun ismini tutar.

	public Text textwhite; //1. Oyuncunun ismi ve skorunu ekrana basar.
	public Text textblack; //2. Oyuncunun ismi ve skorunu ekrana basar.

	//Awake() fonksiyonu .exe çalıştırıldıktan ilk işlenecek komutlar için bir fonksiyondur.
	void Awake()
	{
		//Başlangıçta skorlar 0'a eşitlenir.
		wscore = 0;
		bscore = 0;
	}
	//Update() fonksiyonu herbir hareket veya aksiyon sonrası içindekileri günceller.
	void Update()
	{
		//Burada skorlar sürekli güncellenir. ".text" ile ekrana basılır.
		textwhite.text = playernamestr1 + " : " + wscore;
		textblack.text = playernamestr2 + " : " + bscore;
	}

}
