using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Management;
using System.Collections.Generic;


namespace SystemThreads_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceController[] scServices = GetMachineServices(args);

            //ListCranSoftServices(scServices);

            string serviceStartsWith;

            if (args.Length >= 2)
            {
                serviceStartsWith = args[1];
            }
            else
            {
                serviceStartsWith = "CranSoft Service";
                //serviceStartsWith = "Quadrate";
            }

            ServiceController[] scFilteredServices = FilterServices(scServices, serviceStartsWith);

            GetInfoFromFilteredServices(scFilteredServices);

            //KillAllFilteredServices(scFilteredServices);
        }

        private static ServiceController[] GetMachineServices(string[] args)
        {
            if (args.Length > 0)
            {
                return ServiceController.GetServices(args[0]);
            }
            else
            {
                return ServiceController.GetServices();
            }
        }

        private static ServiceController[] FilterServices(ServiceController[] scServices, string serviceStartsWith)
        {
            List<ServiceController> cranSoftServices = new List<ServiceController>();
            foreach (ServiceController scService in scServices)
            {
                if (scService.ServiceName.StartsWith(serviceStartsWith))
                {
                    cranSoftServices.Add(scService);
                }
            }
            return cranSoftServices.ToArray();
        }

        private static ManagementObjectCollection GetServiceManagmentObjects(string serviceName)
        {
            string query = string.Format("SELECT ProcessId FROM Win32_Service WHERE Name='{0}'", serviceName);
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(query);
            
            return searcher.Get();
        }

        private static Process GetProcessFromMamagementObject(ManagementObject obj, string serviceName)
        {
            uint processId = (uint)obj["ProcessId"];
            try
            {
                return Process.GetProcessById((int)processId);
            }
            catch (ArgumentException)
            {
                Console.WriteLine(String.Format("Error process {0} was not found.", processId.ToString(), serviceName));
                throw;
            }
        }

        private static List<Process> GetChildProcesses(Process process)
        {
            List<Process> children = new List<Process>();
            ManagementObjectSearcher mos = new ManagementObjectSearcher(String.Format("Select * From Win32_Process Where ParentProcessID={0}", process.Id));

            foreach (ManagementObject mo in mos.Get())
            {
                Process child_process = null;
                child_process = Process.GetProcessById(Convert.ToInt32(mo["ProcessID"]));

                if (child_process != null)
                {
                    List<Process> offspring = new List<Process>();
                    offspring = GetChildProcesses(child_process);
                    children.AddRange(offspring);
                }
                else
                {
                    Console.WriteLine("Child process was null :(");
                }
                
                children.Add(child_process);
            }

            return children;
        }

        private static void GetInfoFromFilteredServices(ServiceController[] scFilteredServices)
        {

            foreach (ServiceController scService in scFilteredServices)
            {
                foreach (ManagementObject obj in GetServiceManagmentObjects(scService.ServiceName))
                { 
                    Process process;
                    process = GetProcessFromMamagementObject(obj, scService.ServiceName);

                    PrintThreadInfo(process, scService.ServiceName);

                    PrintChildProcessInfo(process, scService.ServiceName);

                }
            }
        }

        private static void PrintThreadInfo(Process process, string serviceName)
        {
            string message;
            message = String.Format("Service {0}, processName {1} ({3}) has {2} threads",
                serviceName, process.ProcessName, process.Threads.Count.ToString(), process.Id.ToString());

            Console.WriteLine(message);

            int activeThreads = 0;

            foreach (ProcessThread thread in process.Threads)
            {
                if (thread.ThreadState == ThreadState.Running)
                {
                    activeThreads++;
                }

                if (thread.ThreadState != ThreadState.Wait)
                {
                    Console.WriteLine(thread.ThreadState.ToString());
                }
                //Console.WriteLine("Thread name: " + thread.ToString());
                //Console.WriteLine("Thread site name: " + thread.Site.Name);   //DUMPS
            }

            message = String.Format("Service {0}, processID {1} has {2} active threads",
                serviceName, process.Id.ToString(), activeThreads.ToString());

            Console.WriteLine(message);
        }

        private static void PrintChildProcessInfo(Process process, string serviceName)
        {
            List<Process> processes = GetChildProcesses(process);

            Console.WriteLine("Number of kids: " + processes.Count);

            foreach (Process p in processes)
            {
                if (p == null)
                {
                    Console.WriteLine("Child was null!");
                    continue;
                }
                string message;
                message = String.Format("Service {0}, processName {1} ({2}) has child process {3} ({4})",
                    serviceName, process.ProcessName, process.Id.ToString(), p.ProcessName, p.Id.ToString());
                Console.WriteLine(message);

            }        
        }

        private static void ListCranSoftServices(ServiceController[] scServices)
        {
            foreach (ServiceController scService in scServices)
            {
                if (scService.Status != ServiceControllerStatus.Stopped)
                {
                    Console.WriteLine("Service name: " + scService.ServiceName + " is " + scService.Status.ToString());
                }
            }

            Console.WriteLine("Scan complete");
        }

        private static void KillAllFilteredServices(ServiceController[] scFilteredServices)
        {
            bool cranSoftServiceFound = false;

            foreach (ServiceController scService in scFilteredServices)
            {
                Console.WriteLine("Fond Cransoft Service: \"" + scService.ServiceName + "\" " + scService.Status.ToString() + " proceeding to kill all its processes.");
                cranSoftServiceFound = true;
                KillService(scService.ServiceName);
            }

            if (cranSoftServiceFound)
            {
                Console.WriteLine("CranSoft services terminated.");
            }
            else
            {
                Console.WriteLine("No CranSoft services have been found.");
            }

        }

        private static void KillService(string serviceName)
        {
            foreach (ManagementObject obj in GetServiceManagmentObjects(serviceName))
            {
                Process process = null;
                process = GetProcessFromMamagementObject(obj, serviceName);

                if (process != null)
                {
                    process.Kill();
                }
                //catch (Win32Exception) { // Thrown if process is already terminating }
                //catch (InvalidOperationException) {  // Thrown if the process has already terminated. }
            }

        }
    }
}
