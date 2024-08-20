using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BuildingInstancer : MonoBehaviour
{
    [Header("Assign these")]
    public GameObject body;
    public GameObject cap;
    public bool fixRotation;

    [Header("Specifications")]
    public int amount;
    float height;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ClearExistingBuilding();
    }

    private void Start()
    {
        GenerateBuilding();
    }

    [ContextMenu("Reset Building")]
    void ResetBuilding()
    {
        ClearExistingBuilding();
        GenerateBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateBuilding()
    {
        Vector3 position = transform.position;
        Quaternion correctedRotation = Quaternion.identity;
        if (fixRotation)
        {
            correctedRotation = Quaternion.Euler(-90, 0, 0);
        }

        height = body.GetComponent<Renderer>().bounds.size.y;
        

        for (int i = 0; i < amount; i++)
        {
            Instantiate(body, position, correctedRotation, transform);
            position.y += height;
        }

        Instantiate(cap, position, correctedRotation, transform);
    }    

    private void ClearExistingBuilding()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject); 
        }
    }
}
