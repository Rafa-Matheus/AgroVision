using AgroVision.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AgroVision
{
    public partial class RasterCalculator : Form
    {

        #region Propriedades
        private List<IndexEquation> Equations { get; set; }

        public string Equation
        {
            get { return equationRcb.Text; }
            set
            {
                bool founded = false;
                for (int equationIndex = 0; equationIndex < Equations.Count; equationIndex++)
                {
                    IndexEquation equation = Equations[equationIndex];
                    if (equation.Equation.Equals(value))
                    {
                        presetsCbx.SelectedIndex = equationIndex;
                        founded = true;
                        break;
                    }
                }

                if (!founded)
                    equationRcb.Text = value;
            }
        }
        #endregion

        #region Inicializar
        public RasterCalculator()
        {
            InitializeComponent();

            equationRcb.SelectionAlignment = HorizontalAlignment.Right;

            LoadEquations();

            presetsCbx.SelectedIndexChanged += delegate
            {
                if (presetsCbx.SelectedIndex != -1)
                {
                    IndexEquation equation = (IndexEquation)presetsCbx.SelectedItem;
                    equationRcb.Text = equation.Equation;
                    descriptionRcb.Text = equation.Description;
                }
            };
        }
        #endregion

        #region Métodos
        private void LoadEquations()
        {
            Equations = new List<IndexEquation>();

            using (StreamReader file = File.OpenText(@".\indicies.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray indicies = (JArray)JToken.ReadFrom(reader);
                for (int equationIndex = 0; equationIndex < indicies.Count; equationIndex++)
                {
                    string name = indicies[equationIndex]["name"].ToString();
                    string equation = indicies[equationIndex]["equation"].ToString();
                    string description = indicies[equationIndex]["description"].ToString();

                    IndexEquation index = new IndexEquation(name, equation, description);

                    Equations.Add(index);
                    presetsCbx.Items.Add(index);
                }
            }
        }

        private void Backspace()
        {
            if (caretPosition == -1)
                caretPosition = equationRcb.SelectionStart;

            int textLength = equationRcb.Text.Length;
            if (textLength > 0 && caretPosition > 0)
            {
                equationRcb.Text = $"{equationRcb.Text.Substring(0, caretPosition - 1)}{equationRcb.Text.Substring(caretPosition, textLength - caretPosition)}";
                caretPosition--;
            }
        }
        #endregion

        #region Eventos
        private void OnClickKey(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;

            if (caretPosition == -1)
                caretPosition = equationRcb.SelectionStart;

            StringBuilder builder = new StringBuilder(equationRcb.Text);
            builder.Insert(caretPosition, senderButton.Text);
            equationRcb.Text = builder.ToString();

            caretPosition++;
        }

        private void OnClick_backspcBtn(object sender, EventArgs e)
        {
            Backspace();
        }

        private void OnClick_applyBtn(object sender, EventArgs e)
        {
            if (RasterUtil.IsOmittingZeros(equationRcb.Text))
            {
                MessageBox.Show("Por favor, evite omitir zeros antes do separador decimal.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!RasterUtil.IsValidEquation(equationRcb.Text))
            {
                MessageBox.Show("A equação inserida não é válida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(presetsCbx.Text))
            {
                bool founded = false;
                for (int equationIndex = 0; equationIndex < Equations.Count; equationIndex++)
                {
                    IndexEquation equation = Equations[equationIndex];
                    if (equation.Name.Equals(presetsCbx.Text))
                    {
                        founded = true;
                        break;
                    }
                }

                if (!founded)
                    Equations.Add(new IndexEquation(presetsCbx.Text, equationRcb.Text, descriptionRcb.Text));

                JArray array = new JArray();
                for (int equationIndex = 0; equationIndex < Equations.Count; equationIndex++)
                {
                    IndexEquation equation = Equations[equationIndex];

                    JObject obj = new JObject
                    {
                        ["name"] = equation.Name,
                        ["equation"] = equation.Equation,
                        ["description"] = equation.Description
                    };

                    array.Add(obj);
                }

                File.WriteAllText(@".\indicies.json", array.ToString(), Encoding.UTF8);
            }

            DialogResult = DialogResult.OK;
        }

        private int caretPosition;
        private void OnClick_equationRcb(object sender, EventArgs e)
        {
            caretPosition = -1;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
                applyBtn.PerformClick();

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

    }
}