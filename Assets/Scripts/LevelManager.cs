using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public static LevelManager instance;
    [HideInInspector] public Vector2 spawnPos;

    private void Awake()
    {
        instance = this;
        spawnPos = GameObject.Find("SpawnPos").transform.position;
        GameManager.instance.player.transform.position = spawnPos;
    }
}
