namespace SOAProject
{
    public class Toastr
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public ToastrType Type { get; set; }

        public Toastr()
        {

        }
        public Toastr(string message, string title = "Bilgi", ToastrType type = ToastrType.Info)
        {
            this.Message = message;
            this.Title = title;
            this.Type = type;
        }
    }
}