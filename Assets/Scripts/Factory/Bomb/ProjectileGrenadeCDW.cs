using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenadeCDW : ProjectileBombMother
{
    private void Start()
    {
        Destroy(gameObject, cdw);
    }
}
