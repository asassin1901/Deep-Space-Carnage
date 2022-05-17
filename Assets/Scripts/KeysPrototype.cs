using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPrototype : MonoBehaviour
{
    string keyColor;
    Collider2D ourCollider;
    /*To Do:
    If we have access to multiple:
    1. set up several bools.
    2. Check wich key has been collected and enable appropriate interaction
    3. You can only open door #1 with key #1
    
    If we have access to one:
    1. check if key was picked up
    2. If key is picked up allow for interaction with door.*/

    // Start is called before the first frame update
    void Start()
    {
        ourCollider = gameObject.GetComponent<Collider2D>();
        keyColor = gameObject.name;
        Color thisColor = keyColor.ToColor();

        SpriteRenderer spritey = gameObject.GetComponent<SpriteRenderer>();
        spritey.color = thisColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

 public static class ColorExtensions
 {
     /// <summary>
     /// Convert string to Color (if defined as a static property of Color)
     /// </summary>
     /// <param name="color"></param>
     /// <returns></returns>
     public static Color ToColor(this string color)
     {
         return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
     }
 }
