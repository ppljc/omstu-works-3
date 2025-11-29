using System.Security.Cryptography;
using System.Text;

namespace lab5_3;

public class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: MyBackup.exe <sourceFolder> <backupFolder>");
            return;
        }

        string sourcePath = args[0];
        string backupRoot = args[1];

        DirectoryInfo source = new DirectoryInfo(sourcePath);
        DirectoryInfo backup = new DirectoryInfo(backupRoot);

        if (!source.Exists)
        {
            Console.WriteLine("Source folder not found");
            return;
        }

        if (!backup.Exists)
            backup.Create();

        int latestVersion = GetLatestVersion(backup);

        if (latestVersion == 0)
        {
            CreateBackup(source, backup, 1);
            Console.WriteLine("Backup version 1 created");
            return;
        }

        DirectoryInfo latestBackupFolder = new DirectoryInfo(
            Path.Combine(backup.FullName, "v" + latestVersion));

        bool changed = IsChanged(source, latestBackupFolder);

        if (changed)
        {
            int newVersion = latestVersion + 1;
            CreateBackup(source, backup, newVersion);
            Console.WriteLine("Backup version " + newVersion + " created");
        }
        else
        {
            Console.WriteLine("No changes detected");
        }

        Console.WriteLine("\nНажмите любую клавишу, чтобы выйти...");
        Console.ReadKey();
    }

    static int GetLatestVersion(DirectoryInfo backup)
    {
        int max = 0;
        foreach (DirectoryInfo dir in backup.GetDirectories())
        {
            if (dir.Name.StartsWith("v"))
            {
                if (int.TryParse(dir.Name.Substring(1), out int v))
                {
                    if (v > max) max = v;
                }
            }
        }

        return max;
    }

    static bool IsChanged(DirectoryInfo source, DirectoryInfo backupFolder)
    {
        FileInfo[] srcFiles = source.GetFiles("*", SearchOption.AllDirectories);
        FileInfo[] bakFiles = backupFolder.GetFiles("*", SearchOption.AllDirectories);

        if (srcFiles.Length != bakFiles.Length)
            return true;

        foreach (FileInfo src in srcFiles)
        {
            string relative = Path.GetRelativePath(source.FullName, src.FullName);
            string bakPath = Path.Combine(backupFolder.FullName, relative);
            FileInfo bak = new FileInfo(bakPath);

            if (!bak.Exists)
                return true;

            string md5Src = GetMD5(src.FullName);
            string md5Bak = GetMD5(bak.FullName);

            if (!md5Src.Equals(md5Bak, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    static void CreateBackup(DirectoryInfo source, DirectoryInfo backup, int version)
    {
        string versionFolder = Path.Combine(backup.FullName, "v" + version);
        DirectoryInfo target = new DirectoryInfo(versionFolder);
        target.Create();

        foreach (FileInfo file in source.GetFiles("*", SearchOption.AllDirectories))
        {
            string relative = Path.GetRelativePath(source.FullName, file.FullName);
            string destPath = Path.Combine(target.FullName, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(destPath));
            file.CopyTo(destPath, true);
        }
    }

    static string GetMD5(string path)
    {
        using (var md5 = MD5.Create())
        using (var stream = File.OpenRead(path))
        {
            byte[] hash = md5.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}