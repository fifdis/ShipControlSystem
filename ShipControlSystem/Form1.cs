using System;
using System.Windows.Forms;

namespace ShipControlSystem
{
    public partial class Form1 : Form
    {
        private readonly Data _data = new Data();
        private readonly Rules _rules;

        public Form1()
        {
            InitializeComponent();
            _rules = new Rules(_data);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double courseAngle = Convert.ToDouble(textBoxAngle.Text);
                double psi = Convert.ToDouble(textBoxPsi.Text);
                double theta = Convert.ToDouble(textBoxTeta.Text);

                _rules.SetData(courseAngle, psi, theta);

                textBoxType.Text = _rules.GetPitchingType();
                textBoxBayes.Text = $"{_data.Bayes * 100:F2}%";
                textBoxShortliff.Text = $"{_data.Shortliff * 100:F2}%";
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

