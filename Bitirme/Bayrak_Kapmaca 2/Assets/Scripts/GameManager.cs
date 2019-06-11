using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    AlphaBeta ab = new AlphaBeta();
	public GameObject winPanel;
	public Text winText;
    float timer = 0;
    Board _board;
	void Start ()
    {
		winPanel.SetActive (false);
        _board = Board.Instance;
        _board.SetupBoard();
		DestroyAll();


	}

	void Update ()
    {
		
		if (Score.wscore == 2) {
			
				//Debug.Log("WINNER!");
				//UnityEditor.EditorApplication.isPlaying = false;
			winPanel.SetActive (true);
			winText.text = Score.playernamestr1 + "\n Winner";
			playerTurn = true;
		} 
		else if (Score.bscore == 2) 
		{
			winPanel.SetActive (true);
			winText.text = Score.playernamestr2 + "\n Winner";

		}
            
            
        
        if (!playerTurn && timer < 3)
        {
            timer += Time.deltaTime;
        }
        else if (!playerTurn && timer >= 3)
        {
            Move move = ab.GetMove();
            _DoAIMove(move);
            timer = 0;
        }
	}

    public bool playerTurn = true;



	public void BackMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

    void _DoAIMove(Move move)
    {
        Tile firstPosition = move.firstPosition;
        Tile secondPosition = move.secondPosition;

		/*if (secondPosition.CurrentPiece /*&& secondPosition.CurrentPiece.Type == Piece.pieceType.FLAG)
        {
            SwapPieces(move);
        }*/
        //else
		//{
		//if(firstPosition.CurrentPiece.Type!=Piece.pieceType.FLAG)
            SwapPieces(move);
        //}*/
    }

	void DestroyAll()
	{
		GameObject[] enemy = GameObject.FindGameObjectsWithTag ("Tas");
		for (int i = 0; i < enemy.Length; i++) 
		{
			GameObject.Destroy (enemy [i]);
		}
	}

    public void SwapPieces(Move move)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Highlight");
        foreach (GameObject o in objects)
        {
            Destroy(o);
        }

        Tile firstTile = move.firstPosition;
        Tile secondTile = move.secondPosition;

        firstTile.CurrentPiece.MovePiece(new Vector3(-move.secondPosition.Position.x, 0, move.secondPosition.Position.y));



		if (secondTile.CurrentPiece != null && firstTile.CurrentPiece.Type!=Piece.pieceType.BFLAG)
        {
			if (secondTile.CurrentPiece.Type == Piece.pieceType.BFLAG && secondTile.CurrentPiece.Player == Piece.playerColor.BLACK && playerTurn == true) 
			{
				Score.wscore++;
			} else if (secondTile.CurrentPiece.Type == Piece.pieceType.WFLAG && secondTile.CurrentPiece.Player==Piece.playerColor.WHITE && playerTurn == false) 
			{
				Score.bscore++;
			}


			Destroy(secondTile.CurrentPiece.gameObject);
           
        }
		if (firstTile.CurrentPiece.Type == Piece.pieceType.BFLAG) 
		{
			firstTile.CurrentPiece = null;
		}

		if (firstTile.CurrentPiece.position.y == 7 && firstTile.CurrentPiece.Type == Piece.pieceType.O) {
			Destroy(firstTile.CurrentPiece.gameObject);
		} else if (firstTile.CurrentPiece.position.y == 0 && firstTile.CurrentPiece.Type == Piece.pieceType.O) 
		{
			Destroy(firstTile.CurrentPiece.gameObject);
		}
            

        secondTile.CurrentPiece = move.pieceMoved;
        firstTile.CurrentPiece = null;
        secondTile.CurrentPiece.position = secondTile.Position;
        secondTile.CurrentPiece.HasMoved = true;

		if (secondTile.CurrentPiece.position.y == 7 && secondTile.CurrentPiece.Type == Piece.pieceType.O) {
			Object.Destroy (secondTile.CurrentPiece.gameObject, 1.0f);
		} else if (secondTile.CurrentPiece.position.y == 0 && secondTile.CurrentPiece.Type == Piece.pieceType.O) 
		{
			Object.Destroy (secondTile.CurrentPiece.gameObject, 1.0f);

		}

        playerTurn = !playerTurn;
    }
}
