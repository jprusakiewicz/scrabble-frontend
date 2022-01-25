using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playersTile;
    private int tileSpacing = 13;
    List<string> l = new List<string>();


    public void NewLetters(List<string> letters)
    {
        l = letters;

        SetLetters(l);
    }

    public void SetLetters(List<string> letters)
    {
        RemoveLetters();
        if (letters == null)
            return;
        int numberOfLetters = letters.Count;

        var rt = playersTile.GetComponent<RectTransform>();
        var rect = rt.rect;
        var tileWidth = rect.width;
        var positionOffset = numberOfLetters * (tileWidth / 2 + tileSpacing + 9) / 2;
        var spawnPosition = rect.x - positionOffset;


        foreach (var letter in letters)
        {
            var newTile = Instantiate(playersTile, new Vector3(), rt.rotation);
            newTile.transform.SetParent(gameObject.transform);
            newTile.transform.localPosition = new Vector3(spawnPosition, rect.y);
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
            var tile = child.GetComponent<Tile>();
            tile.Deselect();
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