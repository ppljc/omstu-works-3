namespace lab5;

public class Task2
{
    public void Execute()
    {
        List<string> docs = [];
        string[] docExtensions = [".doc", ".docx", ".pdf", ".txt", ".rtf", ".xls", ".xlsx", ".ppt", ".pptx"];

        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady)
                continue;

            try
            {
                TraverseDirectory(drive.RootDirectory.FullName, docExtensions, docs);
            }
            catch
            {
                // ignored
            }
        }

        File.WriteAllLines("docs.txt", docs);
    }

    private void TraverseDirectory(string path, string[] exts, List<string> buffer)
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

        foreach (var f in files)
        {
            string ext = Path.GetExtension(f);
            if (Array.Exists(exts, e => string.Equals(ext, e, StringComparison.OrdinalIgnoreCase)))
            {
                buffer.Add(f);
            }
        }

        foreach (var sub in subdirs)
            TraverseDirectory(sub, exts, buffer);
    }
}