using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealDisplayScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Komponen ini pasti ada untuk model dengan shapekeys")]
    public SkinnedMeshRenderer skinnedMeshRenderer;

    
    void Start()
    {
        //nyari component
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            if (skinnedMeshRenderer == null)
            {
                Debug.LogError("SkinnedMeshRenderer not found on this object!");
            }
        }

        //semua dijadiin 1 untuk non animasi, karena 0 adalah shown, 1 adalah hidden
        for (int i=0; i<13; i++)
        {
            UpdateShapekey(i, 1);
        }
    }
    //0=noodle -> 0.0 to 0.1
    //1=bihun -> 0.0 to 0.1
    //2-4=ayam -> 0.0 to 0.05
    //5-8=Baso -> 0.0 to 0.1
    //9-11=sayur -> 0.0 to 0.1
    //12=kerupuk -> 0.0 to 0.1

    [ContextMenu("NoodleToggle")]
    public void NoodleToggle()
    {
        //kalau hidden (100), show tapi random dikit sizenya
        if(skinnedMeshRenderer.GetBlendShapeWeight(0) == 100)
        {
            //UpdateShapekey(0, Random.Range(0.0f, 0.1f));
            StartCoroutine(AnimateIngredientDrop(13, 0));
        } else //kalau nggak hidden, jadiin hidden
        {
            UpdateShapekey(0, 1);
        }
    }
    [ContextMenu("BihunToggle")]
    public void BihunToggle()
    {
        //kalau hidden (1), show tapi random dikit sizenya
        if (skinnedMeshRenderer.GetBlendShapeWeight(1) == 100)
        {
            //UpdateShapekey(1, Random.Range(0.0f, 0.1f));
            StartCoroutine(AnimateIngredientDrop(14, 1));
        }
        else //kalau nggak hidden, jadiin hidden
        {
            UpdateShapekey(1, 1);
        }
    }
    [ContextMenu("AyamAdd")]
    public void AyamAdd()
    {
        //ada 3 separate shapekey. ngetoggle sampai semuanya shown.
        int ayamAddTemp = 1;
        for (int i = 0; i < 3; i++)
        {
            if (skinnedMeshRenderer.GetBlendShapeWeight(i + 2) == 100 && ayamAddTemp == 1)
            {
                //UpdateShapekey(i+2, Random.Range(0.0f, 0.05f));
                StartCoroutine(AnimateIngredientDrop(15, i+2));
                ayamAddTemp = 0;
            }
        }
    }
    [ContextMenu("AyamClear")]
    public void AyamClear()
    {
        //set semuanya jadi 1
        for (int i = 0; i<3; i++)
        {
            UpdateShapekey(i + 2, 1);
        }
    }
    [ContextMenu("BasoAdd")]
    public void BasoAdd()
    {
        //ada 4 separate shapekey. ngetoggle sampai semuanya shown.
        int basoAddTemp = 1;
        for (int i = 0; i < 4; i++)
        {
            if (skinnedMeshRenderer.GetBlendShapeWeight(i + 5) == 100 && basoAddTemp == 1)
            {
                //UpdateShapekey(i + 5, Random.Range(0.0f, 0.1f));
                StartCoroutine(AnimateIngredientDrop(16, i+5));
                basoAddTemp = 0;
            }
        }
    }
    [ContextMenu("BasoClear")]
    public void BasoClear()
    {
        //set semuanya jadi 1
        for (int i = 0; i < 4; i++)
        {
            UpdateShapekey(i + 5, 1);
        }
    }
    [ContextMenu("SayurAdd")]
    public void SayurAdd()
    {
        //ada 3 separate shapekey. ngetoggle sampai semuanya shown.
        int sayurAddTemp = 1;
        for (int i = 0; i < 3; i++)
        {
            if (skinnedMeshRenderer.GetBlendShapeWeight(i + 9) == 100 && sayurAddTemp == 1)
            {
                //UpdateShapekey(i + 9, Random.Range(0.0f, 0.1f));
                StartCoroutine(AnimateIngredientDrop(17, i+9));
                sayurAddTemp = 0;
            }
        }
    }
    [ContextMenu("SayurClear")]
    public void SayurClear()
    {
        //set semuanya jadi 1
        for (int i = 0; i < 3; i++)
        {
            UpdateShapekey(i + 9, 1);
        }
    }
    [ContextMenu("KerupukToggle")]
    public void KerupukToggle()
    {
        //kalau hidden (100), show tapi random dikit sizenya
        if (skinnedMeshRenderer.GetBlendShapeWeight(12) == 100)
        {
            //UpdateShapekey(12, Random.Range(0.0f, 0.1f));
            StartCoroutine(AnimateIngredientDrop(18, 12));
        }
        else //kalau nggak hidden, jadiin hidden
        {
            UpdateShapekey(12, 1);
        }
    }

    //13 : Baso
    //14 : Bihun
    //15 : Ayam
    //16 : Baso
    //17 : Sayur
    //18 : Kerupuk
    private IEnumerator AnimateIngredientDrop(int dropIndex, int unhideIndex)
    {
        Debug.Log("coroutine start" + dropIndex + " and " +unhideIndex);
        float animationDuration = 1f; // Adjust as needed
        float elapsedTime = 0f;
        float unhideTiming = 0.9f;
        bool tempSwitch = true;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration; // Normalized time (0 to 1)
            float shapeKeyValue = Mathf.Lerp(0f, 100f, t); // Interpolate from 0 to 100

            skinnedMeshRenderer.SetBlendShapeWeight(dropIndex, shapeKeyValue);

            if (t > unhideTiming && tempSwitch) {
                UpdateShapekey(unhideIndex, Random.Range(0.0f, 0.1f));
                tempSwitch = false;
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset the shape key after the animation
        skinnedMeshRenderer.SetBlendShapeWeight(dropIndex, 0f);
        Debug.Log("coroutine end");
    }

    public void UpdateShapekey(int shapekeyIndex, float value)
    {
        if (skinnedMeshRenderer != null && shapekeyIndex >= 0 && shapekeyIndex < skinnedMeshRenderer.sharedMesh.blendShapeCount)
        {
            // Set the specified shape key to the desired value
            skinnedMeshRenderer.SetBlendShapeWeight(shapekeyIndex, value * 100f); // Unity expects values between 0 and 100
        }
        else
        {
            Debug.LogError("Invalid shapekey index or SkinnedMeshRenderer not found!");
        }
    }
}
