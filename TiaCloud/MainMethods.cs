using System;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW;

namespace TiaCloud
{
    class MainMethods
    {
        static string projectVersion = "16";

        public static void DisplayNetwork(Project project)  //displays network view
        {
            project.ShowHwEditor(Siemens.Engineering.HW.View.Network);
            //project.ShowHwEditor(Siemens.Engineering.HW.View.Topology);
            Console.WriteLine("--Displaying Network on TIA--");
        }
      
        public static string Compile(Project project, int index) //to compile all project
        {
            var allDevices = project.Devices;
            Device device = DeviceMethods.GetDevice(project, index);

            string message = null;
            PlcSoftware plcSoftware = DeviceItemMethods.GetPlcSoftware(device); //software
            message = CompileMethods.CompilePlcSoftware(plcSoftware);
            message += CompileMethods.CompileCodeBlock(plcSoftware);

            //HmiTarget hmiTarget = DeviceItemMethods.GetHmiTarget(device); //hardware
            //message += CompileMethods.CompileHmiTarget(hmiTarget);
            return message;
        }

        public static void Save(Project project)
        {
            Console.WriteLine("Project is saving...");
            project.Save();
            Console.WriteLine("Saved!");
        }

        public static void Close(Project project)
        {
            Console.WriteLine("Project is closing...");
            project.Close();
            Console.WriteLine("Closed!");
        }
     
    }
}
