using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomizer : MonoBehaviour
{
    [Tooltip("This is all of the children found to have meshes. Note that any children WITHOUT a SkinnedMeshRenderer will not be added.")]
    public List<int> listOfChildrenWithMesh;
    [Tooltip ("Set a value to pick a single mesh from the children of this gameObject. Set to 0 to randomize. Set to a larger amount than the children amount to remove every mesh.")]
    public int meshSelection;
    [Tooltip("This is to make sure the gameObject is selected correctly")]
    public GameObject modelGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject modelGameObject = gameObject;
        //Checks all children of fbx object
        for (int i = 0; i < modelGameObject.transform.childCount; i++)
        {
            //Adds child's index to list if it contains SkinnedMeshRenderer Component, which is the mesh
            if (modelGameObject.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>() != null)
            {
                listOfChildrenWithMesh.Add(i);
            }
        }
        //Go through every childIndex to check and randomize it's shapekeys/blendshapes
        for (int selectedChildren = 0; selectedChildren < listOfChildrenWithMesh.Count; selectedChildren++)
        {
            GameObject Mesh = modelGameObject.transform.GetChild(listOfChildrenWithMesh[selectedChildren]).gameObject;
            SkinnedMeshRenderer MeshRenderer = Mesh.GetComponent<SkinnedMeshRenderer>();
            //Randomize all available shapekeys, regardless if they have it or not
            for (int blend = 0; blend < MeshRenderer.sharedMesh.blendShapeCount; blend++)
            {
                MeshRenderer.SetBlendShapeWeight(blend, Random.Range(0f, 100f));
            }
        }
        
        /*
        //Done after the for loop above because it screws up the check for the rest of the children
        for (int selectedChildren = 0; selectedChildren < listOfChildrenWithMesh.Count; selectedChildren++)
        {
            GameObject Mesh = modelGameObject.transform.GetChild(listOfChildrenWithMesh[selectedChildren]).gameObject;
            SkinnedMeshRenderer MeshRenderer = Mesh.GetComponent<SkinnedMeshRenderer>();
            //Remove from list of valid meshes if it doesnt have any
            if (MeshRenderer.sharedMesh.blendShapeCount == 0)
            {
                listOfChildrenWithMesh.RemoveAt(selectedChildren);
            }
        }*/

        //Get random integer, from 1 to the amount of valid child meshes. Then, disable every valid child mesh whose index does not match the integer.
        //In other words, disable every child except a random one

        //ONLY randomize if meshSelection is unset
        if (meshSelection == 0)
        {
            meshSelection = Random.Range(1, listOfChildrenWithMesh.Count);
        }

        for (int f = 1; f < listOfChildrenWithMesh.Count+1; f++)
        {
            if (f != meshSelection)
            {
                GameObject RandomizerCheckMesh = modelGameObject.transform.GetChild(listOfChildrenWithMesh[f-1]).gameObject;
                SkinnedMeshRenderer RandomizerCheckMeshRenderer = RandomizerCheckMesh.GetComponent<SkinnedMeshRenderer>();
                RandomizerCheckMeshRenderer.enabled = false;
            }
        }

       // FirstMesh = fbxPack.transform.GetChild(1).gameObject;
       // SecondMesh = fbxPack.transform.GetChild(3).gameObject;

      //  MeshRenderer1 = FirstMesh.GetComponent<SkinnedMeshRenderer>();
      //  MeshRenderer2 = SecondMesh.GetComponent<SkinnedMeshRenderer>();
      //  MeshSelector = Random.Range(0f, 2f);
      //  if (MeshSelector > 1)
      //  {
      //      MeshRenderer2.enabled = false;
      //      for (int i1 = 0; i1 < MeshRenderer1.sharedMesh.blendShapeCount; i1++)
      //      {
      //          MeshRenderer1.SetBlendShapeWeight(i1, Random.Range(0f, 100f));
      //      }
      //  }
     //   else
     //   {
     //       MeshRenderer1.enabled = false;
      //      for (int i2 = 0; i2 < MeshRenderer2.sharedMesh.blendShapeCount; i2++)
      //      {
      //          MeshRenderer2.SetBlendShapeWeight(i2, Random.Range(0f, 100f));
      //      }
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
