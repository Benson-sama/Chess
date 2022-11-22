//-----------------------------------------------------------
// <copyright file="FieldVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the FieldVM class.</summary>
//-----------------------------------------------------------
namespace Chess.ViewModel
{
    using System.ComponentModel;
    using Chess.Model;

    /// <summary>
    /// Represents the <see cref="FieldVM"/> class.
    /// </summary>
    public class FieldVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The value indicating whether the <see cref="FieldVM"/> is selected or not.
        /// </summary>
        private bool selected;

        /// <summary>
        /// The value indicating whether the <see cref="FieldVM"/> is highlighted or not.
        /// </summary>
        private bool highlighted;

        /// <summary>
        /// The value indicating whether the <see cref="FieldVM"/> is in danger.
        /// </summary>
        private bool danger;

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldVM"/> class.
        /// </summary>
        /// <param name="field">The field of the <see cref="FieldVM"/>.</param>
        public FieldVM(Field field)
        {
            this.Field = field;
        }

        /// <summary>
        /// The property changed event of the <see cref="FieldVm"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="FieldVM"/> is selected or not.
        /// </summary>
        /// <value>The value indicating whether the <see cref="FieldVM"/> is selected or not.</value>
        public bool Selected
        {
            get => this.selected;

            set
            {
                this.selected = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Selected)));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="FieldVM"/> is highlighted.
        /// </summary>
        /// <value>The value indicating whether the <see cref="FieldVM"/> is highlighted.</value>
        public bool Highlighted
        {
            get => this.highlighted;

            set
            {
                this.highlighted = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Highlighted)));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="FieldVM"/> is in danger.
        /// </summary>
        /// <value>The value indicating whether the <see cref="FieldVM"/> is in danger.</value>
        public bool Danger
        {
            get => this.danger;

            set
            {
                this.danger = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Danger)));
            }
        }

        /// <summary>
        /// Gets the field of the <see cref="FieldVM"/>
        /// </summary>
        /// <value>The field of the <see cref="FieldVM"/></value>
        public Field Field
        {
            get;
            private set;
        }
    }
}