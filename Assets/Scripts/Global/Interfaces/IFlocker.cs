using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlocker
{
    public void Flock(Transform player, Rigidbody2D rb);
}
