using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
public class PlayerMekanik : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int kecapCount = 0;
    private bool isPointerOver = false; 

    void Update()
    {
      
        if (Input.GetMouseButtonDown(1) && isPointerOver)
        {
            TambahKecap();
        }
    }

    
    void TambahKecap()
    {
        kecapCount++;
        Debug.Log("Kecap ditambahkan. Total kecap: " + kecapCount);
    }

  
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        Debug.Log("Pointer masuk ke objek.");
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false; 
        Debug.Log("Pointer keluar dari objek.");
    }
}


