using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundItem : MonoBehaviour
{
    UsableSO SO;
    public int nb;

    public void Init(UsableSO SO)
    {
        GetComponent<SpriteRenderer>().sprite = SO.sprite;
        nb = SO.nbRecolted;
        this.SO = SO;
    }

    private UsableSO getObject(int owManyYouTake)
    {
        nb -= owManyYouTake;
        return SO;
    }
}
