using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    SpriteRenderer m_spriteRenderer;
    
    public abstract void OnDeath(); // Must enforce child classes to overwrite it.
}
