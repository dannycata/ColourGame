using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    private float tiempo;
    private InputField inputFieldTiempo;
    private Text t_warning;

    void Start()
    {
        inputFieldTiempo = GameObject.Find("InputFieldTiempo").GetComponent<InputField>();
		inputFieldTiempo.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat("VariableTiempo", 5f).ToString();
        t_warning = GameObject.Find("t_warning").GetComponent<Text>();
        t_warning.enabled = false;

        inputFieldTiempo.onEndEdit.AddListener(Read);
    }

    void Read(string inputValue)
    {
        if (float.TryParse(inputValue, out float result))
        {
            tiempo = result;
            t_warning.enabled = false;
            PlayerPrefs.DeleteKey("VariableTiempo");
            PlayerPrefs.SetFloat("VariableTiempo", tiempo);
        }
        else
        {
            t_warning.enabled = true;
        }
    }
}
