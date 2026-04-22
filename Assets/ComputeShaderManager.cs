using Unity.VisualScripting;
using UnityEngine;

public class ComputeShaderManager : MonoBehaviour
{
    private ComputeShader initializeGrassComputeShader;
    private ComputeBuffer grassDataBuffer;
    [SerializeField] private int dimension = 10;
    [SerializeField] private int scale = 1;
    [SerializeField] private Material[] grassMaterials;

    struct GrassData
{
    Vector4 position;
};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        initializeGrassComputeShader = Resources.Load<ComputeShader>("GrassSpawner");
        grassDataBuffer = new ComputeBuffer(dimension * dimension, sizeof(float) * 4);

        initializeGrassComputeShader.SetBuffer(0, "_GrassDataBuffer", grassDataBuffer);
        initializeGrassComputeShader.SetInt("Dimension", dimension);
        initializeGrassComputeShader.SetInt("Scale", scale);
        initializeGrassComputeShader.Dispatch(0, Mathf.CeilToInt(dimension / 8.0f), Mathf.CeilToInt(dimension / 8.0f), 1);        

        grassMaterials[0].SetBuffer("_GrassDataBuffer", grassDataBuffer);
        grassMaterials[1].SetBuffer("_GrassDataBuffer", grassDataBuffer);
        grassMaterials[2].SetBuffer("_GrassDataBuffer", grassDataBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onDisable()
    {
        if (grassDataBuffer != null)
        {
            grassDataBuffer.Release();
        }
    }
}
