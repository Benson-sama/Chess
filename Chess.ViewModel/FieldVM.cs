//-----------------------------------------------------------
// <copyright file="FieldVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Benjamin Bogner</author>
// <summary>Contains the FieldVM class.</summary>
//-----------------------------------------------------------

namespace Chess.ViewModel;

using System.ComponentModel;
using Chess.Model.Utility;
using CommunityToolkit.Mvvm.ComponentModel;

/// <summary>
/// Represents the <see cref="FieldVM"/> class.
/// </summary>
public partial class FieldVM : ObservableObject
{
    /// <summary>
    /// The value indicating whether the <see cref="FieldVM"/> is selected or not.
    /// </summary>
    [ObservableProperty]
    private bool _isSelected;

    /// <summary>
    /// The value indicating whether the <see cref="FieldVM"/> is highlighted or not.
    /// </summary>
    [ObservableProperty]
    private bool _isHighlighted;

    /// <summary>
    /// The value indicating whether the <see cref="FieldVM"/> is in danger.
    /// </summary>
    [ObservableProperty]
    private bool _isInDanger;

    /// <summary>
    /// Initialises a new instance of the <see cref="FieldVM"/> class.
    /// </summary>
    /// <param name="field">The field of the <see cref="FieldVM"/>.</param>
    public FieldVM(Field field) => Field = field;

    /// <summary>
    /// Gets the field of the <see cref="FieldVM"/>
    /// </summary>
    /// <value>The field of the <see cref="FieldVM"/></value>
    public Field Field { get; init; }
}