using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupText : MonoBehaviour
{
    UnityEngine.UI.Text text;
    private float hidetime = 0.01f;
    private float time = 0;

    private void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }

    public void ShowText(Vector3 Screenpos)
    {
        text.enabled = true;
        text.transform.position = Screenpos;
        time = Time.time;
    }

    public void Update()
    {
        if(Time.time > time + hidetime)
        {
            text.enabled = false;
        }
    }
}
