namespace lab5;

public class Task2
{
    public void Execute()
    {
        List<string> pictures = new List<string>();
        string[] pictureExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp" };

        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady)
                continue;

            try
            {
                TraverseDirectory(drive.RootDirectory, pictureExtensions, pictures);
            }
            catch { }
        }

        var output = new FileInfo("pictures.txt");
        using (var writer = new StreamWriter(output.OpenWrite()))
        {
            foreach (var path in pictures)
                writer.WriteLine(path);
        }
    }

    private void TraverseDirectory(DirectoryInfo dir, string[] exts, List<string> buffer)
    {
        FileInfo[] files;
        DirectoryInfo[] subdirs;

        try
        {
            files = dir.GetFiles();
            subdirs = dir.GetDirectories();
        }
        catch
        {
            return;
        }

        foreach (var file in files)
        {
            if (Array.Exists(exts, e => 
                    file.Extension.Equals(e, StringComparison.OrdinalIgnoreCase)))
            {
                buffer.Add(file.FullName);
            }
        }

        foreach (var sub in subdirs)
            TraverseDirectory(sub, exts, buffer);
    }
}