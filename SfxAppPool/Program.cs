using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SfxAppPool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] apps = args[0].Split(',');
                foreach (string app in apps)
                {
                    CreateAppPool7(app);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message+ex.StackTrace);
            }
        }
        public static bool CreateAppPool7(string appPoolName)
        {
          
                ServerManager sm = new ServerManager();
                //判断是否存在应用程序池
                ApplicationPool appPool = sm.ApplicationPools[appPoolName];
                if (appPool == null)
                {
                    sm.ApplicationPools.Add(appPoolName);
                    ApplicationPool apppool = sm.ApplicationPools[appPoolName];

                  
                    
                    apppool.ManagedPipelineMode = ManagedPipelineMode.Integrated; 
                    apppool.ManagedRuntimeVersion = "v4.0";
                    apppool.Recycling.DisallowOverlappingRotation = true;
                    apppool.Recycling.PeriodicRestart.Time = TimeSpan.FromMinutes(0);
                    apppool.Recycling.PeriodicRestart.Schedule.Add(TimeSpan.Parse("00:00:00"));
                    apppool.Recycling.PeriodicRestart.Schedule.Add(TimeSpan.Parse("06:00:00"));
                    apppool.Recycling.PeriodicRestart.Schedule.Add(TimeSpan.Parse("12:30:00"));
                    apppool.Recycling.PeriodicRestart.Schedule.Add(TimeSpan.Parse("17:50:00"));
                    apppool.Recycling.LogEventOnRecycle = RecyclingLogEventOnRecycle.Memory
                        | RecyclingLogEventOnRecycle.Requests
                        | RecyclingLogEventOnRecycle.ConfigChange
                        | RecyclingLogEventOnRecycle.IsapiUnhealthy
                        | RecyclingLogEventOnRecycle.OnDemand
                        | RecyclingLogEventOnRecycle.PrivateMemory
                        | RecyclingLogEventOnRecycle.Schedule
                        | RecyclingLogEventOnRecycle.Time;
                    apppool.ProcessModel.IdleTimeout = TimeSpan.FromMinutes(0);
                    apppool.Failure.RapidFailProtection = false;
                    apppool.AutoStart = true;
                    sm.CommitChanges();
                 
                    return true;
                }
                else
                {
                    return false;
                }
            
            
        }
    }
}
