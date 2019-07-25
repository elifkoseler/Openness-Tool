using System;
using System.IO;
using Siemens.Engineering;

namespace TiaCloud
{
    class Program
    {
        //public static object DeviceMethods { get; private set; }
        //public static int Counter = 0;
        // static string projectVersion = "15_1";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting TIA Portal...");
            //using (TiaPortal tiaPortal = new TiaPortal(TiaPortalMode.WithoutUserInterface))
            using (TiaPortal tiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface))
            {
                Console.WriteLine("TIA Portal has started");
                ProjectComposition projects = tiaPortal.Projects;

                Console.WriteLine("Opening Project...");

                string path = @"C:\Users\CT-EVO\Desktop\Project2\Project2.ap16"; 
                FileInfo projectPath = new FileInfo(path);

                Project myProject = null;

                try
                {
                    myProject = projects.Open(projectPath);
                }
                catch (Exception)
                {
                    Console.WriteLine(String.Format("Could not open project {0}", projectPath.FullName));
                    Console.WriteLine("Demo complete hit enter to exit");
                }
               
                MainMethods.DisplayNetwork(myProject);

                     Server.RunServer(myProject); //server is running

                MainMethods.DisplayNetwork(myProject);

                DeviceMethods.DisplayDeviceNames(myProject);

                MainMethods.DisplayNetwork(myProject); // necessary for withUserInterface mode

              
               //Console.WriteLine("Creating Project...");
               //Project MyProject = tiaPortal.Projects.Create(new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath)), Guid.NewGuid().ToString());


            }

           

        }
    }
}
