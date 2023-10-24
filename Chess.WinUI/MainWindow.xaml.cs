using Chess.ViewModel;
using Microsoft.UI.Xaml;

namespace Chess.WinUI;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private ChessGameVM _chessGameVM = new(new Model.ChessGame(new Model.ChessBoardParameters(8, 8)));

    public MainWindow()
    {
        InitializeComponent();
    }

    private void MyButton_Click(object sender, RoutedEventArgs e) => myButton.Content = "Clicked";
}
