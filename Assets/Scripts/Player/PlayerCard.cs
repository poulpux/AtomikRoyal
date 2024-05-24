using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCard : MonoBehaviour, IActiveWhenPlayerIsDead
{
    PlayerInfos infos;

    [HideInInspector] public int nbPiece;
    [HideInInspector] public List<Card> deck = new List<Card>(), inHand = new List<Card>(), inPile = new List<Card>();
    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        nbPiece = infos.nbKill * _StaticPlayer.pieceByKill;
        StartCoroutine(PieceCdwCoroutine());
        GetDeck();
        Melange();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            UseCard(0);
    }

    public void UseCard(int index)
    {
        if (inHand[index].SO.cost > nbPiece)
            return;

        nbPiece -= inHand[index].SO.cost;
        inHand[index].Draw();

        inPile.Add(inHand[index]);
        inHand.RemoveAt(index);
        inHand.Add(inPile[0]);
        inPile.RemoveAt(0);
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
        {
            Card card = GF.SetScripts<Card>(_StaticCards.allCards[/*PlayerPrefs.GetInt("card" + i)*/i].script, gameObject);
            card.SO = _StaticCards.allCards[/*PlayerPrefs.GetInt("card" + i)*/i];
            deck.Add(card);
        }
    }

    private void Melange()
    {
        List<Card> deck = this.deck.ToList();

        for (int i = 0; i < this.deck.Count; i++)
        {
            int index = Random.Range(0, deck.Count);
            if (i < _StaticPlayer.nbCardInHand)
                inHand.Add(deck[index]);
            else
                inPile.Add(deck[index]);

            deck.RemoveAt(index);
        }
    }
}
