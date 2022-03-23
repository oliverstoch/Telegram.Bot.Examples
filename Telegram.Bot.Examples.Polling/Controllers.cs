using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Examples.Polling;

namespace  Telegram.Bot.Examples.controllers;

internal static class Controller
{
    public static async Task<JObject> PostABugToAsana(string customApprovalStatus, string customName,string customNotes)
    {

        //FIND WHERE TO PUT CLIENTS? ---[
        Console.WriteLine("calling some api for asana or something: ");
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + "1/1201865069369252:ad74856e67d98ad7704c97330ceb8b32");
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.BaseAddress = new Uri("https://app.asana.com/api/1.0/");
        client.DefaultRequestHeaders.Add("Host","app.asana.com");
        //]---



        //approval_status, name, notes,
        var wraped = new BugProps
        {
            data = new Data
            {
                approval_status = customApprovalStatus,
                assignee = "1201865069369252",
                assignee_status ="upcoming",
                completed=false,
                due_at=Convert.ToDateTime("2021-03-22T02:06:58.147Z"),
                followers= new List<string>{"1201865069369252"},
                html_notes= "<body>Mittens <em>really</em> likes the stuff from Humboldt.</body>",
                liked=false,
                name= customName,
                notes=customNotes,
                start_on="2021-03-14"
            }
        };
        var bugPropsJson = JsonConvert.SerializeObject(wraped);
        var propsString = new StringContent(bugPropsJson, Encoding.UTF8, "application/json");
        var postResponce  = await client.PostAsync("" + "workspaces/8050725593772/tasks/", propsString);
        /*
        HttpResponseMessage result = await client.GetAsync("https://app.asana.com/api/1.0/"  + "workspaces/8050725593772/users");
        */
        postResponce.EnsureSuccessStatusCode();

        string asText = await postResponce.Content.ReadAsStringAsync();
        return JObject.Parse(asText);
    }


    public static async Task<JObject> GetAllTasksForUser()
    {
        Console.WriteLine("calling some api for asana or something: ");

        //FIND WHERE TO PUT CLIENTS?
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + "1/1201865069369252:ad74856e67d98ad7704c97330ceb8b32");
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.BaseAddress = new Uri("https://app.asana.com/api/1.0/");
        client.DefaultRequestHeaders.Add("Host","app.asana.com");
        //]---


        var allTasksResponseMessage=await client.GetAsync(""+ "workspaces/8050725593772/tasks/?assignee=1201865069369252");
        string responseBody = await allTasksResponseMessage.Content.ReadAsStringAsync();
        //HOW IS THIS  TO BE USED?
        /*
        responseBody.EnsureSuccessStatusCode();
        */
        /*
        string asText = await responseBody.Content.ReadAsStringAsync();
        */
        return JObject.Parse(responseBody);
    }
}
