using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCard : MonoBehaviour, IActiveWhenPlayerIsDead
{
    PlayerInfos infos;

    [HideInInspector] public int nbPiece { get; private set; }
    private List<CardMother> _deck = new List<CardMother>();
    private List<CardMother> _inHand = new List<CardMother>();
    private List<CardMother> _inPile = new List<CardMother>();

    public List<CardMother> deck { get { return _deck; } private set { _deck = value; } }
    public List<CardMother> inHand { get { return _inHand; } private set { _inHand = value; } }
    public List<CardMother> inPile { get { return _inPile; } private set { _inPile = value; } }

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
    }

    public void UseCard(int index)
    {
        if (_inHand[index].SO.cost > nbPiece)
            return;

        nbPiece -= _inHand[index].SO.cost;
        _inHand[index].Draw();

        _inPile.Add(_inHand[index]);
        _inHand.RemoveAt(index);
        _inHand.Add(_inPile[0]);
        _inPile.RemoveAt(0);
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
            _deck.Add(card);
        }
    }

    private void Melange()
    {
        List<CardMother> deck = this._deck.ToList();

        for (int i = 0; i < this._deck.Count; i++)
        {
            int index = Random.Range(0, deck.Count);
            if (i < _StaticPlayer.nbCardInHand)
                _inHand.Add(deck[index]);
            else
                _inPile.Add(deck[index]);

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
