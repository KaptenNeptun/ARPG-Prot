using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHealthPoints = 100f;
    float currentHealthPoints = 100f;
	// Use this for initialization
    public float healthAsPercentage
    {
        get { return (currentHealthPoints / maxHealthPoints); }
    }
}
