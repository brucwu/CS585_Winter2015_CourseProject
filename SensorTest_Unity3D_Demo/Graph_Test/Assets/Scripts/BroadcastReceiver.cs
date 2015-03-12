using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
// This class receive the data sent from Myo's Accelerometer
public class BroadcastReceiver : MonoBehaviour
{
	AndroidJavaClass jc;
	string pose = "";
	public string head, x, y, z;
	Quaternion localRotation;
	public float speed = 1.0f;
	void Start()
	{
		Input.gyro.enabled = true;
		// Acces the android java receiver we made
		jc = new AndroidJavaClass("com.calHack.brucewu.myotounity.MyoReceiver");
		// We call our java class function to create our MyReceiver java object
		jc.CallStatic("createInstance");       
	}
	void OnGUI()
	{
		// Displaying the data sent from Bluetooth Accelerometer
		GUI.Label (new Rect (20, 20, 200, 200), "Bluetooth Accelerometer");
		GUI.Label (new Rect (20, 40, 200, 200), "x = " + x);
		GUI.Label (new Rect (20, 60, 200, 200), "y = " + y);
		GUI.Label (new Rect (20, 80, 200, 200), "z = " + z);
	}
	
	void Update()
	{              
		// We get the text property of our receiver
		pose = jc.GetStatic<string>("text");
		char[] delimiters = new char[] { ',', ' ' };
		string[] parts = pose.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
		// Parsing the data sent from Bluetooth Accelerometer
		head = parts [0];
		x = parts [1];
		y = parts [2];
		z = parts [3];
	}

}
