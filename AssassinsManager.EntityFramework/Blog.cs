using System.ComponentModel.DataAnnotations.Schema;

namespace AssassinsManager.EntityFramework;

public class Blog
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public string Text { get; set;}
    public DateTime Timestamp { get; set;}
}
