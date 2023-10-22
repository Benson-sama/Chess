//------------------------------------------------------------
// <copyright file="App.xaml.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the App class.</summary>
//------------------------------------------------------------

using System.Windows;
using Chess.Model;
using Chess.ViewModel;

namespace Chess.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Starts the chess game of the <see cref="App"/>.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The <see cref="StartupEventArgs"/> arguments.</param>
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ChessBoardParameters parameters = new(e.Args);

        if (!parameters.WasSuccessful)
        {
            MessageBox.Show("Required format: -size WIDTHxHEIGHT\n\nStarting application with default parameters.", "Invalid command line arguments!");
        }

        ChessGame game = new(parameters);
        ChessGameVM gameVM = new(game);
        MainWindow window = new() { ChessGameVM = gameVM };

        window.ChessBoardVM = window.ChessGameVM.ChessBoardVM;
        window.DataContext = window.ChessBoardVM;
        window.gameStatusBorder.DataContext = window.ChessGameVM.Status;
        window.beatenWhiteChessPieces.DataContext = window.ChessGameVM.BeatenWhiteChessPieces;
        window.beatenBlackChessPieces.DataContext = window.ChessGameVM.BeatenBlackChessPieces;
        window.moveList.DataContext = window.ChessGameVM.MoveList;

        window.Show();
    }
}
