using System;
using System.Collections.Generic;

namespace h5_blazor_web_app.Models;

public partial class TodoList
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public byte[] Item { get; set; } = null!;
}
