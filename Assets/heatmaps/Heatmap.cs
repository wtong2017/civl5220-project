// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;

public class Heatmap : MonoBehaviour
{
    public Vector4[] positions;
    public Vector4[] properties;

    public Material material;

    public int count = 50;

    void Start() {
        positions = new Vector4[count];
        properties = new Vector4[count];

        for (int i = 0; i < positions.Length; i++) {
            positions[i] = new Vector4(transform.position.x+Random.Range(-0.4f, +0.4f), transform.position.y + Random.Range(-0.4f, +0.4f), transform.position.z, 0);
            properties[i] = new Vector4(Random.Range(0f, 0.25f), Random.Range(-0.25f, 1f), 0, 0);
        }
    }

    void Update() {
        for (int i = 0; i < positions.Length; i++)
            positions[i] += new Vector4(Random.Range(-0.1f, +0.1f), Random.Range(-0.1f, +0.1f), 0, 0) * Time.deltaTime;

        UpdateData();
    }

    void UpdateData() {
        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }
}