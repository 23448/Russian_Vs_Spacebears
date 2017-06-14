using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	private static TestScript instance5;
	
	public static TestScript Instance
	{
		get { return instance5; }
	}

	public string UniqueID;
	public string Name;
	public int Score;

}
