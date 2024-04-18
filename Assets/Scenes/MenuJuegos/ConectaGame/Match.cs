using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class Match : MonoBehaviour
{
    private LineRenderer lineRenderer;
	[SerializeField] private int matchId;
	private bool isDragging;
	private Vector3 endPoint;
	private Match match;
	private GameObject GM;
	private GameManagerMatch GMM;
	public static int aciertos=0;
	public int veces=0;
	private bool hasExecuted = false;
	
    private void Start()
    {
		GM = GameObject.Find("GameManager");
		GMM = GM.GetComponent<GameManagerMatch>();
        lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = 2;
		lineRenderer.sortingLayerName = "Foreground";
		lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
	}

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                lineRenderer.SetPosition(0, mousePosition);
            }
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            lineRenderer.SetPosition(1, mousePosition);
            endPoint = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            StartCoroutine(ProcessMouseButtonUp());
        }
    }

    private IEnumerator ProcessMouseButtonUp()
    {	
        yield return new WaitForSeconds(0.1f);
		
		veces++;
		Debug.Log("Entro "+veces);
        RaycastHit2D hit = Physics2D.Raycast(endPoint, Vector2.zero);
        if (hit.collider != null && hit.collider.TryGetComponent(out match) && matchId == match.Get_ID() && hit.collider.gameObject != gameObject && !hasExecuted)
        {
			Debug.Log("Dentro");
			hasExecuted = true;
            aciertos = aciertos + 1;
            this.enabled = false;
            match.enabled = false;
            Debug.Log("Aciertos: " + aciertos);
            if (aciertos == 4)
            {
                aciertos = 0;
                GMM.NextQuestion();
            }
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
        lineRenderer.positionCount = 2;
    }

    public int Get_ID()
    {
        return matchId;
    }

    public void eraseLine()
    {
		hasExecuted = false;
        this.enabled = true;
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}