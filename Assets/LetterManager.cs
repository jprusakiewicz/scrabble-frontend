using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Cell(char letter, int x, int y)
    {
        this.letter = letter;
        this.x = x;
        this.y = y;
    }

    public char letter;
    public int x;
    public int y;
}

public class LetterManager : MonoBehaviour
{
    private List<Cell> cells = new List<Cell>();
    char? choosenLetter;
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    public void AddLetter(char letter, int x, int y)
    {
        var c = new Cell(letter, x, y);
        cells.Add(c);
        Debug.Log(cells.Count);
    }


    public void AddOrReplaceLetter(char letter, int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        if (cell != null)
            cells.Remove(cell);

        AddLetter(letter, x, y);
    }

    public bool IsCellFilled(int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        return cell != null;
    }

    public char? GetLetter(int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        char? returnLetter = cell?.letter;
        return returnLetter;
    }

    public void RemoveLetter(char letter)
    {
        Cell cell = cells.Find(c => c.letter == letter);
        if (cell != null)
            cells.Remove(cell);
    }

    public void PutBackLetter(char letter)
    {
        // z planszy na kupke
        choosenLetter = null;
        player.AddLetter(letter.ToString());
    }
    public void TakeLetterFromPlayer(string letter)
    {
        // z kupki na plansze
        choosenLetter = null;
        player.ResetTilesOpacity();
        player.RemoveLetter(letter);
    }

    public char? GetChosenLetter()
    {
        var letterToReturn = choosenLetter;
        choosenLetter = null;
        return letterToReturn;
    }

    public bool IsChosenLetter()
    {
        return choosenLetter != null;
    }

    public void SetChosenLetter(char currentLetter)
    {
        choosenLetter = currentLetter;
        Debug.Log("choosen letter" + currentLetter);

    }
}