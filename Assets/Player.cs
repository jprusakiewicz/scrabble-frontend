using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playersTile;
    private int tileSpacing = 10;
    List<string> l = new List<string>();

    void Start()
    {
        l.Add("a");
        l.Add("b");
        l.Add("c");
        l.Add("d");
        SetLetters(l);
    }

    public void SetLetters(List<string> letters)
    {
        RemoveLetters();
        int numberOfLetters = letters.Count;
        var rt = playersTile.GetComponent<RectTransform>();
        var tileWidth = rt.rect.width;
        var positionOffset = numberOfLetters * (tileWidth / 2 + tileSpacing) / 2;
        var spawnPosition = rt.rect.x - positionOffset;

        foreach (var letter in letters)
        {
            var newTile = Instantiate(playersTile, new Vector3(), rt.rotation);
            newTile.transform.SetParent(gameObject.transform);
            newTile.transform.localPosition = new Vector3(spawnPosition, rt.rect.y);
            newTile.GetComponent<Tile>().SetLetter(letter);
            spawnPosition += (tileWidth + tileSpacing);
        }
    }


    public void RemoveLetters()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetTilesOpacity()
    {
        foreach (Transform child in transform)
        {
            var image = child.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }

    public void RemoveLetter(string currentLetter)
    {
        l.Remove(currentLetter);
        SetLetters(l);

    }

    public void AddLetter(string letter)
    {
        l.Add(letter);
        SetLetters(l);
    }
}