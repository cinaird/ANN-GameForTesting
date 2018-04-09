using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimplePong
{

    enum PlayerType
    {
        Human
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PlayerType PlayerOne = PlayerType.Human;
        private PlayerType PlayerTwo = PlayerType.Human;

        Direction _p1Direction = Direction.none;
        Direction _p2Direction = Direction.none;

        private Game _game;
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();


            _game = new Game(10, 50, 4, 8);
            PaddleOne.Width = _game.PaddleOne.XSizeFromCenter * 2;
            PaddleTwo.Width = _game.PaddleTwo.XSizeFromCenter * 2;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SimulateFrame();
            RenderFrame();
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (!_game.IsGameRunning)
                _game.StartGame();

            if (PlayerOne == PlayerType.Human)
            {
                if (e.Key == Key.Right)
                    _p1Direction = Direction.right;
                if (e.Key == Key.Left)
                    _p1Direction = Direction.left;
            }

            if (PlayerTwo == PlayerType.Human)
            {
                if (e.Key == Key.D)
                    _p2Direction = Direction.right;
                if (e.Key == Key.A)
                    _p2Direction = Direction.left;
            }
            base.OnPreviewKeyDown(e);
        }

        private void RenderFrame()
        {

            Canvas.SetLeft(PinBall, _game.PinBallX);
            Canvas.SetBottom(PinBall, _game.PinBallY);

            Canvas.SetLeft(PaddleOne, (_game.PaddleOne.XPos - _game.PaddleOne.XSizeFromCenter));
            Canvas.SetLeft(PaddleTwo, (_game.PaddleTwo.XPos - _game.PaddleTwo.XSizeFromCenter));

            xPosText.Text = "X: " + _game.PinBallX;
            yPosText.Text = "Y: " + _game.PinBallY;

            PlayerOneLifeText.Text = "P1: " + _game.PaddleOneLifes;
            PlayerTwoLifeText.Text = "P2: " + _game.PaddleTwoLifes;
        }

        private void SimulateFrame()
        {


            if (_game.IsGameRunning)
            {
                _game.LoadPaddleDirectionForNextFrame(_p1Direction, _p2Direction);
                _game.NextFrame();

            }
            _p1Direction = Direction.none;
            _p2Direction = Direction.none;
        }
    }
}
