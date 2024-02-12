using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	Image timerBar;
	public float maxTime = 5f;
	float timeLeft;
	GameManager gameManager;
	
    public void Empieza()
    {
		Camera mainCamera = Camera.main;
		gameManager = mainCamera.GetComponent<GameManager>();
		timerBar = GetComponent<Image> ();
		ResetTimer();
		gameManager.NextQuestion();
		Update();
    }

    void Update()
    {
        if (timeLeft > 0){
			timeLeft -= Time.deltaTime;
			timerBar.fillAmount = timeLeft / maxTime;
		} else {
			Time.timeScale = 0;
			if (gameManager.NextQuestion()){
				ResetTimer();
			}
        }
    }

    public void ResetTimer()
    {
        timeLeft = maxTime;
        timerBar.fillAmount = 1f;
        Time.timeScale = 1;
    }
}
