using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePiont : MonoBehaviour
{
    public GameObject tip;
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.C)&&tip.activeInHierarchy)
        {
            SaveLoadManager.Instance.Save();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player")
        {
            tip.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag=="Player")
        {
            tip.SetActive(false);
        }
    }
}
