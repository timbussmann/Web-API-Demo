namespace DemoApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TodoTask
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "task title must at least be 3 characters long")]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}