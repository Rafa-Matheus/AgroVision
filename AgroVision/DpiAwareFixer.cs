using System.Collections.Generic;
using System.Windows.Forms;

namespace AgroVision
{
    public class DpiAwareFixer
    {

        // Dicionário para armazenar as distâncias iniciais dos controles
        private static readonly Dictionary<Control, AnchorDistances> initialDistances = new Dictionary<Control, AnchorDistances>();

        public static void AdjustAllForms(Control mainControl)
        {
            foreach (Form form in Application.OpenForms)
            {
                AdjustUserControls(form);
            }
        }

        // Varre todos os controles dentro de um formulário
        public static void AdjustUserControls(Control container)
        {
            foreach (Control control in container.Controls)
            {
                // Se o controle for um UserControl ou outro container, faça a varredura recursiva
                if (control is UserControl || control.HasChildren)
                {
                    AdjustUserControls(control); // Varre recursivamente UserControls
                }

                // Verifica se o controle possui ancoragem
                if (control.Anchor != AnchorStyles.None)
                {
                    // Calcula e armazena as distâncias iniciais se não estiver já armazenado
                    if (!initialDistances.ContainsKey(control))
                    {
                        initialDistances[control] = GetAnchorDistances(control);
                    }

                    // Ajusta a posição com base nas âncoras
                    AdjustControlPosition(control, container);
                }
            }
        }

        // Ajusta a posição do controle com base nas âncoras e distâncias iniciais
        private static void AdjustControlPosition(Control control, Control container)
        {
            // Obtém as distâncias iniciais armazenadas
            var distances = initialDistances[control];

            // Recalcula a posição com base nas âncoras
            if (control.Anchor.HasFlag(AnchorStyles.Left))
            {
                control.Left = distances.Left;
            }
            if (control.Anchor.HasFlag(AnchorStyles.Right))
            {
                control.Left = container.ClientSize.Width - control.Width - distances.Right;
            }
            if (control.Anchor.HasFlag(AnchorStyles.Top))
            {
                control.Top = distances.Top;
            }
            if (control.Anchor.HasFlag(AnchorStyles.Bottom))
            {
                control.Top = container.ClientSize.Height - control.Height - distances.Bottom;
            }
        }

        // Função que calcula e retorna as distâncias iniciais de um controle para as bordas do container
        private static AnchorDistances GetAnchorDistances(Control control)
        {
            var container = control.Parent;

            // Calcula as distâncias iniciais de cada lado
            int leftDistance = control.Left;
            int rightDistance = container.ClientSize.Width - control.Right;
            int topDistance = control.Top;
            int bottomDistance = container.ClientSize.Height - control.Bottom;

            return new AnchorDistances(leftDistance, rightDistance, topDistance, bottomDistance);
        }

        // Classe auxiliar para armazenar as distâncias de cada âncora
        private class AnchorDistances
        {
            public int Left { get; }
            public int Right { get; }
            public int Top { get; }
            public int Bottom { get; }

            public AnchorDistances(int left, int right, int top, int bottom)
            {
                Left = left;
                Right = right;
                Top = top;
                Bottom = bottom;
            }
        }

    }
}