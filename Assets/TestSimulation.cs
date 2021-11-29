using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RvtFader;

public class TestSimulation : MonoBehaviour
{
    // Start is called before the first frame update
    AttenuationCalculator calculator;
    public GameObject obj1;
    public GameObject obj2;

    void Start()
    {
        var doc = new Document();
        var settings = new Settings();
        calculator = new AttenuationCalculator(doc, settings);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(calculator.Attenuation(obj1.transform.position, obj2.transform.position));
    }
}
