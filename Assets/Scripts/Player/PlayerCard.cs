using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : MonoBehaviour, IActiveWhenPlayerIsDead
{
    PlayerInfos infos;
    public int nbPiece;

    public List<Card> deck = new List<Card>();
    public List<Card> inHand = new List<Card>();
    public List<Card> inPile = new List<Card>();
    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        nbPiece = infos.nbKill * _StaticPlayer.pieceByKill;
        StartCoroutine(PieceCdwCoroutine());
        GetDeck();
    }

    void Update()
    {
        
    }

    private IEnumerator PieceCdwCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_StaticPlayer.cdwPiece);
            nbPiece++;
        }
    }

    private void GetDeck()
    {
        for (int i = 0; i < _StaticPlayer.nbCardInDeck; i++)
            deck.Add(GF.SetScripts<Card>(_StaticCards.allCards[PlayerPrefs.GetInt("card" + i)].script, gameObject));
    }
}
