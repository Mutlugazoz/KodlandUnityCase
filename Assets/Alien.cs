using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Action<Alien> Died;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<PlayerController>().ChangeHealth(-50);
    }

    public void Die()
    {
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}
