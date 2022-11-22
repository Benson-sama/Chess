//-------------------------------------------------------------
// <copyright file="ChessGame.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGame class.</summary>
//-------------------------------------------------------------
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Chess.Model.ChessPieces;

    /// <summary>
    /// Represents the <see cref="ChessGame"/> class.
    /// </summary>
    public class ChessGame
    {
        /// <summary>
        /// The <see cref="RuleBook"/> of this <see cref="ChessGame"/>.
        /// It knows how each <see cref="ChessPiece"/> type is permitted to move.
        /// </summary>
        private readonly RuleBook ruleBook;

        /// <summary>
        /// The current <see cref="ChessGameStatus"/> of this <see cref="ChessGame"/>.
        /// </summary>
        private ChessGameStatus status;

        /// <summary>
        /// The value indicating whether the black <see cref="King"/> is in danger.
        /// </summary>
        private bool blackKingInDanger;

        /// <summary>
        /// The value indicating whether the white <see cref="King"/> is in danger.
        /// </summary>
        private bool whiteKingInDanger;

        /// <summary>
        /// The move list of this <see cref="ChessGame"/> containing successful past moves.
        /// </summary>
        private List<ChessMove> moveList;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessGame"/> class.
        /// </summary>
        /// <param name="parameters">The processed command line arguments of the <see cref="ChessGame"/>.</param>
        public ChessGame(ChessBoardParameters parameters)
        {
            this.MoveList = new List<ChessMove>();
            this.Board = new ChessBoard(parameters);
            this.ruleBook = new RuleBook(this.Board);
            this.FirstPlayer = new Player("Black", Direction.North);
            this.SecondPlayer = new Player("White", Direction.South);
            this.Status = ChessGameStatus.BlackActive;

            this.InitialiseChessPieces();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessGame"/> class.
        /// </summary>
        /// <param name="chessGameSave">The <see cref="ChessGameSave"/> that gets loaded.</param>
        public ChessGame(ChessGameSave chessGameSave)
        {
            ChessBoardParameters parameters = new ChessBoardParameters(chessGameSave.Width, chessGameSave.Height);
            this.MoveList = new List<ChessMove>();
            this.Board = new ChessBoard(parameters);
            this.ruleBook = new RuleBook(this.Board);
            this.FirstPlayer = new Player("Black", Direction.North);
            this.SecondPlayer = new Player("White", Direction.South);
            this.Status = ChessGameStatus.BlackActive;

            this.InitialiseChessPieces();
        }

        /// <summary>
        /// Gets fired when a <see cref="ChessPiece"/> was placed.
        /// </summary>
        public event EventHandler<ChessPiecePlacedEventArgs> ChessPiecePlaced;

        /// <summary>
        /// Gets fired when a <see cref="ChessPiece"/> was beaten.
        /// </summary>
        public event EventHandler<ChessPieceBeatenEventArgs> ChessPieceBeaten;

        /// <summary>
        /// Gets fired when a <see cref="ChessPiece"/> was moved.
        /// </summary>
        public event EventHandler<ChessPieceMovedEventArgs> ChessPieceMoved;

        /// <summary>
        /// Gets fired when the <see cref="ChessGameStatus"/> was updated.
        /// </summary>
        public event EventHandler<StatusUpdatedEventArgs> StatusUpdated;

        /// <summary>
        /// Gets fired when the danger status of the <see cref="King"/> was changed.
        /// Contains a value indicating whether the <see cref="King"/> is now in danger or not.
        /// </summary>
        public event EventHandler<KingInDangerEventArgs> KingInDanger;

        /// <summary>
        /// Gets the move list of this <see cref="ChessGame"/>.
        /// </summary>
        /// <value>The move list of this <see cref="ChessGame"/>.</value>
        public List<ChessMove> MoveList
        {
            get => this.moveList;

            private set
            {
                this.moveList = value ?? throw new ArgumentNullException(nameof(value), "The specified value cannot be null.");
            }
        }

        /// <summary>
        /// Gets the <see cref="ChessGameStatus"/> of this <see cref="ChessGame"/>.
        /// </summary>
        /// <value>The <see cref="ChessGameStatus"/> of this <see cref="ChessGame"/>.</value>
        public ChessGameStatus Status
        {
            get => this.status;

            private set
            {
                this.status = value;
                this.StatusUpdated?.Invoke(this, new StatusUpdatedEventArgs(value));
            }
        }

        /// <summary>
        /// Gets the first <see cref="Player"/> of this <see cref="ChessGame"/>.
        /// </summary>
        /// <value>The first <see cref="Player"/> of this <see cref="ChessGame"/>.</value>
        public Player FirstPlayer { get; private set; }

        /// <summary>
        /// Gets the second <see cref="Player"/> of this <see cref="ChessGame"/>.
        /// </summary>
        /// <value>The second <see cref="Player"/> of this <see cref="ChessGame"/>.</value>
        public Player SecondPlayer { get; private set; }

        /// <summary>
        /// Gets the <see cref="ChessBoard"/> of this <see cref="ChessGame"/>.
        /// </summary>
        /// <value>The <see cref="ChessBoard"/> of this <see cref="ChessGame"/>.</value>
        public ChessBoard Board { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ChessGame"/> is over.
        /// </summary>
        /// <value>The value indicating whether this <see cref="ChessGame"/> is over.</value>
        public bool IsGameOver { get; private set; }

        /// <summary>
        /// Saves the move list and the board size to ensure compatibility when loading.
        /// </summary>
        /// <param name="filePath">The file path for the save file.</param>
        /// <returns>The value indicating whether saving was successful.</returns>
        public bool Save(string filePath)
        {
            ChessGameSave chessGameSave = new ChessGameSave(this.moveList, this.Board.Width, this.Board.Height);
            XmlSerializer formatter = new XmlSerializer(typeof(ChessGameSave));

            if (File.Exists(filePath))
            {
                return false;
            }

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fs, chessGameSave);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to retrieves a <see cref="ChessGameSave"/> from the specified file path.
        /// </summary>
        /// <param name="filePath">The file path for retrieving the save file.</param>
        /// <param name="chessGameSave">The retrieved <see cref="ChessGameSave"/> file.</param>
        /// <returns>A value indicating whether the retrieving of the save file was successful.</returns>
        public bool RetrieveSave(string filePath, out ChessGameSave chessGameSave)
        {
            if (!File.Exists(filePath))
            {
                chessGameSave = null;
                return false;
            }

            XmlSerializer formatter = new XmlSerializer(typeof(ChessGameSave));

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    chessGameSave = (ChessGameSave)formatter.Deserialize(fs);
                    return true;
                }
            }
            catch (Exception)
            {
                chessGameSave = null;
                return false;
            }
        }

        /// <summary>
        /// Loads the desired <see cref="ChessGameSave"/> if it is compatible in board size.
        /// </summary>
        /// <param name="chessGameSave">The desired <see cref="ChessGameSave"/>.</param>
        public void Load(ChessGameSave chessGameSave)
        {
            // Check compatibility.
            if (chessGameSave.Height != this.Board.Height
                || chessGameSave.Width != this.Board.Width)
            {
                return;
            }

            // Replace this statement with a method that rewinds all turns to enable loading at any state,
            // without having to create a new chess game instance.
            if (this.MoveList.Any())
            {
                return;
            }

            chessGameSave.MoveList.ForEach(x => this.Move(x.From, x.To));
        }

        /// <summary>
        /// Rewinds the given <see cref="ChessMove"/> if it is the last in the move list.
        /// </summary>
        /// <param name="chessMove">The <see cref="ChessMove"/> to rewind.</param>
        public void Rewind(ChessMove chessMove)
        {
            if (this.MoveList.Last() != chessMove)
            {
                return;
            }

            this.IsGameOver = false;
            var movedChessPiece = this.Board.GetChessPiece(chessMove.To);

            if (movedChessPiece == null)
            {
                return;
            }

            this.Board.Move(movedChessPiece, chessMove.From);
            this.ChessPieceMoved?.Invoke(this, new ChessPieceMovedEventArgs(movedChessPiece, chessMove.From, true));
            this.MoveList.Remove(chessMove);

            if (chessMove.BeatenChessPiece != null)
            {
                this.Board.Place(chessMove.BeatenChessPiece, chessMove.To.Left, chessMove.To.Top);
                this.ChessPiecePlaced?.Invoke(this, new ChessPiecePlacedEventArgs(chessMove.BeatenChessPiece, chessMove.To));
            }

            if (movedChessPiece.Player.Colour.ToLower() == "black")
            {
                this.Status = ChessGameStatus.BlackActive;
            }
            else
            {
                this.Status = ChessGameStatus.WhiteActive;
            }

            this.StatusUpdated?.Invoke(this, new StatusUpdatedEventArgs(this.Status));
        }

        /// <summary>
        /// Moves from one <see cref="Field"/> to another <see cref="Field"/>.
        /// The move only gets executed if the source <see cref="Field"/> contains
        /// a <see cref="ChessPiece"/> that is permitted to move to the destination
        /// <see cref="Field"/>.
        /// </summary>
        /// <param name="from">The source <see cref="Field"/>.</param>
        /// <param name="to">The destination <see cref="Field"/>.</param>
        public void Move(Field from, Field to)
        {
            if (this.IsGameOver)
            {
                return;
            }

            ChessPiece chessPiece = this.Board.GetChessPiece(from);
            var legalMoves = this.GetLegalMoves(chessPiece);

            if (!this.IsChessPiecePermittedToMove(chessPiece) || !legalMoves.Contains(to))
            {
                return;
            }

            ChessPiece enemy = this.Board.GetChessPiece(to);

            if (enemy != null)
            {
                this.Board.Remove(enemy);
                this.ChessPieceBeaten?.Invoke(this, new ChessPieceBeatenEventArgs(enemy));
                this.MoveList.Add(new ChessMove(from, to, enemy));
            }
            else
            {
                this.MoveList.Add(new ChessMove(from, to));
            }

            this.Board.Move(chessPiece, to);
            this.ChessPieceMoved?.Invoke(this, new ChessPieceMovedEventArgs(chessPiece, to));
            
            this.SwitchActivePlayer();
            this.DetermineCurrentGameStatus();
        }

        /// <summary>
        /// Gets all legal moves that a given <see cref="ChessPiece"/> is permitted to perform.
        /// </summary>
        /// <param name="chessPiece">The <see cref="ChessPiece"/> which gets evaluated.</param>
        /// <returns>All legal moves that the given <see cref="ChessPiece"/> is permitted to perform.</returns>
        public IEnumerable<Field> GetLegalMoves(ChessPiece chessPiece)
        {
            chessPiece.Accept(this.ruleBook);
            return this.ruleBook.LegalMoves;
        }

        /// <summary>
        /// Determines the current <see cref="ChessGameStatus"/> of this <see cref="ChessGame"/>
        /// and fires the appropriate events if necessary.
        /// </summary>
        public void DetermineCurrentGameStatus()
        {
            // Get both kings.
            KingsRetriever kingsRetriever = new KingsRetriever();
            this.Board.OccupiedFields.Values.ToList().ForEach(x => x.Accept(kingsRetriever));

            King blackKing = kingsRetriever.Kings.FirstOrDefault(x => x.Player.Colour.ToLower() == "black");

            if (blackKing == null)
            {
                this.blackKingInDanger = false;
                this.IsGameOver = true;
                this.Status = ChessGameStatus.WhiteWon;
                this.StatusUpdated?.Invoke(this, new StatusUpdatedEventArgs(this.Status));
                return;
            }

            Field fieldOfBlackKing = this.Board.OccupiedFields.FirstOrDefault(x => x.Value == blackKing).Key;
            King whiteKing = kingsRetriever.Kings.FirstOrDefault(x => x.Player.Colour.ToLower() == "white");

            if (whiteKing == null)
            {
                this.whiteKingInDanger = false;
                this.IsGameOver = true;
                this.Status = ChessGameStatus.BlackWon;
                this.StatusUpdated?.Invoke(this, new StatusUpdatedEventArgs(this.Status));
                return;
            }

            Field fieldOfWhiteKing = this.Board.OccupiedFields.FirstOrDefault(x => x.Value == whiteKing).Key;

            this.EvaluateAllWhiteTurns(blackKing, fieldOfBlackKing);

            if (!this.IsGameOver)
            {
                this.EvaluateAllBlackTurns(whiteKing, fieldOfWhiteKing);
            }
        }

        /// <summary>
        /// Evaluates all turns from white <see cref="ChessPiece"/>s in order to determine the
        /// current <see cref="ChessGameStatus"/> of this <see cref="ChessGame"/>.
        /// </summary>
        /// <param name="blackKing">The black <see cref="King"/>.</param>
        /// <param name="fieldOfBlackKing">The <see cref="Field"/> of the black <see cref="King"/>.</param>
        private void EvaluateAllWhiteTurns(King blackKing, Field fieldOfBlackKing)
        {
            List<Field> allWhiteMoves = this.GetAllMovesFromPlayer("white");

            bool isBlackKingCurrentlyInDanger = allWhiteMoves.Contains(fieldOfBlackKing);
            if (isBlackKingCurrentlyInDanger != this.blackKingInDanger)
            {
                this.blackKingInDanger = isBlackKingCurrentlyInDanger;
                this.KingInDanger?.Invoke(this, new KingInDangerEventArgs(blackKing, isBlackKingCurrentlyInDanger));
            }

            var blackKingMoves = this.GetLegalMoves(blackKing);

            if (blackKingMoves.Intersect(allWhiteMoves).SequenceEqual(blackKingMoves))
            {
                if (this.blackKingInDanger && this.Status == ChessGameStatus.BlackActive)
                {
                    this.IsGameOver = true;
                    this.Status = ChessGameStatus.WhiteWon;
                    this.StatusUpdated?.Invoke(this, new StatusUpdatedEventArgs(this.Status));
                }
            }
        }

        /// <summary>
        /// Evaluates all turns from black <see cref="ChessPiece"/>s in order to determine the
        /// current <see cref="ChessGameStatus"/> of this <see cref="ChessGame"/>.
        /// </summary>
        /// <param name="whiteKing">The white <see cref="King"/>.</param>
        /// <param name="fieldOfWhiteKing">The <see cref="Field"/> of the white <see cref="King"/>.</param>
        private void EvaluateAllBlackTurns(King whiteKing, Field fieldOfWhiteKing)
        {
            var allBlackMoves = this.GetAllMovesFromPlayer("black");
            
            bool isWhiteKingCurrentlyInDanger = allBlackMoves.Contains(fieldOfWhiteKing);
            if (isWhiteKingCurrentlyInDanger != this.whiteKingInDanger)
            {
                this.whiteKingInDanger = isWhiteKingCurrentlyInDanger;
                this.KingInDanger?.Invoke(this, new KingInDangerEventArgs(whiteKing, isWhiteKingCurrentlyInDanger));
            }

            var whiteKingMoves = this.GetLegalMoves(whiteKing);

            if (whiteKingMoves.Intersect(allBlackMoves).SequenceEqual(whiteKingMoves))
            {
                if (this.whiteKingInDanger && this.Status == ChessGameStatus.WhiteActive)
                {
                    this.IsGameOver = true;
                    this.Status = ChessGameStatus.BlackWon;
                    this.StatusUpdated?.Invoke(this, new StatusUpdatedEventArgs(this.Status));
                }
            }
        }

        /// <summary>
        /// Gets all moves that a player with a given colour can perform.
        /// </summary>
        /// <param name="colour">The colour of the desired player.</param>
        /// <returns>The list containing all moves that can be performed.</returns>
        private List<Field> GetAllMovesFromPlayer(string colour)
        {
            var chessPieces = this.Board.OccupiedFields.Values.ToList().Where(x => x.Player.Colour.ToLower() == colour).ToList();
            var moves = new List<Field>();
            foreach (var chessPiece in chessPieces)
            {
                chessPiece.Accept(this.ruleBook);
                moves = moves.Concat(this.ruleBook.LegalMoves).ToList();
            }

            return moves;
        }

        /// <summary>
        /// Switches the active <see cref="Player"/> of this <see cref="ChessGame"/>.
        /// </summary>
        private void SwitchActivePlayer()
        {
            switch (this.Status)
            {
                case ChessGameStatus.BlackActive:
                    this.Status = ChessGameStatus.WhiteActive;
                    break;
                case ChessGameStatus.WhiteActive:
                    this.Status = ChessGameStatus.BlackActive;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Cannot switch active player if no player is currently active.");
            }
        }

        /// <summary>
        /// Evaluates whether the given <see cref="ChessPiece"/> is permitted to move.
        /// </summary>
        /// <param name="chessPiece">The <see cref="ChessPiece"/> to evaluate.</param>
        /// <returns>The value indicating whether the given <see cref="ChessPiece"/> is permitted to move.</returns>
        private bool IsChessPiecePermittedToMove(ChessPiece chessPiece)
        {
            if (chessPiece == null)
            {
                return false;
            }

            if (chessPiece.Player.Colour.ToLower() == "black" && this.Status == ChessGameStatus.WhiteActive)
            {
                return false;
            }

            if (chessPiece.Player.Colour.ToLower() == "white" && this.Status == ChessGameStatus.BlackActive)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Initialises new <see cref="ChessPiece"/> objects and places them on their corresponding <see cref="Field"/>.
        /// </summary>
        private void InitialiseChessPieces()
        {
            List<ChessPiece> firstSetOfChessPieces = this.GenerateSetOfChessPieces(this.FirstPlayer);

            // Place the first row.
            for (int i = 0; i < 8; i++)
            {
                this.Board.Place(firstSetOfChessPieces[i], i, 0);
            }

            // Place the second row.
            for (int i = 0; i < 8; i++)
            {
                this.Board.Place(firstSetOfChessPieces[i + 8], i, 1);
            }

            List<ChessPiece> secondSetOfChessPieces = this.GenerateSetOfChessPieces(this.SecondPlayer);

            // Place the first row.
            for (int i = 0; i < 8; i++)
            {
                this.Board.Place(secondSetOfChessPieces[i], i, this.Board.Height - 1);
            }

            // Place the second row.
            for (int i = 0; i < 8; i++)
            {
                this.Board.Place(secondSetOfChessPieces[i + 8], i, this.Board.Height - 2);
            }
        }

        /// <summary>
        /// Generates a set of <see cref="ChessPiece"/> objects.
        /// Containing 2 <see cref="Rook"/>s, 2 <see cref="Knight"/>s,
        /// 2 <see cref="Bishop"/>s, one <see cref="Queen"/> and one <see cref="King"/>.
        /// </summary>
        /// <param name="player">The player to whom generated <see cref="ChessPiece"/> objects should belong to.</param>
        /// <returns>Returns the list containing the generated <see cref="ChessPiece"/> objects.</returns>
        private List<ChessPiece> GenerateSetOfChessPieces(Player player)
        {
            List<ChessPiece> chessPieceSet = new List<ChessPiece>()
            {
                new Rook(player),
                new Knight(player),
                new Bishop(player),
                new Queen(player),
                new King(player),
                new Bishop(player),
                new Knight(player),
                new Rook(player),
            };

            for (int i = 0; i < 8; i++)
            {
                chessPieceSet.Add(new Pawn(player));
            }

            return chessPieceSet;
        }
    }
}
