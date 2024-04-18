using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int columns = 4;
    public static int rows = 2;
	public static int corrects = (columns * rows) / 2;
	private int[] locations=null;
	private string nombre;
    
    [SerializeField] private CartaScript startObject;
    [SerializeField] private Color[] colors;
    [SerializeField] private Transform Container;
	
	private CartaScript firstOpen;
	private CartaScript secondOpen;
	
	private int score = 0;
	private int attempts = 0;
	
	[SerializeField] private Text scoreText;
    [SerializeField] private Text attemptsText;
    
    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }
	
	private void InitializeColors()
    {
        colors = new Color[corrects];
        for (int i = 0; i < colors.Length; i++)
        {
            Color randomColor;
            bool colorIsValid;
            do
            {
                randomColor = new Color(Random.value, Random.value, Random.value);
                colorIsValid = IsColorValid(randomColor, i);
            } while (!colorIsValid);
            colors[i] = randomColor;
        }
    }

    private bool IsColorValid(Color newColor, int currentIndex)
    {
        for (int i = 0; i < currentIndex; i++)
        {
            float distance = ColorDistance(newColor, colors[i]);
            if (distance < 0.5f)
            {
                return false;
            }
        }
        return true;
    }

    private float ColorDistance(Color c1, Color c2)
    {
        float r = c1.r - c2.r;
        float g = c1.g - c2.g;
        float b = c1.b - c2.b;
        return Mathf.Sqrt(r * r + g * g + b * b);
    }


	void Start()
	{
		nombre = PlayerPrefs.GetString("Nombre", "");
		PlayerPrefs.SetString("Juego","Pair");
		string dimensiones = PlayerPrefs.GetString(nombre+"Dimensiones", "4x2");
		if (dimensiones == "4x2")
		{
			locations = new int[] {0, 0, 1, 1, 2, 2, 3, 3};
			locations = Randomiser(locations);
			columns = 4;
			rows = 2;
			corrects = (columns * rows) / 2;
		} 
		else if (dimensiones == "6x2")
		{
			locations = new int[] {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
			locations = Randomiser(locations);
			columns = 6;
			rows = 2;
			corrects = (columns * rows) / 2;
		}
		else if (dimensiones == "6x3")
		{
			locations = new int[] {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8};
			locations = Randomiser(locations);
			columns = 6;
			rows = 3;
			corrects = (columns * rows) / 2;
		}
        InitializeColors();
        Vector3 startPosition = startObject.transform.position;
        
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                CartaScript carta;
                if (i == 0 && j == 0)
                {
                    carta = startObject;
                }
                else
                {
                    carta = Instantiate(startObject, Container);
                }
                
                int index = (j * columns) + i;
                int id = locations[index];
                carta.CambiaSprite(id, colors[id]);
            }
        }
    }
	
	public bool canOpen
	{
		get { return secondOpen == null;}
	}
	
	public void imageOpened(CartaScript startObject)
	{
		if(firstOpen == null)
		{
			firstOpen = startObject;
		}
		else
		{
			secondOpen = startObject;
			StartCoroutine(CheckGuessed());
		}
	}
	
	private IEnumerator CheckGuessed()
	{
		if(firstOpen.ID == secondOpen.ID)
		{
			score++;
			PlayerPrefs.SetInt(nombre+"CorrectAnswersPair", score);
			scoreText.text = "Puntos: " +score;
			firstOpen.Acierto();
			secondOpen.Acierto();
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
			
			firstOpen.Fallo();
			secondOpen.Fallo();
		}
		
		attempts++;
		attemptsText.text = "Intentos: "+attempts;
		PlayerPrefs.SetInt(nombre+"AttemptsPair", attempts);
		
		firstOpen = null;
		secondOpen = null;
		
		if (score == corrects)
		{
			Invoke("End",1f);
		}
	}
	
	public void End()
	{
		SceneManager.LoadScene("FinJuego");
	}
	
	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Animacion");
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.Save();
    }
}