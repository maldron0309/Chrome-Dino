using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private int frame;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
       Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        { 
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            _spriteRenderer.sprite = sprites[frame];
        }
        
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
