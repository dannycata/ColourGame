using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoresSimon : MonoBehaviour
{
    private Button button;
    private Image image;
    private string nombre;
    [SerializeField] private string variableColor = "VerdeSimon";
    [SerializeField] private Color defaultColor = Color.green;
    private string colorElegido;

    void Start()
    {
        button = GetComponent<Button>();
        Transform childTransform = transform.Find("Image");
        image = childTransform.GetComponent<Image>();
        nombre = PlayerPrefs.GetString("Nombre", "");
        Debug.Log("Color guardado: " + PlayerPrefs.GetString(nombre + variableColor));
        Debug.Log("Nombre: " + nombre);
        Debug.Log("Nombre de color: " + variableColor);
        Color savedColor = PlayerPrefs.HasKey(nombre + variableColor) ? HexToColor(PlayerPrefs.GetString(nombre + variableColor)) : defaultColor;
        SetColor(savedColor);
        button.onClick.AddListener(OpenColorPicker);
    }

    void OpenColorPicker()
    {
        ColorPicker.Create(image.color, "Elige un color!", SetColor, ColorFinished, true);
    }

    private void SetColor(Color currentColor)
    {
        image.color = currentColor;
        colorElegido = ColorToHex(currentColor);
        Debug.Log("Color: " + colorElegido);
        PlayerPrefs.SetString(nombre + variableColor, colorElegido);
    }

    private string ColorToHex(Color color)
    {
        Color32 color32 = color;
        return "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2") + color32.a.ToString("X2");
    }

    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogWarning("Invalid hexadecimal color: " + hex);
            return Color.white;
        }
    }

    private void ColorFinished(Color finishedColor)
    {
        Debug.Log("You chose the color " + ColorUtility.ToHtmlStringRGBA(finishedColor));
    }
}

