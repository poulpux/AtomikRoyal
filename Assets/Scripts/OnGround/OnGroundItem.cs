using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundItem : MonoBehaviour
{
    UsableSOMother SO;
    public int nb;

    public void Init(UsableSOMother SO)
    {
        GetComponent<SpriteRenderer>().sprite = SO.sprite;
        nb = SO.nbRecolted;
        this.SO = SO;
    }

    private UsableSOMother getObject(int owManyYouTake)
    {
        nb -= owManyYouTake;
        return SO;
    }
}
