using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int scoreWorth;
    private AudioSource aS;

    private void Awake() =>
        aS = GetComponent<AudioSource>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            Collect();
    }

    private void Collect()
    {
        GameManager.instance.score += scoreWorth;
        GameManager.instance.ScoreChange();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        aS.Play();
        Invoke("CleanUp", 1f);
    }

    private void CleanUp() =>
        Destroy(gameObject);
}
