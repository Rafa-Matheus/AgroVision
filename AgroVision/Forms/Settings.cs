using AgroVision.CustomViews;
using AgroVision.Utils;
using AgroVision.Views;
using System;
using System.Windows.Forms;

namespace AgroVision.Forms
{
    public partial class Settings : Form
    {

        private readonly SpinnerView medidas;
        public Settings()
        {
            InitializeComponent();

            medidas = new SpinnerView();
            medidas.AddOption(new SpinnerOption("m²", 0));
            medidas.AddOption(new SpinnerOption("Acre", 1));
            medidas.AddOption(new SpinnerOption("Hectar", 2));

            medidas.Value = Properties.Settings.Default["metrics_unit"];

            ControlUtil.AddCustomField(new CustomControl("Unidade de Medida", "", Properties.Resources.metrics, medidas, settingsPnl));
        }

        private void OnClick_applyBtn(object sender, EventArgs e)
        {
            Properties.Settings.Default["metrics_unit"] = medidas.Value;

            Properties.Settings.Default.Save();

            Close();
        }

        private void OnClick_cancelBtn(object sender, EventArgs e)
        {
            Close();
        }

    }
}