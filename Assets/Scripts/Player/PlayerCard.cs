using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCard : MonoBehaviour, IActiveWhenPlayerIsDead
{
    PlayerInfos infos;

    [HideInInspector] public int nbPiece { get; private set; }

    public List<CardMother> deck { get; private set; } = new List<CardMother>();
    public List<CardMother> inHand { get; private set; } = new List<CardMother>();
    public List<CardMother> inPile { get; private set; } = new List<CardMother>();

    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        nbPiece = infos.nbKill * _StaticPlayer.pieceByKill;
        StartCoroutine(PieceCdwCoroutine());
        GetDeck();
        Melange();
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
        CheckIfItFirstTime();
        for (int i = 0; i < _StaticPlayer.nbCardInDeck; i++)
        {
            CardMother card = GF.SetScripts<CardMother>(_StaticCards.allCards[PlayerPrefs.GetInt("card" + i)].script, gameObject);
            card.SO = _StaticCards.allCards[PlayerPrefs.GetInt("card" + i)];
            deck.Add(card);
        }
    }

    private void Melange()
    {
        List<CardMother> deck = this.deck.ToList();

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

    private void CheckIfItFirstTime()
    {
        if (PlayerPrefs.GetInt("card" + 1) == 0 && PlayerPrefs.GetInt("card" + 2) == 0)
        {
            for (int i = 1; i < _StaticPlayer.nbCardInDeck; i++)
                PlayerPrefs.SetInt("card" + 1, i);
        }
    }
}
