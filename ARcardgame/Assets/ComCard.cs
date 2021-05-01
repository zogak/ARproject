using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComCard: MonoBehaviour
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
        if (GameManager.manager.comCardNum == 0) //ace
        {
            return materials[9];
        }
        else if (GameManager.manager.comCardNum == 1){ // 2
            return materials[0];
        }
        else if (GameManager.manager.comCardNum == 2)  // 3
        {
            return materials[1];
        }
        else if (GameManager.manager.comCardNum == 3)  // 4
        {
            return materials[2];
        }
        else if (GameManager.manager.comCardNum == 4)  // 5
        {
            return materials[3];
        }
        else if (GameManager.manager.comCardNum == 5)  // 6
        {
            return materials[4];
        }
        else if (GameManager.manager.comCardNum == 6)  // 7
        {
            return materials[5];
        }
        else if (GameManager.manager.comCardNum == 7)  // 8
        {
            return materials[6];
        }
        else if (GameManager.manager.comCardNum == 8)  // 9
        {
            return materials[7];
        }
        else  // 10
        {
            return materials[8];
        }

    }

}