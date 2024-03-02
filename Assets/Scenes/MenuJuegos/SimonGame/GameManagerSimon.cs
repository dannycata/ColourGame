using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerSimon : MonoBehaviour
{
    [SerializeField] BotonSimon[] button;
    [SerializeField] List<int> colorOrder;
    [SerializeField] int pickNumber = 0;
	[SerializeField] Text texto;
	[SerializeField] GameObject panel;
	[SerializeField] private string nombreDeLaNuevaEscena = "FinJuego";
	private int nsecuencias;
	private float pickDelay;
	private int aciertos=0;
	public Coroutine rutina;
	int score=0;
	

    void Start()
    {
		PlayerPrefs.SetString("Juego", "Simon");
		pickDelay = PlayerPrefs.GetFloat("VelocidadSimon", 0.4f);
		nsecuencias = PlayerPrefs.GetInt("NSecuencias", 5);
        ResetGame();
        SetButtonIndex();
        StartGame();
    }
	
	void StartGame()
	{
		texto.text = "Memoriza";
		texto.color = Color.green;
		rutina = StartCoroutine("PlayGame");
	}

    void SetButtonIndex()
    {
        for(int cnt = 0; cnt < button.Length; cnt++)
            button[cnt].ButtonIndex = cnt;
    }

    IEnumerator PlayGame()
    {
        pickNumber = 0;
        yield return new WaitForSeconds(pickDelay);

        foreach(int colorIndex in colorOrder)
        {
            button[colorIndex].PressButton();
            yield return new WaitForSeconds(pickDelay);
        }

        PickRandomColor();
		Invoke("Panel",1f);
    }
	
	void Panel()
	{
		texto.text = "Tu turno";
		texto.color = Color.white;
		panel.SetActive(false);
	}

    void PickRandomColor()
    {
        int rnd = Random.Range(0, button.Length);
        button[rnd].PressButton();
        colorOrder.Add(rnd);
    }

    public void PlayersPick(int pick)
    {
        if(pick == colorOrder[pickNumber])
        {
            Debug.Log("Acierto");

            pickNumber++;
            if(pickNumber == colorOrder.Count)
            {
				score++;
				if (pickNumber == nsecuencias)
				{
					PlayerPrefs.SetInt("CorrectAnswersSimon", score);
					SceneManager.LoadScene(nombreDeLaNuevaEscena);
				}
				StartGame();
				panel.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Fallo");
			if (rutina != null)
			{
				StopCoroutine(rutina);
				rutina = null;
			}
			PlayerPrefs.SetInt("CorrectAnswersSimon", score);
			if(pickNumber == nsecuencias)
			{
				SceneManager.LoadScene(nombreDeLaNuevaEscena);
			}
            SceneManager.LoadScene(nombreDeLaNuevaEscena);
        }
    }

    void ResetGame()
    {
        colorOrder.Clear();
    }
}
