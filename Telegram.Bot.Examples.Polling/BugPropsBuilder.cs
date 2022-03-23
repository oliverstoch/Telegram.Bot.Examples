namespace Telegram.Bot.Examples.Polling
{


    public class Data
    {

        public string approval_status { get; set; }
        public string assignee { get; set; }
        public string assignee_status { get; set; }
        public bool completed { get; set; }
        public DateTime due_at { get; set; }
        public List<string> followers { get; set; }
        public string html_notes { get; set; }
        public bool liked { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public string start_on { get; set; }
    }

    public class BugProps
    {
        public Data data { get; set; }
    }



}
