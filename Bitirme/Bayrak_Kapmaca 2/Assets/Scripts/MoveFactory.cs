using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveFactory
{
    Board _board;
    List<Move> moves = new List<Move>();
    Dictionary<Piece.pieceType, System.Action> pieceToFunction = new Dictionary<Piece.pieceType, System.Action>();

    private Piece _piece;
    private Piece.pieceType _type;
    private Piece.playerColor _player;
    private Vector2 _position;

    public MoveFactory(Board board)
    {
        _board = board;
		pieceToFunction.Add(Piece.pieceType.O, _GetOMoves);
		pieceToFunction.Add(Piece.pieceType.X, _GetXMoves);
           }

    public List<Move> GetMoves(Piece piece, Vector2 position)
    {
        _piece = piece;
        _type = piece.Type;
        _player = piece.Player;
        _position = position;

        foreach(KeyValuePair<Piece.pieceType, System.Action> p in pieceToFunction)
        {
            if (_type == p.Key)
            {
                p.Value.Invoke();
            }
        }

        return moves;
    }

    void _GetOMoves()
    {
        if (_piece.Player == Piece.playerColor.BLACK)
        {
            int limit = _piece.HasMoved ? 2 : 2;
            _GenerateMove(limit, new Vector2(0, 1));
			_GenerateMove(limit, new Vector2(-1, 1));
			_GenerateMove(limit, new Vector2(1, 1));
        }
        else
        {
            int limit = _piece.HasMoved ? 2 : 2;
            _GenerateMove(limit, new Vector2(0, -1));
			_GenerateMove(limit, new Vector2(-1, -1));
			_GenerateMove(limit, new Vector2(1, -1));
        }
    }

  
    void _GetXMoves()
    {
       
		_GenerateMove(3, new Vector2(0, 1));
		_GenerateMove(3, new Vector2(0, -1));
		_GenerateMove(3, new Vector2(1, 0));
		_GenerateMove(3, new Vector2(-1, 0));
		_GenerateMove(3, new Vector2(1, 1));
		_GenerateMove(3, new Vector2(-1, -1));
		_GenerateMove(3, new Vector2(1, -1));
		_GenerateMove(3, new Vector2(-1, 1));
    }

    void _GenerateMove(int limit, Vector2 direction)
    {
        for (int i = 1; i < limit; i++)
        {
            Vector2 move = _position + direction * i;
            if (_IsOnBoard(move) && _ContainsPiece(_board.GetTileFromBoard(move)))
            {
				if (_IsEnemy(_board.GetTileFromBoard(move)))
                {
                    _CheckAndStoreMove(move);
                }
                break;
            }
            _CheckAndStoreMove(move);
        }
    }

    void _CheckAndStoreMove(Vector2 move)
    {
        if (_IsOnBoard(move) && (!_ContainsPiece(_board.GetTileFromBoard(move))  || _IsEnemy(_board.GetTileFromBoard(move))))
        {
            Move m = new Move();
            m.firstPosition = _board.GetTileFromBoard(_position);
            m.pieceMoved = _piece;
            m.secondPosition = _board.GetTileFromBoard(move);

            if (m.secondPosition != null)
                m.pieceKilled = m.secondPosition.CurrentPiece;



            moves.Add(m);
        }
    }

    bool _IsEnemy(Tile tile)
    {
        if (_player != tile.CurrentPiece.Player)
            return true;
        else
            return false;
    }

    bool _ContainsPiece(Tile tile)
    {
        if (!_IsOnBoard(tile.Position))
            return false;

        if (tile.CurrentPiece != null)
            return true;
        else
            return false;
    }

    bool _IsOnBoard(Vector2 point)
    {
        if (point.x >= 0 && point.y >= 0 && point.x < 7 && point.y < 8)
            return true;
        else
            return false;
    }
}
