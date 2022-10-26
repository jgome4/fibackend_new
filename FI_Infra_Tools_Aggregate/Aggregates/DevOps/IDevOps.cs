using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace FI_Infra_Tools_Aggregate;

    public interface IDevOps
    {
        WorkItem CreateBugUsingClientLib(string title, string steps, string info, string name, string unit);
        void GenerateIndicators();   
}

