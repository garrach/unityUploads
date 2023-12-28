
using UnityEngine;
using UnityEngine.UI;

public class toggleCityMode : MonoBehaviour
{
    public void SelectCityBuildMode()
    {
        bool mode = GetComponent<Toggle>().isOn;
        PlayerPrefs.SetString("cityMode", mode.ToString());
    }
}
