//------------------------------------------------------------------------
// <copyright file="ChessBoardParameters.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessBoardParameters class.</summary>
//------------------------------------------------------------------------
namespace Chess.Model
{
    using System;

    /// <summary>
    /// Represents the <see cref="ChessBoardParameters"/> class.
    /// </summary>
    public class ChessBoardParameters
    {
        /// <summary>
        /// The width of the chess board.
        /// </summary>
        private int width;

        /// <summary>
        /// The height of the chess board.
        /// </summary>
        private int height;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessBoardParameters"/> class.
        /// </summary>
        /// <param name="args">The specified command line arguments.
        /// Format: e.g. "-size 8x8", where values can range from 8 to 26 including borders.</param>
        public ChessBoardParameters(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                this.SetDefaultParameters();
                this.WasSuccessful = true;
                return;
            }

            if (args[0] == "-size")
            {
                if (!args[1].Contains("x"))
                {
                    this.SetDefaultParameters();
                    this.WasSuccessful = false;
                    return;
                }

                var sizes = args[1].Split('x');

                try
                {
                    this.Width = Convert.ToInt32(sizes[0]);
                    this.Height = Convert.ToInt32(sizes[1]);
                    this.WasSuccessful = true;
                }
                catch (Exception e)
                {
                    this.SetDefaultParameters();
                    this.WasSuccessful = false;
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
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets a value indicating whether or not the creation of the <see cref="ChessBoardParameters"/>
        /// by command line arguments was successful or not.
        /// </summary>
        /// <value>The value indicating whether or not the creation of the <see cref="ChessBoardParameters"/>
        /// by command line arguments was successful or not.</value>
        public bool WasSuccessful
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width of the chess board.
        /// </summary>
        /// <value>The width of the chess board.</value>
        public int Width
        {
            get
            {
                return this.width;
            }

            private set
            {
                if (value > 26 || value < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The value must be within 8 and 26, including borders.");
                }

                this.width = value;
            }
        }

        /// <summary>
        /// Gets the height of the chess board.
        /// </summary>
        /// <value>The height of the chess board.</value>
        public int Height
        {
            get
            {
                return this.height;
            }

            private set
            {
                if (value > 26 || value < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The value must be within 8 and 26, including borders.");
                }

                this.height = value;
            }
        }

        /// <summary>
        /// Sets the default height and width of the chess board.
        /// </summary>
        private void SetDefaultParameters()
        {
            this.Height = 8;
            this.Width = 8;
        }
    }
}
