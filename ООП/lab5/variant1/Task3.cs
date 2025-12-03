namespace lab5;

public class Task3
{
    private string sourceExe;

    public void Execute()
    {
        Console.Write("Введите имя файла для копирования: ");
        sourceExe = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrWhiteSpace(sourceExe))
        {
            return;
        }
        
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady)
            {
                continue;
            }

            try
            {
                TraverseAndCopy(drive.RootDirectory.FullName);
            }
            catch
            {
                // ignored
            }
        }
    }

    private void TraverseAndCopy(string path)
    {
        string[] subdirs;

        try
        {
            subdirs = Directory.GetDirectories(path);

            string targetPath = Path.Combine(path, Path.GetFileName(sourceExe));

            if (!File.Exists(targetPath))
            {
                File.Copy(sourceExe, targetPath, overwrite: false);
                Console.WriteLine($"Скопировано в: {targetPath}");
            }
        }
        catch
        {
            return;
        }

        foreach (var sub in subdirs)
        {
            TraverseAndCopy(sub);
        }
    }
}