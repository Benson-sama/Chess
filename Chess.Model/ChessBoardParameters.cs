//------------------------------------------------------------------------
// <copyright file="ChessBoardParameters.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessBoardParameters class.</summary>
//------------------------------------------------------------------------

namespace Chess.Model;

using System;

/// <summary>
/// Represents the <see cref="ChessBoardParameters"/> class.
/// </summary>
public record ChessBoardParameters
{
    /// <summary>
    /// The width of the chess board.
    /// </summary>
    private int _width;

    /// <summary>
    /// The height of the chess board.
    /// </summary>
    private int _height;

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessBoardParameters"/> class.
    /// </summary>
    /// <param name="args">The specified command line arguments.
    /// Format: e.g. "-size 8x8", where values can range from 8 to 26 including borders.</param>
    public ChessBoardParameters(string[] args)
    {
        if (args.Length < 2)
        {
            SetDefaultParameters();
            WasSuccessful = true;
            
            return;
        }

        if (args[0] == "-size")
        {
            if (!args[1].Contains('x'))
            {
                SetDefaultParameters();
                WasSuccessful = false;
                
                return;
            }

            var sizes = args[1].Split('x');

            try
            {
                Width = Convert.ToInt32(sizes[0]);
                Height = Convert.ToInt32(sizes[1]);
                WasSuccessful = true;
            }
            catch (Exception e)
            {
                SetDefaultParameters();
                WasSuccessful = false;
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="ChessBoardParameters"/> class.
    /// </summary>
    /// <param name="width">The width of the <see cref="ChessBoard"/>.</param>
    /// <param name="height">The height of the <see cref="ChessBoard"/>.</param>
    public ChessBoardParameters(int width, int height)
        => (Width, Height) = (width, height);

    /// <summary>
    /// Gets a value indicating whether or not the creation of the <see cref="ChessBoardParameters"/>
    /// by command line arguments was successful or not.
    /// </summary>
    /// <value>The value indicating whether or not the creation of the <see cref="ChessBoardParameters"/>
    /// by command line arguments was successful or not.</value>
    public bool WasSuccessful { get; init; }

    /// <summary>
    /// Gets the width of the chess board.
    /// </summary>
    /// <value>The width of the chess board.</value>
    public int Width
    {
        get => _width;

        private set
        {
            if (value is > 26 or < 8)
                throw new ArgumentOutOfRangeException(nameof(value), "The value must be within 8 and 26, including borders.");

            _width = value;
        }
    }

    /// <summary>
    /// Gets the height of the chess board.
    /// </summary>
    /// <value>The height of the chess board.</value>
    public int Height
    {
        get => _height;

        private set
        {
            if (value is > 26 or < 8)
                throw new ArgumentOutOfRangeException(nameof(value), "The value must be within 8 and 26, including borders.");

            _height = value;
        }
    }

    /// <summary>
    /// Sets the default height and width of the chess board.
    /// </summary>
    private void SetDefaultParameters()
        => (Height, Width) = (8, 8);
}
