using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class selectCity : MonoBehaviour
{
    public List<string> cityList = new List<string>();
    public void setACity()
    {
        int index = GetComponent<TMP_Dropdown>().value;
        PlayerPrefs.SetString("city", cityList[index]);
        string city = PlayerPrefs.GetString("city");
        Debug.Log(city);

    }
    public void PlaytheGame(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }
}
