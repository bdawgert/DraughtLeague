using System;
using System.Collections.Generic;
using System.Text;

namespace DraughtLeague.Untappd.Models
{
    public interface IClientService
    {
        int Id { get; set; }
        UntappdClient Client { get; set; }
    }
}
