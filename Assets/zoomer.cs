using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomer : MonoBehaviour
{
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject buttons;
    private buttons Buttons;
    private RectTransform rt;


    private const float zoomRatio = 1.5f;
    private float moveRatio;

    private const int minBoardSize = 445;
    private const int maxBoardSize = 1200;
    private const int maxBoardMove = 400;


    // Start is called before the first frame update
    void Start()
    {
        rt = board.GetComponent<RectTransform>();
        moveRatio = rt.rect.x / 5;
        Buttons = buttons.GetComponent<buttons>();
    }

    public void ZoomIn()
    {
        if (board.GetComponent<RectTransform>().rect.width > maxBoardSize)
            return;
        rt.sizeDelta = new Vector2(rt.rect.width * zoomRatio, rt.rect.height * zoomRatio);
        Buttons.SpawnButtons();

    }

    public void ZoomOut()
    {
        if (board.GetComponent<RectTransform>().rect.width < minBoardSize)
            return;
        rt.sizeDelta = new Vector2(rt.rect.width / zoomRatio, rt.rect.height / zoomRatio);

        MoveToCenter();
        Buttons.SpawnButtons();

    }

    public void MoveDown()
    {
        if (board.GetComponent<RectTransform>().rect.width < minBoardSize)
            return;
        if (rt.localPosition.y > maxBoardMove)
            return;
        rt.localPosition -= new Vector3(0, moveRatio);
        Buttons.SpawnButtons();
    }

    public void MoveUp()
    {
        if (board.GetComponent<RectTransform>().rect.width < minBoardSize)
            return;
        if (rt.localPosition.y < (-maxBoardMove))
            return;
        rt.localPosition += new Vector3(0, moveRatio);
        Buttons.SpawnButtons();

    }

    public void MoveLeft()
    {
        if (board.GetComponent<RectTransform>().rect.width < minBoardSize)
            return;
        if (rt.localPosition.x > maxBoardMove)
            return;
        rt.localPosition -= new Vector3(moveRatio, 0);
        Buttons.SpawnButtons();

    }

    public void MoveRight()
    {
        if (board.GetComponent<RectTransform>().rect.width < minBoardSize)
            return;
        if (rt.localPosition.x < (-maxBoardMove))
            return;
        rt.localPosition += new Vector3(moveRatio, 0);
        Buttons.SpawnButtons();

    }

    void MoveToCenter()
    {
        rt.localPosition = new Vector3(rt.localPosition.x / 3, rt.localPosition.y / 3);
    }
}