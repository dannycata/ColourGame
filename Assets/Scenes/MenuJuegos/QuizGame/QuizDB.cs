using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDB : MonoBehaviour
{
    [SerializeField] private List<Pregunta> r_questionlist = null;
	
	private List<Pregunta> r_backup = null;
	private const string PlayerPrefsKey = "QuestionList";
	
	private void Awake()
	{
		r_backup = r_questionlist.ToList();
	}
	
	public Pregunta GetRandom(bool remove = true)
	{
		if(r_questionlist.Count == 0)
		{
			return null;
		}
		
		int index = Random.Range(0, r_questionlist.Count);
		
		if(!remove)
			return r_questionlist[index];
		
		Pregunta p = r_questionlist[index];
		r_questionlist.RemoveAt(index);
		
		return p;
	}
	
	public void AgregarPregunta(Pregunta nuevaPregunta)
    {
        r_questionlist.Add(nuevaPregunta);
		r_backup = r_questionlist.ToList();
    }
	
	public void SaveQuestionList() {
        // Convertir la lista a JSON
        string json = JsonUtility.ToJson(r_questionlist);
        
        // Guardar en PlayerPrefs
        PlayerPrefs.SetString(PlayerPrefsKey, json);
    }

    public void LoadQuestionList() {
        // Cargar desde PlayerPrefs
        string json = PlayerPrefs.GetString(PlayerPrefsKey);
        
        // Convertir JSON de vuelta a lista
        r_questionlist = JsonUtility.FromJson<List<Pregunta>>(json);
    }
	
	// Ejemplo de cómo añadir una pregunta y guardar la lista
    private void AddQuestion(Pregunta pregunta) {
        if (r_questionlist == null)
            r_questionlist = new List<Pregunta>();
        
        r_questionlist.Add(pregunta);
        
        SaveQuestionList();
    }

    // Ejemplo de cómo eliminar una pregunta y guardar la lista
    private void RemoveQuestion(Pregunta pregunta) {
        if (r_questionlist != null) {
            r_questionlist.Remove(pregunta);
            SaveQuestionList();
        }
    }

    // Método para limpiar la lista de preguntas guardadas
    private void ClearQuestionList() {
        PlayerPrefs.DeleteKey(PlayerPrefsKey);
        r_questionlist = null;
    }
}
