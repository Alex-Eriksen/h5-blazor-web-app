﻿using System;
using System.Collections.Generic;

namespace h5_blazor_web_app.Models;

public partial class Cpr
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string CprNr { get; set; } = null!;
}
