using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public GameObject cardPrefab;
    private StageData _stageData;
    Vector2 offset = new Vector2(-250f, -350f);

    void Awake()
    {
        _stageData = GameManager.Instance.StageData;
    }

    public void SetBoard()
    {

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cards/CardSheet");
        int[] cardIndices = new int[_stageData.cardsCount];
        for (int i = 0; i < cardIndices.Length; i++)
        {
            cardIndices[i] = i / 2;
        }
        cardIndices = cardIndices.OrderBy(x => Random.value).ToArray();
        
        for (int i = 0; i < _stageData.cardsCount; i++)
        {
            CreateCard(cardIndices[i], sprites[cardIndices[i]]);
        }
    }
    
    void CreateCard(int index, Sprite sprite)
    {
        GameObject cardObj = Instantiate(cardPrefab);
        cardObj.transform.SetParent(transform);
        Card card = cardObj.GetComponent<Card>();
        card.Setting(index, sprite);
        card.OnCardOpen += GameManager.Instance.OpenCard;
    }
}
