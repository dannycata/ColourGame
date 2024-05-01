using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevasPreguntas : MonoBehaviour
{
    public GameObject preguntaPrefab;
	string nombre;
    private QuizDB nuevaDB;

    private void Start()
    {
        nuevaDB = GetComponent<QuizDB>();
		nombre = PlayerPrefs.GetString("Nombre", "");
		string datosString = PlayerPrefs.GetString(nombre+"DatosPreguntas","");
		string datosIntCorrect = PlayerPrefs.GetString(nombre+"DatosPreguntasCorrecta","");
		string[] datos = datosString.Split(',');
		
		string[] datosStringCorrect = datosIntCorrect.Split(',');
		int[] datoscorrecta = new int[datosStringCorrect.Length];

		for (int i = 0; i < datosStringCorrect.Length; i++)
		{
			if (int.TryParse(datosStringCorrect[i], out int result))
			{
				datoscorrecta[i] = result;
			}
		}
		
		for (int i = 0; i < datos.Length; i++)
		{
			int indice = i;
			GameObject nuevaPreguntaObject = Instantiate(preguntaPrefab);
            Pregunta nuevaPregunta = nuevaPreguntaObject.GetComponent<Pregunta>();
			
			nuevaPregunta.color = Color.white;
			string[] stringcolores = datos[indice].Split('/');
			Color[] colores = new Color[stringcolores.Length]; // Debes inicializar el array con la longitud correcta

			for (int j = 0; j < stringcolores.Length; j++)
			{
				Color color;
				if (ColorUtility.TryParseHtmlString(stringcolores[j], out color))
				{
					colores[j] = color;
				}
			}
		
            nuevaPregunta.opciones = new List<Opcion>
			{
                new Opcion { text = "A", correct = false, color = colores[0] },
                new Opcion { text = "B", correct = false, color = colores[1] },
				new Opcion { text = "C", correct = false, color = colores[2] },
				new Opcion { text = "D", correct = false, color = colores[3] },
				new Opcion { text = "E", correct = false, color = colores[4] },
				new Opcion { text = "F", correct = false, color = colores[5] }
            };
			
			for (int j = 0; j < nuevaPregunta.opciones.Count; j++)
			{
				if (datoscorrecta[i] == j)
				{
					nuevaPregunta.opciones[j].correct = true;
					nuevaPregunta.color = nuevaPregunta.opciones[j].color;
					break;
				}
			}
			nuevaDB.AgregarPregunta(nuevaPregunta);
		}
    }
}

