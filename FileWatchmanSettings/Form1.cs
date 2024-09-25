using FileWatchers;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Text.RegularExpressions;

namespace FileWatchmanSettings
{
    public partial class Form1 : Form
    {
        private Settings _settings;

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            InitializeComboBox();
            UpdateServiceButtonStates();
        }

        private void LoadSettings()
        {
            string settingsPath = Path.Combine(Application.StartupPath, "appsettings.json");
           // MessageBox.Show("Settings path: " + settingsPath);
            if (File.Exists(settingsPath))
            {
                _settings = Settings.Load(settingsPath);
                txtPathToWatch.Text = _settings.PathToWatch;
                txtToEmailAddress.Text = _settings.ToEmailAddress;
                txtToEmailName.Text = _settings.ToEmailName;
                txtToFilter.Text = _settings.ToFilter;

                comboBoxTimerInterval.SelectedItem = _settings.TimerInterval;
            }
            else
            {
                _settings = new Settings();
            }
        }

        private void InitializeComboBox()
        {
            comboBoxTimerInterval.Items.Add("10000");
            comboBoxTimerInterval.Items.Add("20000");
            comboBoxTimerInterval.Items.Add("30000");

            if (comboBoxTimerInterval.Items.Count > 0)
            {
                comboBoxTimerInterval.SelectedIndex = 0;
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            _settings.PathToWatch = txtPathToWatch.Text;
            _settings.ToEmailAddress = txtToEmailAddress.Text;
            _settings.ToEmailName = txtToEmailName.Text;
            _settings.ToFilter = txtToFilter.Text;
            _settings.TimerInterval = comboBoxTimerInterval.SelectedItem.ToString();

            string settingsPath = Path.Combine(Application.StartupPath, "appsettings.json");
            File.WriteAllText(settingsPath, Newtonsoft.Json.JsonConvert.SerializeObject(_settings, Newtonsoft.Json.Formatting.Indented));
            MessageBox.Show("Settings saved successfully!");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Select the folder to watch";
                folderBrowser.ShowNewFolderButton = false;

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    txtPathToWatch.Text = folderBrowser.SelectedPath;
                }
            }
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController("filewatcherservice");
            try
            {
                if (service.Status != ServiceControllerStatus.Running && service.Status != ServiceControllerStatus.StartPending)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                    MessageBox.Show("Service started successfully!");
                    UpdateServiceButtonStates();
                }
                else
                {
                    MessageBox.Show("Service is already running.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start service: " + ex.Message);
            }
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            string serviceName = "filewatcherservice";
            ServiceController service = new ServiceController(serviceName);

            try
            {
                if (service.Status == ServiceControllerStatus.Stopped || service.Status == ServiceControllerStatus.StopPending)
                {
                    MessageBox.Show("Service is already stopped.");
                    return;
                }

                if (service.Status == ServiceControllerStatus.Running || service.Status == ServiceControllerStatus.StartPending)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                    MessageBox.Show("Service stopped successfully!");
                    UpdateServiceButtonStates();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to stop service: {ex.Message}");
            }
        }

        private void btnInstallUninstallService_Click(object sender, EventArgs e)
        {
            string serviceName = "filewatcherservice";
            string exePath = Path.Combine(Application.StartupPath, "FileWatchman.exe");

            if (IsServiceInstalled(serviceName))
            {
                DialogResult result = MessageBox.Show("Service is already installed. Do you want to uninstall it?", "Uninstall Service", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    UninstallService(serviceName);
                    UpdateServiceButtonStates();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Service is not installed. Do you want to install it?", "Install Service", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    InstallService(exePath);
                    UpdateServiceButtonStates();
                }
            }
        }

        private bool IsServiceInstalled(string serviceName)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceName))
                {
                    return sc.Status != ServiceControllerStatus.Stopped;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking service installation: {ex.Message}");
                return false;
            }
        }

        private void InstallService(string exePath)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "sc",
                        Arguments = $"create filewatcherservice binPath= \"{exePath}\" start= auto",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    MessageBox.Show("Service installed successfully! Output: " + output);
                }
                else
                {
                    MessageBox.Show($"Failed to install service. Error: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to install service: {ex.Message}");
            }
        }

        private void UninstallService(string serviceName)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceName))
                {
                    if (sc.Status != ServiceControllerStatus.Stopped && sc.Status != ServiceControllerStatus.StopPending)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                    }
                }

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "sc",
                        Arguments = $"delete {serviceName}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    MessageBox.Show("Service uninstalled successfully! Output: " + output);
                }
                else
                {
                    MessageBox.Show($"Failed to uninstall service. Error: {error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to uninstall service. Error: {ex.Message}");
            }
        }

        private void txtPathToWatch_TextChanged(object sender, EventArgs e)
        {
            txtPathToWatch.ReadOnly = true;
        }

        private void txtToEmailAddress_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidEmail(txtToEmailAddress.Text))
            {
                lblEmailValidation.Text = "Invalid email address!";
                lblEmailValidation.ForeColor = Color.Red;
            }
            else
            {
                lblEmailValidation.Text = "";
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            var idn = new IdnMapping();

            string domainName = idn.GetAscii(match.Groups[2].Value);
            return match.Groups[1].Value + domainName;
        }

        private void UpdateServiceButtonStates()
        {
            ServiceController service = new ServiceController("filewatcherservice");

            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                }
                else
                {
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                }
            }
            catch
            {
                btnStartService.Enabled = true;
                btnStopService.Enabled = false;
            }
        }
    }
}
