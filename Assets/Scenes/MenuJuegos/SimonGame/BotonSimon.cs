using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class BotonSimon : MonoBehaviour
{
    public int ButtonIndex { get; set; }
    [SerializeField] GameManagerSimon gm;
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay = .25f;
	[SerializeField] private AudioClip sound = null;

    private AudioSource audioSource = null;
    Button button;
	bool Press;

    void Start()
    {
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        button = GetComponent<Button>();
        ResetButton();
		button.onClick.AddListener(Click);
    }

    void Click()
    {
        gm.PlayersPick(ButtonIndex);
        PressButton();
    }

    public void PressButton()
    {
        audioSource.PlayOneShot(sound);
        button.image.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    public void ResetButton()
    {
        button.image.color = defaultColor;
    }
}
