using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update

    void Start()
    {
        //text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextValue(Single value) {
        Debug.Log(value);
        text.text = value.ToString();
    }
}
