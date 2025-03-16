using System.Collections.Generic;
using UnityEngine;

public class MazeGameManager : MonoBehaviour
{
    private const int xsize = 14;
    private const int ysize = 14;
    public int[,] openAdj = new int[xsize, ysize];
    public bool[,] board = new bool[xsize, ysize];

    public GameObject blockObj;
    public Transform gameObjLoc;
    public Transform goalObjLoc;
    public Transform playerObjLoc;

    private float xinput;
    private float yinput;

    public bool ended = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateMaze();
    }

    // Update is called once per frame
    void Update()
    {
        if (ended) return;
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
        if ((goalObjLoc.position - playerObjLoc.position).magnitude < 0.5f)
        {
            EndGame();
        }
    }

    private void FixedUpdate()
    {
        if (ended)
        {
            gameObjLoc.GetComponent<Rigidbody2D>().linearVelocity = new Vector2();
            return;
        }
        gameObjLoc.GetComponent<Rigidbody2D>().linearVelocity = -new Vector2(xinput, yinput) * 5f;
    }

    public void GenerateMaze()
    {
        for (int x = 0; x < xsize; x++) {
            for (int y = 0; y < ysize; y++)
            {
                board[x, y] = true;
                openAdj[x, y] = 0;
            }
        }
        OpenSpot(0, 0);
        OpenSpot(0, 1);
        OpenSpot(1, 0);
        OpenSpot(1, 1);
        int randomDec = Mathf.FloorToInt(Random.value * 4f);
        switch (randomDec)
        {
            case 0:
                Dig(2, 0);
                break;
            case 1:
                Dig(2, 1);
                break;
            case 2:
                Dig(1, 2);
                break;
            case 3:
                Dig(0, 2);
                break;
        }
        OpenSpot(xsize - 1, ysize - 1);
        OpenSpot(xsize - 1, ysize - 2);
        OpenSpot(xsize - 2, ysize - 1);
        OpenSpot(xsize - 2, ysize - 2);
        for (int x = 0; x < xsize; x++)
        {
            for (int y = 0; y < ysize; y++)
            {
                if (board[x, y])
                {
                    GameObject newBlock = Instantiate(blockObj, gameObjLoc);
                    newBlock.transform.localPosition = new Vector3(-0.5f + x, 0.5f - y);
                }
            }
        }
    }

    private void Dig(int x, int y)
    {
        OpenSpot(x, y);
        List<(int, int)> avail = new();
        if (CanOpenSpot(x - 1, y))
        {
            avail.Add((x - 1, y));
        }
        if (CanOpenSpot(x + 1, y))
        {
            avail.Add((x + 1, y));
        }
        if (CanOpenSpot(x, y - 1))
        {
            avail.Add((x, y - 1));
        }
        if (CanOpenSpot(x, y + 1))
        {
            avail.Add((x, y + 1));
        }
        if (avail.Count >= 1)
        {
            (int x1, int y1) = avail[(int) (Random.value * avail.Count)];
            Dig(x1, y1);
            Dig(x, y);
        }
    }

    private void OpenSpot(int x, int y)
    {
        if (!board[x, y]) return;
        board[x, y] = false;
        if (IsValidSpot(x - 1, y))
        {
            openAdj[x - 1, y]++;
        }
        if (IsValidSpot(x + 1, y))
        {
            openAdj[x + 1, y]++;
        }
        if (IsValidSpot(x, y - 1))
        {
            openAdj[x, y - 1]++;
        }
        if (IsValidSpot(x, y + 1))
        {
            openAdj[x, y + 1]++;
        }
    }

    private bool CanOpenSpot(int x, int y)
    {
        return IsValidSpot(x, y) && (openAdj[x, y] == 1) && board[x, y];
    }

    private bool IsValidSpot(int x, int y)
    {
        return (x >= 0 && x < xsize && y >= 0 && y < ysize);
    }

    public void EndGame()
    {
        GameManager.main.ExitWarning();
        Destroy(gameObject);
    }
}
