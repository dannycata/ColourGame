using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCards : MonoBehaviour
{
	[SerializeField] private float offsetV;
	[SerializeField] private float offsetH;
	[SerializeField] private float randomOffsetRange = 0.01f;
	[SerializeField] private GameObject[] cardsTop;
	[SerializeField] private GameObject[] cardsBottom;
	
	void OnEnable()
	{
		Shuffle();
	}
	
	public void Shuffle()
    {
        ShuffleCards(cardsTop);
        ShuffleCards(cardsBottom);

        ArrangeCards(cardsTop, offsetH);
        ArrangeCards(cardsBottom, -offsetH);
        ApplyRandomOffset(cardsTop);
        ApplyRandomOffset(cardsBottom);
    }

    void ShuffleCards(GameObject[] cards)
    {
        int numCards = cards.Length;

        for (int i = numCards - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    void ArrangeCards(GameObject[] cards, float offsetY)
    {
        int numCards = cards.Length;
        float cardWidth = cards[0].GetComponent<Renderer>().bounds.size.x + offsetV;
        float totalWidth = cardWidth * numCards;
        float firstCardX = transform.position.x - (totalWidth / 2) + (cardWidth / 2);

        for (int i = 0; i < numCards; i++)
        {
            float offsetX = firstCardX + i * cardWidth;
            Vector3 cardPosition = new Vector3(offsetX, transform.position.y + offsetY, transform.position.z);
            cards[i].transform.position = cardPosition;
        }
    }

    void ApplyRandomOffset(GameObject[] cards)
    {
        foreach (var card in cards)
        {
            Vector3 currentPosition = card.transform.position;
            currentPosition += new Vector3(Random.Range(-randomOffsetRange, randomOffsetRange), 0f, 0f);
            card.transform.position = currentPosition;
        }
    }
}