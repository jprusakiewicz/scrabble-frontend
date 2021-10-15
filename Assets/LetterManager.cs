using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Cell(char letter, int x, int y, bool isPlayers = false)
    {
        this.letter = letter;
        this.x = x;
        this.y = y;
        this.isPlayers = isPlayers;
    }

    public char letter;
    public int x;
    public int y;
    public bool isPlayers;
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

    public void ResetChosenLetter()
    {
        choosenLetter = null;
    }

    public void HandleServerData(List<BoardConfig> serverData)
    {
        cells = new List<Cell>();
        foreach (var cell in serverData)
        {
            AddLetter(cell.letter[0], Convert.ToInt32(cell.x), Convert.ToInt32(cell.y));
        }
    }

    public void AddLetter(char letter, int x, int y, bool isPlayers=false)
    {
        var c = new Cell(letter, x, y, isPlayers);
        cells.Add(c);
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
    
    public void PutBackLetter(int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        // z planszy na kupke
        choosenLetter = null;
        cells.Remove(cell);
        player.AddLetter(cell.letter.ToString());
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
    }

    public bool IsPlayers(int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        if (cell == null) return false;
        return cell.isPlayers;
    }

    public void setIsPlayers(int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        Cell new_cell = new Cell(cell.letter, x, y, true);
        cells.Remove(cell);
        cells.Add(new_cell);
    }
    public void setIsNotPlayers(int x, int y)
    {
        Cell cell = cells.Find(c => c.x == x && c.y == y);
        Cell new_cell = new Cell(cell.letter, x, y, false);
        cells.Remove(cell);
        cells.Add(new_cell);
    }
}