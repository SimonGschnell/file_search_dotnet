// See https://aka.ms/new-console-template for more information

if (args.Length > 1)
{
    var file_name = args[0];
    var dir_name = args[1];
    DirectoryInfo dir_info = new DirectoryInfo(dir_name);
    
    
    
    Console.WriteLine(
        find_file(dir_info, file_name, ref dir_info) switch{
            true => $"found in {dir_info.Name}, full path: {dir_info.FullName+"/"+file_name}",
            false => "not found"
        }
    );
    return 0;   
}
else
{
    Console.WriteLine("Invalid usage of tool, to few arguments");
    return -1;
}


bool find_file(DirectoryInfo dir, string file_name, ref DirectoryInfo info)
{
    foreach(var file in dir.GetFiles())
    {
        if(file.Name == file_name)
        {
            info = dir;
            return true;
        }
    }
    foreach(var directory in dir.GetDirectories())
    {
        if(find_file(directory,file_name, ref info))
        {
            return true;
        }
    }
    
    return false;
}


