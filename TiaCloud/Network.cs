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
            networkNodes1.First().ConnectToSubnet(newSubnet);   //first connection

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
            //IoConnector ioConnector = null; //IoController feature is in construction

            networkNodes1.First().ConnectToSubnet(newSubnet);
           
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
     
        public static void multiConnection(Project project, int sourceIndex, int targetIndex)
        {
            var allSubnets = project.Subnets;
            var allDevices = project.Devices;

            Device source = allDevices[sourceIndex];
            Device target = allDevices[targetIndex];

            DeviceItem sourceHead = null;
            DeviceItem targetHead = null;

            if (source.Name.Contains("PLC"))    //source head
            {
                sourceHead = DeviceItemMethods.GetPlcHead(source);
            }
            else
                sourceHead = DeviceItemMethods.GetHead(source);


            if (target.Name.Contains("PLC"))    //target head
            {
                targetHead = DeviceItemMethods.GetPlcHead(target);
            }
            else
                targetHead = DeviceItemMethods.GetHead(target);

            DeviceItem sourceInterface =
                 (from DeviceItem di in sourceHead .DeviceItems
                  where di.Name.Contains("interface") || di.Name.Contains("MPI") || di.Name.Contains("DP") || di.Name.Contains("ASI")
                  select di).First();

            Console.WriteLine("-sourceInterface Name: "+sourceInterface.Name); 

            NetworkInterface sourceNetworkInterface = sourceInterface.GetService<NetworkInterface>();
            Node sourceNode = sourceNetworkInterface.Nodes.First();

            Console.WriteLine("--sourceNode Name: "+sourceNode.Name);

            DeviceItem targetInterface =
                 (from DeviceItem di in targetHead.DeviceItems
                  where di.Name.Contains("DP") || di.Name.Contains("MPI") || di.Name.Contains("interface") || di.Name.Contains("ASI")
                  select di).First();

            //multiple connection is impossible on same device, such as simatic2
            //because the function always gets just first node of the interface

            Console.WriteLine("-targetInterface Name: " + targetInterface.Name);

            NetworkInterface targetNetworkInterface = targetInterface.GetService<NetworkInterface>();
            Node targetNode = targetNetworkInterface.Nodes.First();

            Console.WriteLine("--targetNode Name: " + targetNode.Name);

            Console.WriteLine("SUBNET NAMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE::::::::::::::::::::::::::::.");
            Console.WriteLine(targetNode.ConnectedSubnet.GetAttribute("Name"));

            sourceNode.ConnectToSubnet(targetNode.ConnectedSubnet);


        }

        //First,follow the path: project > device > deviceItem > interface > nodes > node
        //check node is connected to a subnet or not 
        //then, if is connected ? 
        //if there is no connection apply same function with small changes

        public static void selectConnection(Project project ,int sourceIndex, int targetIndex, string typeIdentifier,string command)
        {
            if(command == "multiCon")
            {
                Console.WriteLine("selectConnection func. enterance");
                multiConnection(project, sourceIndex, targetIndex);
                Console.WriteLine("selectConnection func. exit");
            }

            else
            { 
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
                        Console.WriteLine("DEFAULT CONNECTION!");
                        connectToPN(project, sourceIndex, targetIndex);

                        break;
                }
            }
        }
    }
}
