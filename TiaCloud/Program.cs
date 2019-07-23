using System;
using System.IO;
using Siemens.Engineering;
using Siemens.Engineering.HW;

namespace TiaCloud
{
    class Program
    {
        //public static object DeviceMethods { get; private set; }
        public static int Counter = 0;
        // static string projectVersion = "15_1";
        static void Main(string[] args)
        {
            ////Console.WriteLine("Server is starting to listen...");
            //string data = Server.RunServer();
            //Console.WriteLine(data);

            Console.WriteLine("Starting TIA Portal...");
            using (TiaPortal tiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface))
            {
                Console.WriteLine("TIA Portal has started");
                ProjectComposition projects = tiaPortal.Projects;

                Console.WriteLine("Opening Project...");

                string path = @"C:\Users\CT-EVO\Desktop\Project2\Project2.ap16"; 
                FileInfo projectPath = new FileInfo(path);
                Project MyProject = null;
                // MyProject = projects.Open(projectPath);
                try
                {
                    MyProject = projects.Open(projectPath);
                }
                catch (Exception)
                {
                    Console.WriteLine(String.Format("Could not open project {0}", projectPath.FullName));
                    Console.WriteLine("Demo complete hit enter to exit");
                }
                //MainMethods.DisplayNetwork(MyProject);
                MainMethods.DisplayNetwork(MyProject);

                Server.RunServer(MyProject); //server is running

                MainMethods.DisplayNetwork(MyProject);

                //Device device = MyProject.Devices.Create("System:Device.Simocode", "Controller_SimocodePRO V_Name" + Guid.NewGuid().ToString());
                //DeviceItem rack = device.PlugNew("System:Rack.SimocodePro", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                //DeviceItem head = rack.PlugNew("OrderNumber:3UF7 010-1A*00-0/V4.1", "Head_name" + Guid.NewGuid().ToString(), 2);
                //Console.WriteLine("Added " + head.GetAttribute("Name"));

                //foreach (DeviceItem di in head.DeviceItems)
                //    Console.WriteLine("----" + di.Name);

                /*Console.WriteLine("Enter the name of device to delete");
                string DeviceNameToDelete = Console.ReadLine();
                //DeviceMethods.DeleteDevice(MyProject, DeviceNameToDelete); //does not work*/

                DeviceMethods.DisplayDeviceNames(MyProject);

                
                //Console.WriteLine("Do you want to compile project?");
                //string input = Console.ReadLine();
                //if (input == "yes")
                //{
                //    MainMethods.Compile(MyProject);
                //}

                //else
                //    Console.WriteLine("Program continues...");



                MainMethods.DisplayNetwork(MyProject); // necessary for withUserInterface mode

                /*
               Console.WriteLine("Creating Project...");
               Project MyProject = tiaPortal.Projects.Create(new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath)), Guid.NewGuid().ToString());


                //adder
                /* Device device = MyProject.Devices.Create("System:Device.Simocode", "unique_Device_Name");
                 DeviceItem rack = device.PlugNew("System:Rack.SimocodeProPN", "Rack_Name_123", 0);
                 DeviceItem simocodeHead = rack.PlugNew("OrderNumber:3UF7 011-1A*00-0/V2.0", "unique_Head_name", 0);

                 Device device1 = MyProject.Devices.Create("System:Device.Simocode", "unique_Device_Name1");
                 DeviceItem rack1 = device1.PlugNew("System:Rack.SimocodeProPN", "Rack_Name_1234", 0);
                 DeviceItem simocodeHead1 = rack1.PlugNew("OrderNumber:3UF7 011-1A*00-0/V2.0", "unique_Head_name1", 0);

             //comment adder, it will be organized and added to properties section
                 Console.WriteLine("Enter your comment: ");
                 string comment = Console.ReadLine();
                 simocodeHead.SetAttribute("Comment", comment);
                 Console.WriteLine("Comment added.");

             //changing attribute with get-set functions
                 simocodeHead.SetAttribute("Author", (String)"Elif");
                 Console.WriteLine(simocodeHead.GetAttribute(("Author")).ToString());

                 Console.WriteLine(device.GetAttribute("Comment").ToString());
             //saving project will be turn a function??
                 MyProject.Save();
                 */
               // MyProject.Save();
                Console.WriteLine("Saved..");

                if (Console.ReadLine() != null) //if console takes any input project will close
                    MyProject.Close();

                

            }

           

        }
    }
}
