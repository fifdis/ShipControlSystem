using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipControlSystem
{
    public class Data
    {
        // Параметры корабля
        private double _lengthShip;
        private double _widthShip;
        private double _draftShip;
        private double _metaHeight;
        private double _speedShipKnots;
        private double _waveLength;

        // Константы (вероятности)
        public const double PE = 0.66;       // Исходная вероятность P(E)
        public const double PHE = 0.85;     // Условная вероятность P(H|E)
        public const double PHNOTE = 0.04;  // Условная вероятность P(H|NOT E)
        public const double MDHE1 = 0.85;   // МД(H|E1)
        public const double MDHE2 = 0.92;   // МД(H|E2)

        public double LengthShip
        {
            get => _lengthShip;
            set
            {
                _lengthShip = value;
                UpdateWaveLength();
            }
        }

        public double WidthShip
        {
            get => _widthShip;
            set => _widthShip = value;
        }

        public double DraftShip
        {
            get => _draftShip;
            set => _draftShip = value;
        }

        public double MetaHeight
        {
            get => _metaHeight;
            set => _metaHeight = value;
        }

        public double SpeedShipKnots
        {
            get => _speedShipKnots;
            set
            {
                _speedShipKnots = value;
                SpeedShipMetersPerSecond = value * 0.51444; // Перевод из узлов в м/с
            }
        }

        public double SpeedShipMetersPerSecond { get; private set; } // Скорость в м/с

        public double WaveLength => _waveLength;

        public double SpeedWave => 1.25 * Math.Sqrt(_waveLength); // Скорость волны
        public double PeriodT => 0.8 * _widthShip / Math.Sqrt(_metaHeight); // Период бортовой качки
        public double PeriodP => 2.5 * Math.Sqrt(_draftShip); // Период килевой качки

        public double PeriodWave { get; private set; } // Период волны
        public double Theta { get; private set; } // Амплитуда бортовой качки
        public double Psi { get; private set; } // Амплитуда килевой качки
        public double CourseAngle { get; private set; } // Курсовой угол

        public Data()
        {
            _lengthShip = 90.0;
            _widthShip = 16.0;
            _draftShip = 5.0;
            _metaHeight = 0.9;
            _speedShipKnots = 16.0;
            SpeedShipMetersPerSecond = _speedShipKnots * 0.51444;
            UpdateWaveLength();
        }

        // Метод обновления длины волны
        private void UpdateWaveLength()
        {
            _waveLength = 1.2 * _lengthShip;
        }

        public void SetData(double courseAngle, double psi, double theta)
        {
            CourseAngle = courseAngle;
            Psi = psi;
            Theta = theta;

            // Вычисление периода волны
            PeriodWave = _waveLength / (SpeedWave - SpeedShipMetersPerSecond * Math.Cos(Calculations.ConvertToRadian(courseAngle)));
        }

        // Формула Байеса
        public double Bayes => PHE * PE + PHNOTE * (1 - PE) + (1 - PHE) * PE + PHE * (1 - PE);

        // Формула Шортлиффа
        public double Shortliff => MDHE1 + MDHE2 * (1 - MDHE1);
    }
}


