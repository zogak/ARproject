using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private Renderer renderer;
    public Camera target;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        ChangeMaterial();
    }

    void Update()
    {
        Vector3 v = target.transform.position - transform.position;

        v.Normalize();

        Quaternion q = Quaternion.LookRotation(v);

        Quaternion qRotation = Quaternion.Euler(270f, 0f, 0f);

        transform.rotation = q * qRotation;

    }

    public void ChangeMaterial()
    {
        renderer.material = SelectRandomMaterial();
    }
    private Material SelectRandomMaterial()
    {
        if (GameManager.manager.playerCardNum == 0) //ace
        {
            return materials[9];
        }
        else if (GameManager.manager.playerCardNum == 1)
        { // 2
            return materials[0];
        }
        else if (GameManager.manager.playerCardNum == 2)  // 3
        {
            return materials[1];
        }
        else if (GameManager.manager.playerCardNum == 3)  // 4
        {
            return materials[2];
        }
        else if (GameManager.manager.playerCardNum == 4)  // 5
        {
            return materials[3];
        }
        else if (GameManager.manager.playerCardNum == 5)  // 6
        {
            return materials[4];
        }
        else if (GameManager.manager.playerCardNum == 6)  // 7
        {
            return materials[5];
        }
        else if (GameManager.manager.playerCardNum == 7)  // 8
        {
            return materials[6];
        }
        else if (GameManager.manager.playerCardNum == 8)  // 9
        {
            return materials[7];
        }
        else  // 10
        {
            return materials[8];
        }

    }

}