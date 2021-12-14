using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RvtFader;

public class TestSimulation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj1;
    public GameObject obj2;
    public Gradient gradient;
    public GameObject heatmap;
    public Vector4[] positions;
    public Vector4[] properties;


    public Material material;

    int count;
    public int offsetPoint = 8;

    public float min = 0.1f;
    public float max = 5f;
    public GameObject linesObj;

    AttenuationCalculator calculator;
    List<LineRenderer> lines = new List<LineRenderer>();
    Bounds bounds;


    void Start()
    {
        count = offsetPoint * offsetPoint;
        positions = new Vector4[count];
   properties = new Vector4[count];
    bounds = heatmap.GetComponent<Renderer>().bounds;
        var doc = new Document();
        var settings = new Settings();
        calculator = new AttenuationCalculator(doc, settings);

        foreach (var item in obj2.transform) {
            lines.Add(DrawLine());
        }

        UpdatePlane();
        //foreach (var pos in positions) {
        //    var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    obj.transform.position = pos;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(calculator.Attenuation(obj1.transform.position, obj2.transform.position));

        for (int i = 0; i < lines.Count; i++) {
            var line = lines[i];
            var child = obj2.transform.GetChild(i);
            var attenuation = (float)calculator.Attenuation(obj1.transform.position, child.position);
            Debug.Log(attenuation);
            UpdateLine(line, obj1.transform.position, child.position, ColorFromGradient((attenuation-min)/(max-min)));
        }

        UpdatePlane();
        UpdateData();
    }

    LineRenderer DrawLine() {
        //For creating line renderer object
        var lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.transform.SetParent(linesObj.transform);
        return lineRenderer;
    }

    void UpdateLine(LineRenderer lineRenderer, Vector3 pos1, Vector3 pos2, Color color) {
        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, pos1); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, pos2); //x,y and z position of the end point of the line
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        //lineRenderer
        //calculator.Attenuation(obj1.transform.position, obj2.transform.position)
    }

    public void ShowLines(bool trigger) {
        linesObj.SetActive(trigger);
    }

    public void ShowHeatmap(bool trigger) {
        heatmap.SetActive(trigger);
    }

    void UpdatePlane() {
        for (int i = 0; i < offsetPoint; i++) {
            for (int j = 0; j < offsetPoint; j++) {
                positions[i * offsetPoint + j] = bounds.min + heatmap.transform.forward * bounds.size.z * transform.localScale.z * i /(offsetPoint-1) + heatmap.transform.right * transform.localScale.x * bounds.size.x * j /(offsetPoint-1);
                properties[i * offsetPoint + j] = new Vector4(Mathf.Max(bounds.size.z * transform.localScale.z/(offsetPoint-1), transform.localScale.x * bounds.size.x /(offsetPoint-1)), ((float)calculator.Attenuation(obj1.transform.position, positions[i * offsetPoint + j]) - min) / (max - min), 0, 0);
            }
        }
    }

    Color ColorFromGradient(float value)  // float between 0-1
{
        return gradient.Evaluate(value);
    }

    void UpdateData() {
        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }
}
