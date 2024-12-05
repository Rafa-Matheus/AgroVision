using DHS.ADB;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AgroVision.Utils
{
    public static class TransferUtil
    {

        private const string APP_BUNDLE = "sforzatec.agrovision.cabin";

        public static void Transfer(string filePath)
        {
            //Copiar missão para o celular
            if (ADB.GetDevices().Count == 0)
            {
                MessageBox.Show("O dispositivo não está conectado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ADB.WaitForDevice();

            bool isInstalled = false;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate
            {
                isInstalled = CheckIfAppIsInstalled();
            };
            bw.RunWorkerAsync();

            bw.RunWorkerCompleted += delegate
            {
                if (isInstalled)
                {
                    string fileName = Path.GetFileName(filePath);
                    if (filePath.StartsWith("."))
                        filePath = $"{Path.GetFullPath(Path.GetDirectoryName(filePath))}\\{fileName}";

                    Process process = ADB.CreateProcess($"push \"{filePath}\" /mnt/sdcard/Android/data/{APP_BUNDLE}/files/{RenameFile(fileName)}");
                    process.Start();

                    process.WaitForExit();

                    if (process.ExitCode == 0)
                        MessageBox.Show("A missão foi transferida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Falha! O dispositivo não está conectado ou precisa liberar o acesso para copiar os arquivos.", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("O aplicativo de controle não está instalado no dispositivo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
        }

        private static bool CheckIfAppIsInstalled()
        {
            List<AndroidPackage> apps = ADB.LoadPackages(AppFilters.Third);

            for (int appIndex = 0; appIndex < apps.Count; appIndex++)
                if (apps[appIndex].Name.Contains(APP_BUNDLE))
                    return true;

            return false;
        }

        private static string RenameFile(string fileName)
        {
            return RemoveAccents(fileName).Replace(" ", "_");
        }

        private static string RemoveAccents(this string value)
        {
            char[] special = "àèìòùÀÈÌÒÙ äëïöüÄËÏÖÜ âêîôûÂÊÎÔÛ áéíóúÁÉÍÓÚðÐýÝ ãñõÃÑÕšŠžŽçÇåÅøØ".ToCharArray();
            char[] replace = "aeiouAEIOU aeiouAEIOU aeiouAEIOU aeiouAEIOUdDyY anoANOsSzZcCaAoO".ToCharArray();

            for (int charIndex = 0; charIndex < special.Length; charIndex++)
                value = value.Replace(special[charIndex], replace[charIndex]);

            return value;
        }

    }
}