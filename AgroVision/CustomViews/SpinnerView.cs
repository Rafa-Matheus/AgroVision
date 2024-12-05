using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgroVision.CustomViews;

namespace AgroVision.Forms
{
    public partial class SpinnerView : UserControl
    {

        public event Action OnValueChange;

        public SpinnerView()
        {
            InitializeComponent();

            Options = new List<SpinnerOption>();
        }

        public object Value
        {
            get { return ((SpinnerOption)comboBox.SelectedItem).Value; }
            set
            {
                for (int itemIndex = 0; itemIndex < comboBox.Items.Count; itemIndex++)
                    if (((SpinnerOption)comboBox.Items[itemIndex]).Value.Equals(value))
                    {
                        comboBox.SelectedIndex = itemIndex;

                        UpdateValue();
                        break;
                    }
            }
        }

        public void AddOption(SpinnerOption option)
        {
            Options.Add(option);

            comboBox.Items.Clear();
            comboBox.Items.AddRange(Options.ToArray());

            comboBox.SelectedIndex = 0;
        }

        public List<SpinnerOption> Options { get; set; }

        private void UpdateValue()
        {
            OnValueChange?.Invoke();
        }

        private void OnChanged(object sender, EventArgs e)
        {
            UpdateValue();
        }

    }
}