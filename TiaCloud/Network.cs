using System;
using System.Linq;
using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;


namespace TiaCloud

{
    class Network
    {
        public static void connectToPN(Project project,int sourceDevice, int targetDevice)
        {
            var allDevices = project.Devices;

            //creating subnet to not connected
            SubnetComposition subnets = project.Subnets;

            Subnet newSubnet = subnets.Create("System:Subnet.Ethernet", "Profinet Subnet_" + Guid.NewGuid().ToString());

            //subnet typeIdentifiers can be .Profibus, .Mpi, .Asi 

            //source device
            Device device1 = DeviceMethods.GetDevice(project,sourceDevice);
            Device device2 = DeviceMethods.GetDevice(project, targetDevice);

            DeviceItem head1 = null;
            DeviceItem head2 = null;
            DeviceItem interface1 = null;

            if (device1.Name.Contains("PLC"))
            {
                head1 = DeviceItemMethods.GetPlcHead(device1);
                interface1 =
              (from DeviceItem di in head1.DeviceItems
               where di.Name.Contains("PROFINET")
               select di).First();
            }

            else
            {
                head1 = DeviceItemMethods.GetHead(device1);
                interface1 =
                (from DeviceItem di in head1.DeviceItems
                 where di.Name.Contains("interface")
                 select di).First();
            }

            Console.WriteLine(head1.GetAttribute("Name"));

            NetworkInterface interfacePN1 = interface1.GetService<NetworkInterface>();
            NodeComposition networkNodes1 = interfacePN1.Nodes;
            networkNodes1.First().ConnectToSubnet(newSubnet);
            //target device
            DeviceItem interface2 = null;

            if (device2.Name.Contains("PLC"))
            {
                 head2 = DeviceItemMethods.GetPlcHead(device2);

                 interface2 =
             (from DeviceItem di in head2.DeviceItems
              where di.Name.Contains("PROFINET")
              select di).First();
            }
        
            else
            {
                head2 = DeviceItemMethods.GetHead(device2);

                interface2 =
            (from DeviceItem di in head2.DeviceItems
             where di.Name.Contains("interface")
             select di).First();
            }

            NetworkInterface interfacePN2 = interface2.GetService<NetworkInterface>();
            NodeComposition networkNodes2 = interfacePN2.Nodes;
              //second device connection
           networkNodes2.First().ConnectToSubnet(newSubnet);

            Console.WriteLine("Connected to " + newSubnet.GetAttribute("Name"));



            //SubnetComposition subnets = project.Subnets;
            //Subnet newSubnet = subnets.Create("System:Subnet.Asi", "ASI");

            ////prepare 1 PLC start
            //Device device = project.Devices.Create("System:Device.ASi", "ASI_Name" + Guid.NewGuid().ToString());
            //DeviceItem rack = device.PlugNew("System:Rack.ASi", "Rack_Name" + Guid.NewGuid().ToString(), 0);
            //DeviceItem head = rack.PlugNew("OrderNumber:AS-i F Slave Universal", "Head_name" + Guid.NewGuid().ToString(), 1);
            ////DeviceItem head = DeviceItemMethods.getHead(plc); //getHead bozuk baklması gerekiyor


            //////our first connection
            //DeviceItem plcInterface =
            //    (from DeviceItem di in head.DeviceItems
            //     where di.Name.Contains("AS-Interface")
            //     select di).First();
            ////find interface deviceitem

            //NetworkInterface interfacePN = plcInterface?.GetService<NetworkInterface>();    //now get interface service from it
            //NodeComposition plcNetworkNodes = interfacePN.Nodes;                            //one interface can have several ports
            //plcNetworkNodes.First().ConnectToSubnet(newSubnet);                             //connect to our PN bus. note that this plc can also connect to DP bus if needed
            //Console.WriteLine(plcInterface.Name + " connected to subnet");

            //Device device2 = project.Devices.Create("System:Device.ASi", "ASI_Name" + Guid.NewGuid().ToString());
            //DeviceItem rack2 = device2.PlugNew("System:Rack.ASi", "Rack_Name" + Guid.NewGuid().ToString(), 0);
            //DeviceItem head2 = rack2.PlugNew("OrderNumber:AS-i F Slave Universal", "Head_name" + Guid.NewGuid().ToString(), 1);
            ////our first connection
            //DeviceItem plcInterface2 =
            //    (from DeviceItem di in head2.DeviceItems
            //     where di.Name.Contains("AS-Interface")
            //     select di).First();                                                        //find interface deviceitem
            //NetworkInterface interfacePN2 = plcInterface2?.GetService<NetworkInterface>();    //now get interface service from it
            //NodeComposition plcNetworkNodes2 = interfacePN2.Nodes;                            //one interface can have several ports
            //plcNetworkNodes2.First().ConnectToSubnet(newSubnet);                             //connect to our PN bus. note that this plc can also connect to DP bus if needed
            //Console.WriteLine(plcInterface.Name + " connected to subnet");

        }


        public static void connectToPB(Project project, int sourceDevice, int targetDevice)
        {
            var allDevices = project.Devices;

            //creating subnet to not connected
            SubnetComposition subnets = project.Subnets;

            Subnet newSubnet = subnets.Create("System:Subnet.Profibus", "Profibus Subnet_" + Guid.NewGuid().ToString());

            //subnet typeIdentifiers can be .Profibus, .Mpi, .Asi 

            //source device
            Device device2 = DeviceMethods.GetDevice(project, targetDevice);
            Device device1 = DeviceMethods.GetDevice(project, sourceDevice);
            
            DeviceItem head1 = null;
            DeviceItem head2 = null;

            DeviceItem interface1 = null;

            if (device1.Name.Contains("PLC")) {

                head1 = DeviceItemMethods.GetPlcHead(device1);

                interface1 =
               (from DeviceItem di in head1.DeviceItems
                where di.Name.Contains("DP") 
                select di).First();
            }

            else
            {
                 head1 = DeviceItemMethods.GetHead(device1);

                interface1 =
                    (from DeviceItem di in head1.DeviceItems
                     where di.Name.Equals(head1.Name)
                     select di).First();
            }

            NetworkInterface interfacePN1 = interface1.GetService<NetworkInterface>();
            NodeComposition networkNodes1 = interfacePN1.Nodes;
            
            //IoController plcIoController = interfacePN1.IoControllers.First();
            //IoSystem plcIoSystem = plcIoController?.CreateIoSystem("myIoSystem" + Guid.NewGuid().ToString());
            //IoConnector ioConnector = null;
            networkNodes1.First().ConnectToSubnet(newSubnet);
            //ioConnector = interfacePN1.IoConnectors.First();
            //ioConnector.ConnectToIoSystem(plcIoSystem);

            //connection to the subnet
            //target device
            DeviceItem interface2 = null;

            if (device2.Name.Contains("PLC"))
            {

                head2 = DeviceItemMethods.GetPlcHead(device2);

                interface2 =
               (from DeviceItem di in head2.DeviceItems
                where di.Name.Contains("DP")
                select di).First();
            }

            else
            {
                head2 = DeviceItemMethods.GetHead(device2);
                foreach (DeviceItem di in head2.DeviceItems)
                    Console.WriteLine("------------------" + di.Name);
               

                interface2 =
                    (from DeviceItem di in head2.DeviceItems
                     where di.Name.Equals(head2.Name)
                     select di).First();
            }

         
            NetworkInterface interfacePN2 = interface2.GetService<NetworkInterface>();
            NodeComposition networkNodes2 = interfacePN2.Nodes;
            networkNodes2.First().ConnectToSubnet(newSubnet);   //second device connection


            Console.WriteLine("Connected to " + newSubnet.GetAttribute("Name"));


        }
        public static void connectToASI(Project project, int sourceDevice, int targetDevice)
        {
            var allDevices = project.Devices;

            //creating subnet to not connected
            SubnetComposition subnets = project.Subnets;

            Subnet newSubnet = subnets.Create("System:Subnet.Asi", "ASI Subnet" + Guid.NewGuid().ToString());

            //subnet typeIdentifiers can be .Profibus, .Mpi, .Asi 

            Device device2 = DeviceMethods.GetDevice(project, targetDevice);
            Device device1 = DeviceMethods.GetDevice(project, sourceDevice);

            DeviceItem head1 = DeviceItemMethods.GetHead(device1);

            DeviceItem interface1 =
               (from DeviceItem di in head1.DeviceItems
                where di.Name.Contains("AS-Interface")
                select di).First();

            NetworkInterface interfacePN1 = interface1.GetService<NetworkInterface>();
            NodeComposition networkNodes1 = interfacePN1.Nodes;
            networkNodes1.First().ConnectToSubnet(newSubnet);   //connection to the subnet 

            //target device
            DeviceItem head2 = DeviceItemMethods.GetHead(device2);

            DeviceItem interface2 =
               (from DeviceItem di in head2.DeviceItems
                where di.Name.Contains("AS-Interface")
                select di).First();

            NetworkInterface interfacePN2 = interface2.GetService<NetworkInterface>();
            NodeComposition networkNodes2 = interfacePN2.Nodes;
            networkNodes2.First().ConnectToSubnet(newSubnet);   //second device connection


            Console.WriteLine("Connected to " + newSubnet.GetAttribute("Name"));


        }
        public static void connectToMPI(Project project, int sourceDevice, int targetDevice)
        {
            DeviceComposition allDevices = project.Devices;

            //creating subnet to not connected
            SubnetComposition subnets = project.Subnets;

            Subnet newSubnet = subnets.Create("System:Subnet.Mpi", "MPI Subnet" + Guid.NewGuid().ToString());

            //subnet typeIdentifiers can be .Profibus, .Mpi, .Asi 

            //source device
            Device device2 = DeviceMethods.GetDevice(project, targetDevice);
            Device device1 = DeviceMethods.GetDevice(project, sourceDevice);

            DeviceItem head1 = DeviceItemMethods.GetPlcHead(device1);
         

            DeviceItem interface1 =
               (from DeviceItem di in head1.DeviceItems
                where di.Name.Contains("MPI")
                select di).First();

            NetworkInterface interfacePN1 = interface1.GetService<NetworkInterface>();
            NodeComposition networkNodes1 = interfacePN1.Nodes;
            networkNodes1.First().ConnectToSubnet(newSubnet);   //connection to the subnet 

            //target device
            DeviceItem head2 = DeviceItemMethods.GetPlcHead(device2);

            DeviceItem interface2 =
               (from DeviceItem di in head2.DeviceItems
                where di.Name.Contains("MPI")
                select di).First();

            NetworkInterface interfacePN2 = interface2.GetService<NetworkInterface>();
            NodeComposition networkNodes2 = interfacePN2.Nodes;
            networkNodes2.First().ConnectToSubnet(newSubnet);   //second device connection


            Console.WriteLine("Connected to " + newSubnet.GetAttribute("Name"));


        }
        public static bool isConnected(Project project,Device device)
        {
            var allSubnets = project.Subnets;
            var allDevices = project.Devices;

            Device theDevice = allDevices.Find(device.Name);    
            DeviceItem theHead = DeviceItemMethods.GetHead(device);
            DeviceItem Interface =
                 (from DeviceItem di in theHead.DeviceItems
                  where di.Name.Contains("interface")
                  select di).First();
            NetworkInterface networkInterface = Interface.GetService<NetworkInterface>();
            NodeComposition nodes = networkInterface.Nodes;
            Node interfaceNode = nodes.First();

            Console.WriteLine(device.Name + " is connected to: ");
            Console.WriteLine(interfaceNode.ConnectedSubnet.Name);

            string name = interfaceNode.ConnectedSubnet.Name;
            if (name != null)
                return true;
            else
                return false;

            
        }

        public static bool isPLCconnected(Project project, Device device)
        {
            var allSubnets = project.Subnets;
            var allDevices = project.Devices;

            Device theDevice = allDevices.Find(device.Name);
            DeviceItem theHead = DeviceItemMethods.GetPlcHead(device);
            DeviceItem Interface =
                 (from DeviceItem di in theHead.DeviceItems
                  where di.Name.Contains("DP") || di.Name.Contains("MPI")
                  select di).First();
            NetworkInterface networkInterface = Interface.GetService<NetworkInterface>();
            NodeComposition nodes = networkInterface.Nodes;
            Node interfaceNode = nodes.First();

            Console.WriteLine(device.Name + "  is connected to:  ");
            Console.WriteLine(interfaceNode.ConnectedSubnet.Name +"\n");
            
            string name = interfaceNode.ConnectedSubnet.Name;
            if (name != null)
                return true;
            else
                return false;
        }

        //First,follow the path: project > device > deviceItem > interface > nodes > node
        //check node is connected to a subnet or not 
        //then, if is connected ? 
        //if there is no connection apply same function with small changes

        public static void selectConnection(Project project ,int sourceIndex, int targetIndex, string typeIdentifier)
        {
            SubnetComposition subnets = project.Subnets;
            switch (typeIdentifier)
            {
                case "PN":
                case "pn":
                    connectToPN(project, sourceIndex, targetIndex);
                    break;

                case "pb":
                case "PB":
                    connectToPB(project, sourceIndex, targetIndex);

                    break;

                case "mpi":
                case "MPI":
                    connectToMPI(project, sourceIndex, targetIndex);

                    break;

                case "asi":
                case "ASI":
                    connectToASI(project, sourceIndex, targetIndex);

                    break;

                default:
                    Console.WriteLine("DEFAULT!");
                    connectToPN(project, sourceIndex, targetIndex);

                    break;
            }
           
        }
    }
}
