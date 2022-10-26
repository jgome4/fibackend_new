using FI_Infra_Tools_Aggregate;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace FI_Infra_Tools_Implementation;

    public class DevOps : IDevOps
    {
        private VssConnection Connect()
        {
            Uri orgUrl = new Uri("https://dev.azure.com/devmanagementsystem");                      
            String personalAccessToken = "w53rezpxk63rpp4l76rm73aairwrkpvhoyq5aiijh3dzh6tkvenq";  
            return new VssConnection(orgUrl, new VssBasicCredential(string.Empty, personalAccessToken));
        }
        public WorkItem CreateBugUsingClientLib(string title, string steps, string info, string name, string unit)
        {
            String _personalAccessToken = "w53rezpxk63rpp4l76rm73aairwrkpvhoyq5aiijh3dzh6tkvenq";  
            VssBasicCredential credentials = new VssBasicCredential("", _personalAccessToken);
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Path = "/fields/System.Title",
                    Value = title + "metodo:" + " " + name + "-IP Client:" + unit
                }
            );

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Path = "/fields/Microsoft.VSTS.TCM.ReproSteps",
                    Value = info
                }
            );

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Path = "/fields/Microsoft.VSTS.Common.Priority",
                    Value = "1"
                }
            );

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Path = "/fields/Microsoft.VSTS.Common.Severity",
                    Value = "2 - High"
                }
            );
            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Path = "/fields/System.AssignedTo",
                    Value = "ingetecdeveloper@outlook.com"
                }
            );
            VssConnection connection = Connect();
            WorkItemTrackingHttpClient workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();
            WorkItem result = workItemTrackingHttpClient.CreateWorkItemAsync(patchDocument, "SIGP", "Bug").Result;
            return result;
     
        }


    public void GenerateIndicators()
    {

    }


    private async Task<IList<WorkItem>> QueryOpenBugs()
    {
        var credentials = new VssBasicCredential(string.Empty, "w53rezpxk63rpp4l76rm73aairwrkpvhoyq5aiijh3dzh6tkvenq");

        var wiql = new Wiql()
        {
            Query = "Select [Id] " +
                    "From WorkItems " +
                    "Where [Work Item Type] = 'Task' " +
                    "And [System.State] <> 'Closed' " +
                    "Order By [State] Asc, [Changed Date] Desc",
        };

        using (var httpClient = new WorkItemTrackingHttpClient(new Uri("https://dev.azure.com/devmanagementsystem"), credentials))
        {
            var result = await httpClient.QueryByWiqlAsync(wiql).ConfigureAwait(false);
            var ids = result.WorkItems.Select(item => item.Id).ToArray();

            if (ids.Length == 0)
            {
                return Array.Empty<WorkItem>();
            }

            var fields = new[] { "System.Id", "System.Title", "System.State" };

            return await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false);
        }
    }

    public async Task UpdateWorkItemsByQueryResults()
    {
        JsonPatchDocument patchDocument = new JsonPatchDocument();
        Decimal percentageDelayed = 0;
        DateTime dateTimeEstimatedEnd;
        DateTime dateToday = DateTime.Now;
        int difference = 0;
        int id=0;
        

        VssConnection connection =      Connect();
        WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();
        var workItems = await QueryOpenBugs().ConfigureAwait(false);

        foreach (var workItem in workItems)
        {
            patchDocument.Add(
            new JsonPatchOperation()
            {
                Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                Path = "/fields/Custom.PercentageDelayed",
                Value = "2",
                From = "ingetecdeveloper@outlook.com"
            }             
            );
            id= (int) workItem.Id;
            dateTimeEstimatedEnd = (DateTime) workItem.Fields["Custom.ESTIMATEDENDDATE"];
            difference = (dateTimeEstimatedEnd - dateToday).Days;
           WorkItem result = workItemTrackingClient.UpdateWorkItemAsync(patchDocument,id).Result;
            
        }
    }








}


