using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class rotate : MonoBehaviour {
	string filePath = "/sdcard/spectoccular"; // Location of the emulated input text file
	public string fileName = "Xiaomi_3_Accel_X_Data.txt"; // Name of the emulated input text file
	StreamReader fReader;
	public Toggle tog; // Toggle emulated input On/Off

	float[] array_x;
	// Use this for initialization
	void Start () {
		array_x = new float[200];
	}
	
	// Update is called once per frame
	void Update () {
		if (tog.isOn) { // use emulated input from text file
			int index = (int)((Input.acceleration.x * 100) + 100);
			// Array boundary protection
			if (index > 199)
				index = 199;
			if (index < 0)
				index = 0;
			// Rotate the Cube in X-Axis using emulated input
			transform.Rotate (Vector3.up * array_x [index] * 5.0f);
		} else { // use phone's real accelerometer data
			// Rotate the Cube in X-Axis using real input
			transform.Rotate (Vector3.up * Input.acceleration.x * 5.0f);
		}
	}
	// Load emulated input data from text file into an array
	public void LoadData() {
		fReader = File.OpenText (filePath+"/"+fileName);
		for(int x = 0; x < 200; x++)
			array_x[x] = float.Parse(fReader.ReadLine());
		fReader.Close ();
	}
}
