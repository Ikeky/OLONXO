using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	[System.Serializable]
	public class Statistic{
		public int RealStats, CurrentStats, damage, rate;
		public Statistic(int _real,int _current,int _damage,int _rate){
			this.RealStats = _real;
			this.CurrentStats = _current;
			this.damage = _damage;
			this.rate = _rate;
		}
	}
	public Slider[] StatsSlider;
	public Text[] StatsText;
	public Statistic Health = new Statistic(100,100,10,2);
	public Statistic Satiety = new Statistic(100,100,10,2);
	public Statistic Temperature = new Statistic(100,100,10,2);
	private float timer = 0f;
	void Update(){
		ShowStats ();
		timer = timer + Time.deltaTime;
		TimerFunc ();
		for (int i = 0; i < StatsSlider.Length; i++) {
			
		}
	}
	void ShowStats(){
		StatsSlider[0].value = Health.CurrentStats;
		StatsSlider[1].value = Satiety.CurrentStats;
		StatsSlider[2].value = Temperature.CurrentStats;
		StatsText[0].text = Health.CurrentStats+"%";
		StatsText[1].text = Satiety.CurrentStats+"%";
		StatsText[2].text = Temperature.CurrentStats+"°";
	}
	void TimerFunc(){

	}
}