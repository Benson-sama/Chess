//-------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the MainWindow class.</summary>
//-------------------------------------------------------------------

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Model;
using Chess.Model.Utility;
using Chess.ViewModel;
using Microsoft.Win32;

namespace Chess.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initialises a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the <see cref="ViewModel.ChessGameVM"/>.
    /// </summary>
    /// <value>The <see cref="ViewModel.ChessGameVM"/>.</value>
    public ChessGameVM ChessGameVM { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="ViewModel.ChessBoardVM"/>.
    /// </summary>
    /// <value>The <see cref="ViewModel.ChessBoardVM"/>.</value>
    public ChessBoardVM ChessBoardVM { get; set; }

    /// <summary>
    /// Used to select the corresponding <see cref="Field"/>.
    /// </summary>
    /// <param name="sender">The <see cref="Canvas"/> of the user interface.</param>
    /// <param name="e">Arguments of the event.</param>
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Point point = e.GetPosition((Canvas)sender);
        double left = Math.Floor(point.X);
        double top = Math.Floor(Convert.ToDouble(this.ChessBoardVM.Height) - point.Y);

        this.ChessGameVM.SelectField(left, top);
    }

    /// <summary>
    /// Creates a new chess game and sets the data context of this window.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void NewGameButton_Click(object sender, RoutedEventArgs e)
    {
        // A new game can be created at any time. Therefore no fail early here.
        /*if (!this.ChessGameVM.MoveList.Any())
        {
            return;
        }*/

        // Can be extended by a logic to retrieve a new board size from the user.
        ChessBoardParameters parameters = new(this.ChessBoardVM.Width, this.ChessBoardVM.Height);
        ChessGame game = new(parameters);
        this.Initialise(game);
    }

    /// <summary>
    /// Rewinds the chess game to the state before this clicked move entry,
    /// thereby removing all <see cref="ChessMove"/> entries after it.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void RewindButton_Click(object sender, RoutedEventArgs e)
    {
        Button reportingButton = e.Source as Button;
        ChessMove selectedMove = reportingButton.DataContext as ChessMove;

        this.ChessGameVM.Rewind(selectedMove);
    }

    /// <summary>
    /// Saves the current game state, requires the user to enter a new file.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Title = "Save Game Dialog",
            OverwritePrompt = false
        };

        // Stop if the user cancels the dialog.
        if (saveFileDialog.ShowDialog() == false)
        {
            return;
        }

        if (File.Exists(saveFileDialog.FileName))
        {
            MessageBox.Show("The specified file must not exist, as overwriting is not supported for data protection reasons.");
            return;
        }

        if (this.ChessGameVM.ChessGame.Save(saveFileDialog.FileName))
        {
            MessageBox.Show("Successfully saved the game.");
        }
        else
        {
            MessageBox.Show("An error occured while saving the game.");
        }
    }

    /// <summary>
    /// Loads the current game state.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Title = "Load Game Save File"
        };

        // Stop if the user cancels the dialog.
        if (openFileDialog.ShowDialog() == false)
        {
            return;
        }

        bool wasSuccessful = ChessGame.RetrieveSave(openFileDialog.FileName, out ChessGameSave chessGameSave);

        if (!wasSuccessful)
        {
            MessageBox.Show("Could not retrieve the chess game save from the specified file.");
            return;
        }

        ChessGame chessGame = new(chessGameSave);
        this.Initialise(chessGame);
        chessGame.Load(chessGameSave);
    }

    /// <summary>
    /// Sets up the <see cref="ChessBoardVM"/> and <see cref="ChessGameVM"/>
    /// including the data context.
    /// </summary>
    /// <param name="chessGame">The <see cref="ChessGame"/> that gets set up.</param>
    private void Initialise(ChessGame chessGame)
    {
        this.ChessGameVM = new ChessGameVM(chessGame);
        this.ChessBoardVM = this.ChessGameVM.ChessBoardVM;
        this.DataContext = this.ChessBoardVM;
        this.gameStatusBorder.DataContext = this.ChessGameVM.Status;
        this.beatenWhiteChessPieces.DataContext = this.ChessGameVM.BeatenWhiteChessPieces;
        this.beatenBlackChessPieces.DataContext = this.ChessGameVM.BeatenBlackChessPieces;
        this.moveList.DataContext = this.ChessGameVM.MoveList;
    }
}
