using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Entropedia;

public class TimeSimulation : MonoBehaviour
{
    public Sun sun;
    public float theta = -1f;
    public TextMeshProUGUI text;

    int minutes;

    // Start is called before the first frame update
    void Start()
    {
        // https://www.timeanddate.com/astronomy/hong-kong/hong-kong
        // sun 0 degree; sun rise is 6:50
        //sun.transform.rotation.eulerAngles.Set(0, 0, 0);
        //minutes = 6 * 60 + 50;
        //text.text = Minute2Time(minutes);
    }

    // Update is called once per frame
    void Update()
    {
        // https://brainly.in/question/16328914
        // The sun takes about 4 minutes to cross one degree of longitude.
        //sun.transform.RotateAround(Vector3.zero, Vector3.right, theta);
        //minutes += -(int)(theta*4);
        //minutes %= 60 * 24;
        //text.text = Minute2Time(minutes);
        text.text = sun.GetTime().ToString("t");
    }

    string Minute2Time(int total) {
        int hours = total / 60;
        int minutes = total % 60;
        return string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
