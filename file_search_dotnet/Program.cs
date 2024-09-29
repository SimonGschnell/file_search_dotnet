// See https://aka.ms/new-console-template for more information
using Mono.Unix;

if (args.Length > 1)
{
    try{
        var file_name = args[0];
        var dir_name = args[1];

        
        DirectoryInfo dir_info = new DirectoryInfo(dir_name);
        List<DirectoryInfo> foundInDirs = []; 
        find_file(dir_info, file_name, foundInDirs);
        
        
        if(foundInDirs.Count > 0)
        {
            foreach(var dir in foundInDirs)
            {
                Console.WriteLine($"found file in directory: {dir}, the fullpath is: {dir+file_name}");
            }
        }
        else
        {
            Console.WriteLine($"file with name {file_name} could not be found in the directory {dir_name}");
        }
        
    }catch(Exception e){
        Console.WriteLine(e.Message);
    }
    
    return 0;   
}
else
{
    Console.WriteLine("Invalid usage of tool, to few arguments");
    return -1;
}



void find_file(DirectoryInfo dir, string file_name, List<DirectoryInfo> foundInDirs)
{
    FileInfo[] files = dir.GetFiles();
    DirectoryInfo[] dirs = dir.GetDirectories();

    foreach(var file in files)
    {
        if(file.Name == file_name)
        {
            foundInDirs.Append(dir);
        }
    }
    
    foreach(var directory in dirs)
    {
        // avoid symbolic links and check permission
        if(directory.LinkTarget == null && new UnixDirectoryInfo(directory.FullName).FileAccessPermissions.HasFlag(FileAccessPermissions.UserRead)){
            find_file(directory,file_name,foundInDirs);
        }
    }
}



