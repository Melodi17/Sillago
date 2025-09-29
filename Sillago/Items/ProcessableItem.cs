namespace Sillago
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Utils;

    public class ProcessableItem : Item
    {
        public Item BaseItem { get; }
        public IReadOnlyList<string> Processes { get; }
        
        private static int HashProcesses(List<string> processes)
        {
            int hash = 17;
            foreach (string process in processes)
                hash = hash * 31 + process.GetStableHashCode();
            return hash;
        }
        
        private ProcessableItem(Item baseItem, List<string> processes)
            : base($"{baseItem.Id}_{HashProcesses(processes)}", baseItem.Name, GetInfo(baseItem, processes))
        {
            this.BaseItem = baseItem;
            this.Processes = processes.AsReadOnly();
        }
        
        public static ProcessableItem Adding(Item baseItem, string entry)
        {
            if (baseItem is ProcessableItem pi)
            {
                List<string> newProcesses = new(pi.Processes);
                newProcesses.Add(entry);
                return new ProcessableItem(pi.BaseItem, newProcesses);
            }
            else
                return new ProcessableItem(baseItem, new List<string> { entry });
        }
        
        public static string GetInfo(Item baseItem, List<string> recipes)
        {
            StringBuilder sb = new();
            sb.AppendLine(baseItem.Description);
            sb.AppendLine("Processes:");
            foreach (string entry in recipes)
                sb.AppendLine($"  - {entry}");
            return sb.ToString().TrimEnd();
        }
    }
}