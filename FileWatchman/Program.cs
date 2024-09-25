using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Newtonsoft.Json;

public class FileWatcherService : ServiceBase
{
    private FileSystemWatcher _watcher;
    private Timer _timer;
    private StringBuilder _changeLog;
    private Settings _settings;
    const string FROM_PASSWORD = "xftuyzkkjwxieqqb";

    public FileWatcherService()
    {
        ServiceName = "filewatcherService";
    }

    protected override void OnStart(string[] args)
    {
        try
        {
            string settingsPath = @"..\FileWatchman\appsettings.json";

            _settings = Settings.Load(settingsPath);

            _watcher = new FileSystemWatcher
            {
                Path = _settings.PathToWatch,
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite,
                Filter = _settings.ToFilter
            };

            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.Changed += OnChanged;

            _watcher.EnableRaisingEvents = true;

            _changeLog = new StringBuilder();

            _timer = new Timer(_settings.TimerInterval);
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();

            EventLog.WriteEntry(ServiceName, $"Started watching {_settings.PathToWatch} with filter {_settings.ToFilter}", EventLogEntryType.Information);

            // Bilgisayar açıldığında e-posta gönder
            string startupMessage = $"The service has started and is watching {_settings.PathToWatch} with filter {_settings.ToFilter}";
            SendEmail("Service Started", startupMessage);
        }
        catch (FileNotFoundException ex)
        {
            EventLog.WriteEntry(ServiceName, $"Settings file not found: {ex.Message}\n{ex.StackTrace}", EventLogEntryType.Error);
            throw;
        }
        catch (JsonException ex)
        {
            EventLog.WriteEntry(ServiceName, $"Error parsing settings file: {ex.Message}\n{ex.StackTrace}", EventLogEntryType.Error);
            throw;
        }
        catch (Exception ex)
        {
            EventLog.WriteEntry(ServiceName, $"Failed to start service: {ex.Message}\n{ex.StackTrace}", EventLogEntryType.Error);
            throw;
        }
    }

    private void OnCreated(object source, FileSystemEventArgs e)
    {
        lock (_changeLog)
        {
            long fileSize = GetFileSize(e.FullPath);
            _changeLog.AppendLine($"{DateTime.Now}: {e.FullPath} was created. Size: {fileSize} bytes.");
        }
    }

    private void OnDeleted(object source, FileSystemEventArgs e)
    {
        lock (_changeLog)
        {
            _changeLog.AppendLine($"{DateTime.Now}: {e.FullPath} was deleted.");
        }
    }

    private void OnRenamed(object source, RenamedEventArgs e)
    {
        lock (_changeLog)
        {
            long fileSize = GetFileSize(e.FullPath);
            _changeLog.AppendLine($"{DateTime.Now}: {e.OldFullPath} was renamed to {e.FullPath}. Size: {fileSize} bytes.");
        }
    }

    private void OnChanged(object source, FileSystemEventArgs e)
    {
        lock (_changeLog)
        {
            long fileSize = GetFileSize(e.FullPath);
            _changeLog.AppendLine($"{DateTime.Now}: {e.FullPath} was changed. Size: {fileSize} bytes.");
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        string log;
        lock (_changeLog)
        {
            log = _changeLog.ToString();
            _changeLog.Clear();
        }

        if (!string.IsNullOrEmpty(log))
        {
            string formattedLog = EmailFormatter.FormatChanges(log);
            SendEmail("File Changes", formattedLog);
        }
    }

    private void SendEmail(string subject, string body)
    {
        MailAddress fromAddress = new MailAddress("filewatchman@gmail.com", "File Watchman", Encoding.UTF8);
        MailAddress toAddress = new MailAddress(_settings.ToEmailAddress, _settings.ToEmailName, Encoding.UTF8);

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, FROM_PASSWORD),
            Timeout = 10000
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true // HTML formatında e-posta göndermek için
        })
        {
            try
            {
                smtp.Send(message);
                WriteEntryInChunks(ServiceName, $"Email sent: {subject} - {body}", EventLogEntryType.Information);
            }
            catch (SmtpException smtpEx)
            {
                WriteEntryInChunks(ServiceName, $"Failed to send email (SMTP error): {smtpEx.Message}\n{smtpEx.StackTrace}", EventLogEntryType.Error);
            }
            catch (IOException ioEx)
            {
                WriteEntryInChunks(ServiceName, $"Failed to send email (IO error): {ioEx.Message}\n{ioEx.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                WriteEntryInChunks(ServiceName, $"Failed to send email: {ex.Message}\n{ex.StackTrace}", EventLogEntryType.Error);
            }
        }
    }

    private void WriteEntryInChunks(string source, string message, EventLogEntryType type)
    {
        int chunkSize = 32000; // Her parça için boyut
        for (int i = 0; i < message.Length; i += chunkSize)
        {
            string chunk = message.Substring(i, Math.Min(chunkSize, message.Length - i));
            EventLog.WriteEntry(source, chunk, type);
        }
    }

    private long GetFileSize(string filePath)
    {
        try
        {
            return new FileInfo(filePath).Length;
        }
        catch (FileNotFoundException)
        {
            return 0; // Dosya bulunamazsa 0 döndür
        }
        catch (Exception)
        {
            return 0; // Diğer hatalar için de 0 döndür
        }
    }

    protected override void OnStop()
    {
        _watcher.EnableRaisingEvents = false;
        _watcher.Dispose();
        _timer.Stop();
        _timer.Dispose();

        EventLog.WriteEntry(ServiceName, "Stopped watching", EventLogEntryType.Information);

        // Bilgisayar kapatıldığında e-posta gönder
        string shutdownMessage = "The service has stopped.";
        SendEmail("Service Stopped", shutdownMessage);
    }

    public static void Main()
    {
        ServiceBase.Run(new FileWatcherService());
    }
}
