using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class TestGraph : MonoBehaviour {
	public Slider x_slide;
	public Slider y_slide;
	public Slider z_slide;
	public BroadcastReceiver jc;
	float[] array_x;
	float[] array_y;
	float[] array_z;
	int countX;
	int countY;
	int countZ;


	string filePath = "/sdcard/spectoccular";
	public string fileName = "Xiaomi_3_Accel_X_Data.txt";

	StreamWriter fWriter;
	StreamReader fReader;
	// Use this for initialization
	void Start () {
		array_x = new float[200];
		array_y = new float[200];
		array_z = new float[200];

		for(int x = 0; x < 200; x++)
		{
			array_x[x] = -10;
			array_y[x] = -10;
			array_z[x] = -10;
		}
		countX = 0;
		countY = 0;
		countZ = 0;
	}
	// this function is called when the save data button is pressed
	// it generate the emulated input text file
	public void writeData()
	{
		// write the x-axis array data into a text file
		fWriter = File.CreateText(filePath+"/"+fileName);
		for(int i = 0; i < array_x.Length; i++)
			fWriter.WriteLine(array_x[i]);
		fWriter.Close ();
		Handheld.Vibrate ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		// read bluetooth accelerometer's x,y,z reading
		float x = float.Parse (jc.x);
		float y = float.Parse (jc.y);
		float z = float.Parse (jc.z);
		// read phone accelerometer's x,y,z reading
		float device_x = Input.acceleration.x;
		float device_y = Input.acceleration.y;
		float device_z = Input.acceleration.z;
		// normalize data to a set of integers from 0~200
		int int_x = (int)((x*100)+100);
		int int_y = (int)((y*100)+100);
		int int_z = (int)((z*100)+100);

		// boundary check
		if (int_x > 199)
			int_x = 199;
		if (int_y > 199)
			int_y = 199;
		if (int_z > 199)
			int_z = 199;

		if (int_x < 0)
			int_x = 0;
		if (int_y < 0)
			int_y = 0;
		if (int_z < 0)
			int_z = 0;

		// if x degree from bluetooth accelerometer have not been map with a real phone reading
		if(array_x[int_x] == -10f)
		{
			// map the bluetooth accelerometer input with phone's accelerometer input
			array_x[int_x] = device_x;
			countX++;
		}

		if(array_y[int_y] == -10f)
		{
			array_y[int_y] = device_y;
			countY++;
		}

		if(array_z[int_z] == -10f)
		{
			array_z[int_z] = device_z;
			countZ++;
		}

		// update the progress and show to user
		x_slide.value = countX;
		y_slide.value = countY;
		z_slide.value = countZ;
	}
	void OnGUI()
	{
		// display the reading of phone's accelerometer
		GUI.Label (new Rect (1200, 40, 200, 200), "x = " + Input.acceleration.x);
		GUI.Label (new Rect (1200, 60, 200, 200), "y = " + Input.acceleration.y);
		GUI.Label (new Rect (1200, 80, 200, 200), "z = " + Input.acceleration.z);
	}
}
