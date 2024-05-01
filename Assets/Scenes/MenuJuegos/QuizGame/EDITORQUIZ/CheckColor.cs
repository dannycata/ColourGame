using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckColor : MonoBehaviour
{
    public Toggle[] toggles; // Array de toggles
    public string playerPrefKey; // Clave para PlayerPrefs

    private void Start()
    {
        // Si hay un estado guardado para los toggles, lo restaura
        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            int savedIndex = PlayerPrefs.GetInt(playerPrefKey);
            if (savedIndex >= 0 && savedIndex < toggles.Length)
            {
                toggles[savedIndex].isOn = true;
            }
        }

        // Registra el evento OnValueChanged para cada toggle
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); });
        }
    }

    private void OnToggleValueChanged(Toggle changedToggle)
    {
        // Desactiva todos los toggles excepto el cambiado
        foreach (Toggle toggle in toggles)
        {
            if (toggle != changedToggle)
            {
                toggle.isOn = false;
            }
        }

        // Guarda el índice del toggle activado en PlayerPrefs
        int index = System.Array.IndexOf(toggles, changedToggle);
        PlayerPrefs.SetInt(playerPrefKey, index);
        PlayerPrefs.Save();
    }
}