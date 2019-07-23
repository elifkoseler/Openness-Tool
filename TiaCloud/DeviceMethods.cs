using System;
using Siemens.Engineering;
using Siemens.Engineering.HW;


namespace TiaCloud
{
    public class DeviceMethods
    {
        public static void AddSpecificDevice(Project project,string typeIdentifier)  //string TypeIdentifier, string Name, int PositionNumber
        {
            Device device = null;
            DeviceItem rack = null;
            DeviceItem head = null;

            switch (typeIdentifier)
            {
                case "SIMO":
                case "simocode"://it just supports PN

                    device = project.Devices.Create("System:Device.Simocode", "Controller_SimocodePRO PN_Name" + Guid.NewGuid().ToString());   //guid = random 
                    rack = device.PlugNew("System:Rack.SimocodeProPN", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                    head = rack.PlugNew("OrderNumber:3UF7 011-1A*00-0/V2.0", "Simocode1_" + Guid.NewGuid().ToString(), 0);
                    Console.WriteLine("Added " + head.GetAttribute("Name"));

                    break;

                case "SIM":
                case "simatic": //s7400: it supports mpi and profibus

                    device = project.Devices.Create("System:Device.S7400", "PLC_s7400_Name"+ Guid.NewGuid().ToString());
                    rack = device.PlugNew("OrderNumber:6ES7 400-1TA01-0AA0", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                    head = rack.PlugNew("OrderNumber:6ES7 414-3XM05-0AB0/V5.3", "Simatic1_" + Guid.NewGuid().ToString(), 2);
                    Console.WriteLine("Added " + head.GetAttribute("Name"));
                    break;

                case "ASI":
                case "asi": //asi f slave: it suppors just ASi

                    device = project.Devices.Create("System:Device.ASi", "ASI_Name" + Guid.NewGuid().ToString());
                    rack = device.PlugNew("System:Rack.ASi", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                    head = rack.PlugNew("OrderNumber:AS-i F Slave Universal", "Asi_" + Guid.NewGuid().ToString(), 1);
                    Console.WriteLine("Added " + head.GetAttribute("Name"));
                    break;

                case "SIM2":
                case "simatic2":

                    device = project.Devices.Create("System:Device.S71500", "PLC_s71500_Name" + Guid.NewGuid().ToString());
                    rack = device.PlugNew("OrderNumber:6ES7 590-1***0-0AA0", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                    head = rack.PlugNew("OrderNumber:6ES7 516-3AN00-0AB0/V1.8", "Simatic2_" + Guid.NewGuid().ToString(), 1);
                    Console.WriteLine("Added " + head.GetAttribute("Name"));
                    break;

                case "SIM3":
                case "simatic3":

                    device = project.Devices.Create("System:Device.S71500", "PLC_s71500_Name" + Guid.NewGuid().ToString());
                    rack = device.PlugNew("OrderNumber:6ES7 590-1***0-0AA0", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                    head = rack.PlugNew("OrderNumber:6ES7 511-1AK02-0AB0/V2.8", "Simatic3_" + Guid.NewGuid().ToString(), 1);
                    Console.WriteLine("Added " + head.GetAttribute("Name"));
                    break;

                case "SIMO2":
                case "simocode2":

                    device = project.Devices.Create("System:Device.Simocode", "Controller_SimocodePRO V_Name" + Guid.NewGuid().ToString()); 
                    rack = device.PlugNew("System:Rack.SimocodePro", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                    head = rack.PlugNew("OrderNumber:3UF7 010-1A*00-0/V4.1", "Simocode2_" + Guid.NewGuid().ToString(), 2);
                    Console.WriteLine("Added " + head.GetAttribute("Name"));

                    break;


            }
            Console.WriteLine("Added");


        }


        public static void AddMultipleDevices(Project project, int NumberOfDevices)
        {
            Device device = null;
            DeviceItem rack = null;
            DeviceItem head = null;
            for (int i = 0; i < NumberOfDevices; i++)
            {
                device = project.Devices.Create("System:Device.Simocode", "Device_Name" + Guid.NewGuid().ToString());   //guid = random 
                rack = device.PlugNew("System:Rack.SimocodeProPN", "Rack_Name" + Guid.NewGuid().ToString(), 0);
                head = rack.PlugNew("OrderNumber:3UF7 011-1A*00-0/V2.0", "Head_name" + Guid.NewGuid().ToString(), 0);
                Console.WriteLine("Added" + "Head_name" + i.ToString());

            }

        }

        public static void DeleteDevice(Project project, string DeviceName)
        {
            DeviceComposition devices = project.Devices;
            Device foundDevice = devices.Find(DeviceName);//find first
            devices.Contains(foundDevice);//then try with if statement


            foreach (Device device in devices)  //ERROR: System.InvalidOperationException: 'Collection was modified; enumeration operation may not execute.' 
            {
                if (device.Name == DeviceName)
                {

                    Console.WriteLine("Found");
                    device.Delete();
                    Console.WriteLine("And Deleted...");

                }
                else
                {
                    Console.WriteLine("Not Found...");
                }
            }
        }

        public static void DeleteSpecificDevice(Project project, Device deviceToDelete)
        {
            if (project.UngroupedDevicesGroup.Devices.Contains(deviceToDelete))
                deviceToDelete.Delete();
            else
                Console.WriteLine("Project does not contain this device");

        }

        public static void DisplayDeviceNames(Project project)
        {
            var allDevices = project.Devices;
            int i = 0;
             
            foreach(Device device in allDevices)
            {
                i++;
                Console.WriteLine(i+" "+device.Name);
            }
        }

        public static Device GetDevice(Project project,int index)   
        {
            DeviceComposition allDevices = project.Devices;
            Device device = allDevices[index];
            return device;

        }
      
        public static void ChangeProperties(Project project, int index,string name,string author,string comment) 
        {
            Device device = GetDevice(project, index);

            if (name != null && name != "x")
            {
                Console.WriteLine(device.GetAttribute("Name"));
                device.SetAttribute("Name", name);
                Console.WriteLine(device.GetAttribute("Name"));

            }
            else {
                Console.WriteLine(device.GetAttribute("Name"));
            }

            if (author != null && author != "x")
            {
                Console.WriteLine(device.GetAttribute("Author"));
                device.SetAttribute("Author", author);
                Console.WriteLine(device.GetAttribute("Author"));

            }
            else
            {
                Console.WriteLine(device.GetAttribute("Author"));
            }

            if (comment != null && comment != "x")
            {
                Console.WriteLine(device.GetAttribute("Comment"));
                device.SetAttribute("Comment", comment);
                Console.WriteLine(device.GetAttribute("Comment"));

            }
            else
            {
                Console.WriteLine(device.GetAttribute("Comment"));
            }

            Console.WriteLine("Properties is changed!");

        }
    }

}