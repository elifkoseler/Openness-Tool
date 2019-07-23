using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.Compiler;
using Siemens.Engineering.Cax;

namespace TiaCloud

{
    class Networking
    {
        public static void ConnectToSubnet(Project project)
        {
            var allDevices = project.Devices;

            //creating subnet to not connected
            SubnetComposition subnets = project.Subnets;
            Subnet newSubnet = subnets.Create("System:Subnet.Ethernet", "MyPnSubnet");
            //subnet typeIdentifiers can be .Profibus, .Mpi, .Asi 

            Device device1 = allDevices.First();
            DeviceItem head1 = getHead(device1);

            DeviceItem simocodeInterface1 =
               (from DeviceItem di in head1.DeviceItems
                where di.Name.Contains("interface")
                select di).First();

            NetworkInterface interfacePN1 = simocodeInterface1.GetService<NetworkInterface>();
            NodeComposition simocodeNetworkNodes1 = interfacePN1.Nodes;
            //simocodeNetworkNodes1.First().ConnectToSubnet(newSubnet);



            Device device2 = allDevices[1];
            DeviceItem head2 = getHead(device2);

            DeviceItem simocodeInterface2 =
               (from DeviceItem di in head2.DeviceItems
                where di.Name.Contains("interface")
                select di).First();

            NetworkInterface interfacePN2 = simocodeInterface2.GetService<NetworkInterface>();
            NodeComposition simocodeNetworkNodes2 = interfacePN2.Nodes;
            //simocodeNetworkNodes2.First().ConnectToSubnet(newSubnet);


            Console.WriteLine(simocodeNetworkNodes1.Count()); //just a counter

        }
        public static DeviceItem getHead(Device device)
        {
            foreach (DeviceItem deviceItem in device.DeviceItems)
            {
                if (deviceItem.Classification == DeviceItemClassifications.HM)
                    return deviceItem;
            }
            return null;
        }
    }
}
