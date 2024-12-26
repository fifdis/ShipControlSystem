using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShipControlSystem
{
    public class Rules
    {
        private readonly Data _data;

        public Rules(Data data)
        {
            _data = data;
        }

        // Установка данных
        internal void SetData(double course, double psi, double theta)
        {
            _data.SetData(course, psi, theta);
        }

        // Определение типа качки
        internal string GetPitchingType()
        {
            if (IsResonance(_data.Theta, _data.PeriodWave / _data.PeriodT, 15, 0.8, 1.2))
                return "Основной резонанс бортовой качки";

            if (IsResonance(_data.Theta, _data.PeriodWave / _data.PeriodT, 15, 1.98, 2.08))
                return "Параметрический резонанс бортовой качки";

            if (IsResonance(_data.Psi, _data.PeriodWave / _data.PeriodP, 2.5, 0.8, 1.2))
                return "Основной резонанс килевой качки";

            return "Без качки";
        }
        private bool IsResonance(double amplitude, double periodRatio, double amplitudeThreshold, double minPeriod, double maxPeriod)
        {
            return amplitude > amplitudeThreshold && periodRatio > minPeriod && periodRatio < maxPeriod;
        }
    }
}
