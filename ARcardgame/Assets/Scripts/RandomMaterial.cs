using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        ChangeMaterial();
    }
    
    public void ChangeMaterial()
    {
        renderer.material = SelectRandomMaterial();
    }
    private Material SelectRandomMaterial()
    {
        return materials[Random.Range(0, materials.Length)];
    }
   
}
