using System;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class SeekBarView : UserControl
    {

        public event Action OnValueChange;

        public SeekBarView()
        {
            InitializeComponent();

            colorBar.Value = 0;
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                UpdateValue();
            }
        }

        public int Value
        {
            get { return colorBar.Value; }
            set { colorBar.Value = value; UpdateValue(); }
        }

        public int MinValue
        {
            get { return colorBar.Minimum; }
            set { colorBar.Minimum = value; }
        }

        public int MaxValue
        {
            get { return colorBar.Maximum; }
            set { colorBar.Maximum = value; }
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            valueTbx.Text = $"{Value}{Unit}";

            OnValueChange?.Invoke();
        }

        private bool isValueChanged;
        private void OnTextChanged(object sender, EventArgs e)
        {
            string value = "";
            for (int valueIndex = 0; valueIndex < valueTbx.Text.Length; valueIndex++)
            {
                char valueChar = valueTbx.Text[valueIndex];
                if (char.IsDigit(valueChar) || valueChar == '-')
                    value += valueTbx.Text[valueIndex];
            }

            if (value.Length > 0)
                if (int.TryParse(value, out int colorValue))
                {
                    if (colorValue >= colorBar.Minimum && colorValue <= colorBar.Maximum)
                    {
                        colorBar.Value = colorValue;

                        UpdateValue();
                    }

                    isValueChanged = true;
                }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            if (isValueChanged)
            {
                UpdateValue();
                isValueChanged = false;
            }
        }

    }
}