using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Diagnostics;

[RunInstaller(true)]
public class FileWatcherServiceInstaller : Installer
{
    public FileWatcherServiceInstaller()
    {
        var processInstaller = new ServiceProcessInstaller
        {
            Account = ServiceAccount.LocalSystem
        };

        var serviceInstaller = new ServiceInstaller
        {
            ServiceName = "filewatcherservice",
            DisplayName = "File Watcher Service",
            Description = "Bu hizmetteki e-posta gönderim süresini, e-postanın gönderileceği adresi, izlemek istediğiniz dizini ve dosya türlerini ayarlamak için appsettings.json dosyasını kullanabilirsiniz.",
            StartType = ServiceStartMode.Automatic
        };

        Installers.Add(processInstaller);
        Installers.Add(serviceInstaller);
    }

    public override void Install(System.Collections.IDictionary stateSaver)
    {
        base.Install(stateSaver);
        SetRecoveryOptions();
    }

    public override void Commit(System.Collections.IDictionary savedState)
    {
        base.Commit(savedState);
        SetRecoveryOptions();
    }

    private void SetRecoveryOptions()
    {
        string serviceName = "filewatcherService";
        using (Process process = new Process())
        {
            process.StartInfo.FileName = "sc";
            process.StartInfo.Arguments = $"failure \"{serviceName}\" reset= 0 actions= restart/60000";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }
    }
}
