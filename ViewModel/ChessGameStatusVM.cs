//---------------------------------------------------------------------
// <copyright file="ChessGameStatusVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the ChessGameStatusVM class.</summary>
//---------------------------------------------------------------------
namespace Chess.ViewModel
{
    using System.ComponentModel;
    using Chess.Model;

    /// <summary>
    /// Represents the <see cref="ChessGameStatusVM"/> class.
    /// </summary>
    public class ChessGameStatusVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="ChessGameStatus"/> of this <see cref="ChessGameStatusVM"/>.
        /// </summary>
        private ChessGameStatus status;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChessGameStatusVM"/> class.
        /// </summary>
        /// <param name="status">The <see cref="ChessGameStatus"/> for this <see cref="ChessGameStatusVM"/>.</param>
        public ChessGameStatusVM(ChessGameStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// The event that gets fired when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the <see cref="ChessGameStatus"/> of this <see cref="ChessGameStatusVM"/>.
        /// </summary>
        /// <value>The <see cref="ChessGameStatus"/> of this <see cref="ChessGameStatusVM"/>.</value>
        public ChessGameStatus Status
        {
            get => this.status;

            set
            {
                this.status = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Status)));
            }
        }
    }
}
