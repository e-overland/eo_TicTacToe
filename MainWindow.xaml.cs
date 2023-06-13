using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace eo_TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of the cells for the current game.
        /// </summary>
        private MarkType[] c_mtResults;

        /// <summary>
        /// True if it's Player 1's turn or Player 2's turn.
        /// </summary>
        private bool c_bolPlayer1Turn;

        /// <summary>
        /// Holds the status of the game whether it has finished or not.
        /// </summary>
        private bool c_bolGameStatus;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts the new game and sets the tiles to their default values.
        /// </summary>
        private void NewGame()
        {
            // Create an empty array for all of the new cells.
            c_mtResults = new MarkType[9];

            // Making sure that all of the cells are set to empty.
            for (int l_intI = 0; l_intI < c_mtResults.Length; l_intI++)
            {
                c_mtResults[l_intI] = MarkType.Empty;
            }

            // Player 1 always starts the game.
            c_bolPlayer1Turn = true;
            
            // Iterating over each button in the grid.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            // The game just started
            c_bolGameStatus = true;
        }

        /// <summary>
        /// Handles the click of a tile.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">The event of the click.</param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on click if the current one has ended.
            if (c_bolGameStatus == false)
            {
                NewGame();
                return;
            }

            // Gathering the clicked button.
            Button l_btnButton = (Button)sender;

            // Gather column and row from the clicked button.
            int l_intColumn = Grid.GetColumn(l_btnButton);
            int l_intRow = Grid.GetRow(l_btnButton);

            // Calculate the index for the clicked button.
            int l_intIndex = l_intColumn + (l_intRow * 3);

            // Ignore if the cell is already assigned.
            if (c_mtResults[l_intIndex] != MarkType.Empty)
            {
                return;
            }

            // Assign cell value based on the player's turn.
            c_mtResults[l_intIndex] = c_bolPlayer1Turn ? MarkType.Cross : MarkType.Circle;

            // Setting the actual text for the button.
            l_btnButton.Content = c_bolPlayer1Turn ? "X" : "O";

            // Change O's to red.
            if (!c_bolPlayer1Turn)
            {
                l_btnButton.Foreground = Brushes.Red;
            }

            // Alternate the turn based on it's value.
            c_bolPlayer1Turn ^= true;

            // Check for winner.
            CheckForWinner();
        }

        /// <summary>
        /// Checks to see if anyone has won the game.
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for horizontal wins.
            //
            // Row 1
            //
            if (c_mtResults[0] != MarkType.Empty && (c_mtResults[0] & c_mtResults[1] & c_mtResults[2]) == c_mtResults[0])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn0_0.Background = btn1_0.Background = btn2_0.Background = Brushes.Green;
            }

            //
            // Row 2
            //
            if (c_mtResults[3] != MarkType.Empty && (c_mtResults[3] & c_mtResults[4] & c_mtResults[5]) == c_mtResults[3])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn0_1.Background = btn1_1.Background = btn2_1.Background = Brushes.Green;
            }

            //
            // Row 3
            //
            if (c_mtResults[6] != MarkType.Empty && (c_mtResults[6] & c_mtResults[7] & c_mtResults[8]) == c_mtResults[6])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn0_2.Background = btn1_2.Background = btn2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            // Check for Vertical wins.
            //
            // Column 1
            //
            if (c_mtResults[0] != MarkType.Empty && (c_mtResults[0] & c_mtResults[3] & c_mtResults[6]) == c_mtResults[0])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn0_0.Background = btn0_1.Background = btn0_2.Background = Brushes.Green;
            }

            //
            // Column 2
            //
            if (c_mtResults[1] != MarkType.Empty && (c_mtResults[1] & c_mtResults[4] & c_mtResults[7]) == c_mtResults[1])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn1_0.Background = btn1_1.Background = btn1_2.Background = Brushes.Green;
            }

            //
            // Column 3
            //
            if (c_mtResults[2] != MarkType.Empty && (c_mtResults[2] & c_mtResults[5] & c_mtResults[8]) == c_mtResults[2])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn2_0.Background = btn2_1.Background = btn2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins
            // Check for Diagonal wins.
            //
            // Left to Right
            //
            if (c_mtResults[0] != MarkType.Empty && (c_mtResults[0] & c_mtResults[4] & c_mtResults[8]) == c_mtResults[0])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn0_0.Background = btn1_1.Background = btn2_2.Background = Brushes.Green;
            }

            //
            // Right to Left
            //
            if (c_mtResults[2] != MarkType.Empty && (c_mtResults[2] & c_mtResults[4] & c_mtResults[6]) == c_mtResults[2])
            {
                // Game is over.
                c_bolGameStatus = false;

                // Highlight the winning cells.
                btn2_0.Background = btn1_1.Background = btn0_2.Background = Brushes.Green;
            }
            #endregion

            #region Tie
            if (!c_mtResults.Any(results => results == MarkType.Empty))
            {
                // Game is over.
                c_bolGameStatus = false;

                // Change cells to red.
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.OrangeRed;
                });
            }
            #endregion
        }
    }
}
