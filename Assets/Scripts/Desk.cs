using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> accessories;

    [SerializeField]
    private List<Sprite> lightTheme;

    [SerializeField]
    private List<Sprite> darkTheme;

    [SerializeField]
    private SpriteRenderer accessory1, accessory2, keyboard, mouse;

    [SerializeField]
    private bool isDarkTheme = false;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseDesk();
    }

    private void RandomiseDesk()
    {
        accessory1.sprite = accessories[UnityEngine.Random.Range(0, accessories.Count - 1)];
        accessory2.sprite = accessories[UnityEngine.Random.Range(0, accessories.Count - 1)];

        if (isDarkTheme)
        {
            keyboard.sprite = darkTheme[0];
            mouse.sprite = darkTheme[1];
        }
        else
        {
            keyboard.sprite = lightTheme[0];
            mouse.sprite = lightTheme[1];
        }
    }
}
