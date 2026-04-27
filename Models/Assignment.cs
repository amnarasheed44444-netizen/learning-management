namespace LMS.Models
{
    class Assignment
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Title { get; set; }
        public string Student { get; set; }
        public string Submission { get; set; }
        public int Marks { get; set; }
    }
}