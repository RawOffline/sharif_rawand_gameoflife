using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header ("Cell")]
    public bool isAlive;
    public bool nextGenAlive;
    public int numNeighbors = 0;

    [Header ("Sprite")]
    SpriteRenderer spriteRenderer;
    public Sprite GameOfLife;
    public Sprite GameOfLife2;

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = isAlive;

        if (nextGenAlive)
            spriteRenderer.sprite = GameOfLife2;
        else
            spriteRenderer.sprite = GameOfLife;
    }
}
