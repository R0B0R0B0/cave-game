using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Custom/Enemy/Basic")]
public class Enemy : ScriptableObject
{
    [Header("Stats")]
    new public string name;
    public int health;
    public int damage;
    public int defense;
    [Header("Fight")]
    public GameObject enemyFight;
    public int[] attackOrder;
    public Vector2 playAreaSize;
}
