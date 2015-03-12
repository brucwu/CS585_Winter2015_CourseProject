using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class countPercentage : MonoBehaviour {
	public Text percent;
	public Slider slider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void calcPercent ()
	{
		percent.text = (int)((slider.value / 200f) * 100) + "%";
	}
}
