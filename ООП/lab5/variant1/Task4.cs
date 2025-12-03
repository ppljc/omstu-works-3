namespace lab5;

public class Task4
{
    public void Execute()
    {
        Console.Write("Введите имя файла для удаления: ");
        string sourceExe = Console.ReadLine()?.Trim();
        string targetDigest = ComputeMD5(sourceExe);

        if (string.IsNullOrWhiteSpace(sourceExe))
        {
            return;
        }

        foreach (var drive in DriveInfo.GetDrives())
        {
            Console.WriteLine(drive.Name);
            
            if (!drive.IsReady)
            {
                continue;
            }

            try
            {
                TraverseAndDelete(drive.RootDirectory.FullName, targetDigest);
            }
            catch
            {
                // ignored
            }
        }
    }
    
    private void TraverseAndDelete(string path, string targetDigest)
    {
        string[] files;
        string[] subdirs;

        try
        {
            files = Directory.GetFiles(path);
            subdirs = Directory.GetDirectories(path);
        }
        catch
        {
            return;
        }
        
        Console.WriteLine($"{files.Length} файлов и {subdirs.Length} подпапок в {path}");

        foreach (var file in files)
        {
            if (Path.GetExtension(file).Equals(".exe", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string digest = ComputeMD5(file);
                    if (string.Equals(digest, targetDigest, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Удалён файл: {file}");
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        foreach (var sub in subdirs)
        {
            TraverseAndDelete(sub, targetDigest);
        }
    }

    private string ComputeMD5(string filePath)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}