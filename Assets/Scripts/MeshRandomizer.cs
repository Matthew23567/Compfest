using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRandomizer : MonoBehaviour
{
    [Header("Use 'Check Children' to check for validity of nameIdentifier")]
    [Space(10)]
    [SerializeField]
    [Tooltip("Enter the beginning name of the mesh to specify the targets")]
    public string nameIdentifier;
    [SerializeField]
    [Tooltip("Enter the specified index value of the child to not hide. Index starts with 1. Set to 0 to randomize")]
    public int childSelector;
    [SerializeField]
    [Tooltip("Every children found within this gameObject. Those not also in selectedChildren will be unaffected")]
    public List<GameObject> allChildren;
    [SerializeField]
    [Tooltip("The list of every child that was found with the specified nameIdentifier.")]
    public List<GameObject> selectedChildren;
    // Start is called before the first frame update
    void Start()
    {
        
        CheckChildren();

        if (childSelector > 0)
        {
            childSelector -= 1;
        } else
        {
            childSelector = Random.Range(0, selectedChildren.Count - 1);
        }

        for (int i = 0; i < selectedChildren.Count; i++)
        {
            if (i != childSelector) selectedChildren[i].SetActive(false);
        }

        SkinnedMeshRenderer shownMesh = selectedChildren[childSelector].GetComponent<SkinnedMeshRenderer>();
        for (int blend = 0; blend < shownMesh.sharedMesh.blendShapeCount; blend++)
        {
            shownMesh.SetBlendShapeWeight(blend, Random.Range(0f, 100f));
        }
    }

    [ContextMenu("CheckChildren")]
    void CheckChildren()
    {
        allChildren.Clear();
        selectedChildren.Clear();

        foreach (Transform child in transform)
        {
            allChildren.Add(child.gameObject);
        }

        foreach (Transform child in transform)
        {
            if (child.name.StartsWith(nameIdentifier))
            {
                selectedChildren.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
