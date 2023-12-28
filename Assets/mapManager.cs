using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public GameObject mapOsm;
    public GameObject map3D;
    private void Awake()
    {
        string cityMode = PlayerPrefs.GetString("cityMode");
        if (cityMode.Equals("True"))
        {
            map3D.SetActive(true);
        }else if (cityMode.Equals("False")) {

            mapOsm.SetActive(true);

        }else { mapOsm.SetActive(false);}
    }
}
