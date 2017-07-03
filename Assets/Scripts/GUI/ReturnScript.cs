using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnScript : MonoBehaviour {

    public GameObject Canvas;

	public void Return()
    {
        Canvas.gameObject.SetActive(false);
    }
}
