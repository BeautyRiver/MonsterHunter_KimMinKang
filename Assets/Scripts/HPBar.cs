using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
   

    private void Awake()
    {
        
    }

    
    

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
