using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePong
{

    enum Direction
    {
        none,
        left,
        right
    }

    
    class Game
    {

        public double PinBallX { get { return _pinball.XPos; } }
        public double PinBallY { get { return _pinball.YPos; } }

        public bool IsGameRunning { get; private set; }

        public Game(int amountOfStartLifes, int paddleWidth, double ballspeed = 1, double paddleSpeed = 1)
        {
            PaddleOneLifes = PaddleTwoLifes = amountOfStartLifes;
            _ballSpeed = ballspeed;
            _paddleSpeed = paddleSpeed;
            if (paddleWidth > 200)
                paddleWidth = 200;


            var xSizeFromCenter = paddleWidth / 2;
            PaddleOne.XSizeFromCenter = xSizeFromCenter;
            PaddleTwo.XSizeFromCenter = xSizeFromCenter;
        }

        public int PaddleOneLifes { get; private set; }
        public int PaddleTwoLifes { get; private set; }
        
        private double _ballSpeed;
        private double _paddleSpeed = 1;

        Movable _pinball = new Movable();
        public Movable PaddleOne = new Movable();
        public Movable PaddleTwo = new Movable();


        Direction _paddleOneDirection;
        Direction _paddleTwoDirection;

        

        public void LoadPaddleDirectionForNextFrame(Direction p1 = Direction.none, Direction p2 = Direction.none)
        {
            _paddleOneDirection = p1;
            _paddleTwoDirection = p2;
        }

        public void StartGame()
        {
            RessetMovablesToStartState();

            IsGameRunning = true;
            _pinball.XSpeed = new Random().NextDouble();
            if (_pinball.XSpeed == 0 || _pinball.XSpeed == 1)
                _pinball.XSpeed = 0.5;

            _pinball.YSpeed = 1 - _pinball.XSpeed;

            _pinball.XSpeed = (new Random().NextDouble() > 0.5 ? _pinball.XSpeed : _pinball.XSpeed * -1) * _ballSpeed; 
            _pinball.YSpeed = (new Random().NextDouble() > 0.5 ? _pinball.YSpeed : _pinball.YSpeed * -1) * _ballSpeed;

        }

        public void NextFrame()
        {
            if (_paddleOneDirection != Direction.none)
                PaddleOne.XPos = _paddleOneDirection == Direction.left ? PaddleOne.XPos - _paddleSpeed : PaddleOne.XPos + _paddleSpeed;


            if (_paddleTwoDirection != Direction.none)
                PaddleTwo.XPos = _paddleTwoDirection == Direction.left ? PaddleTwo.XPos - _paddleSpeed : PaddleTwo.XPos + _paddleSpeed;

            HittController();

            _pinball.XPos = _pinball.NextXPos;
            _pinball.YPos = _pinball.NextYPos;
        }

        private void HittController()
        {

            
            if(_pinball.NextXPos <= 0)
            {
                _pinball.XSpeed = _pinball.XSpeed * -1; 
            }

            if (_pinball.NextXPos >= 200)
            {
                _pinball.XSpeed = _pinball.XSpeed * -1;
            }
            //One
            if ((PaddleOne.XPos + PaddleOne.XSizeFromCenter) > 200)
                PaddleOne.XPos = 200 - PaddleOne.XSizeFromCenter;

            if ((PaddleOne.XPos - PaddleOne.XSizeFromCenter) < 0)
                PaddleOne.XPos =  PaddleOne.XSizeFromCenter;

            //Two
            if ((PaddleTwo.XPos + PaddleTwo.XSizeFromCenter) > 200)
                PaddleTwo.XPos = 200 - PaddleTwo.XSizeFromCenter;

            if ((PaddleTwo.XPos - PaddleTwo.XSizeFromCenter) < 0)
                PaddleTwo.XPos = PaddleTwo.XSizeFromCenter;


            if (_pinball.NextYPos >= 390)
            {
                
                if(_pinball.XPos < PaddleOne.XPos + PaddleOne.XSizeFromCenter &&
                   _pinball.XPos > PaddleOne.XPos - PaddleOne.XSizeFromCenter)
                {

                    orka, något här? kanske?
                    var thestuff = _pinball.XPos - PaddleOne.XPos;
                    var newYDirection = Math.Abs(thestuff) /PaddleOne.XSizeFromCenter;
                    var yDirection = thestuff / Math.Abs(thestuff);

                    _pinball.YSpeed = newYDirection-1;
                    _pinball.XSpeed = yDirection * (1 - _pinball.YSpeed);
                    //kanske ändra vinkel här?
                    //_pinball.YSpeed = _pinball.YSpeed * -1;
                }

                var paddelOneLooses = _pinball.NextYPos >= 400;

                if (paddelOneLooses)
                {
                    PaddleOneLifes--;
                    EndTurn();
                }
            }
            else if(_pinball.NextYPos <= 10)
            {

                if (_pinball.XPos < PaddleTwo.XPos + PaddleTwo.XSizeFromCenter &&
                   _pinball.XPos > PaddleTwo.XPos - PaddleTwo.XSizeFromCenter)
                {
                    //kanske ändra vinkel här?
                    _pinball.YSpeed = _pinball.YSpeed * -1;
                }

                var paddelTwoLooses = _pinball.NextYPos <= 0;
                if (paddelTwoLooses)
                {
                    PaddleTwoLifes--;
                    EndTurn();
                }
            }
        }

        private void EndTurn()
        {
            RessetMovablesToStartState();
        }

        private void RessetMovablesToStartState()
        {
            IsGameRunning = false;
            _pinball.XPos = 100;
            _pinball.YPos = 200;
            _pinball.XSpeed = 0;
            _pinball.YSpeed = 0;

            PaddleOne.XPos = 100;
            PaddleOne.YPos = 10;

            PaddleTwo.XPos = 100;
            PaddleTwo.YPos = 390;

        }

        private void GameOwer()
        {
            IsGameRunning = false;

        }
        
    }
}
