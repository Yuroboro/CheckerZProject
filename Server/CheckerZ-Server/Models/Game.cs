using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CheckerZ_Server.Models;

//An object represnting a game stored in server database
public partial class Game
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    [Display(Name = "Date")]
    public DateTime GameDate { get; set; } = default(DateTime);

    [Display(Name = "Duration")]
    public int Duration { get; set; }

    [Display(Name = "Result")]
    public string GameOutcome { get; set; } = "";

    [Display(Name = "Player")]
    [JsonIgnore]
    public virtual Player? Player { get; set; }
}
